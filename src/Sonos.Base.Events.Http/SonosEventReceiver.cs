using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Sonos.Base.Events.Http.Models;
using Sonos.Base.Events.Http.Parsing;
using Sonos.Base.Services;
using System.Collections.Concurrent;
using System.Net;
using System.Xml.Serialization;

namespace Sonos.Base.Events.Http;
/// <summary>
/// SonosEventReceiver is an Http based sonos event receiver, which implements a receiver that can subscribe to Sonos UPNP events
/// </summary>
/// <remarks>Add as IHostedService to your project.</remarks>
public partial class SonosEventReceiver : IHostedService, ISonosEventBus
{
    private readonly HttpClient httpClient;
    private readonly ILogger<SonosEventReceiver> logger;
    private readonly SonosEventReceiverOptions options;
    private readonly ConcurrentDictionary<string, SonosEventSubscription> subscriptions = new();
    private readonly WebApplication _webApplication;

    /// <summary>
    /// SonosEventReceiver constructor
    /// </summary>
    /// <param name="httpClient">Configure your http client</param>
    /// <param name="logger">Log events from the actual receiver</param>
    /// <param name="loggerProvider">Pass through the main logger provider to the child web host for events</param>
    /// <param name="settings">SonosEventReceiverOptions settings</param>
    /// <remarks>Probably called from dependency injection</remarks>
    public SonosEventReceiver(HttpClient? httpClient = null, ILogger<SonosEventReceiver>? logger = null, ILoggerProvider? loggerProvider = null, IOptions<SonosEventReceiverOptions>? settings = null)
    {
        this.logger = logger ?? new NullLogger<SonosEventReceiver>();
        this.httpClient = httpClient ?? new HttpClient();
        options = settings?.Value ?? new SonosEventReceiverOptions();
        _webApplication = CreateWebApplication(options.Port, loggerProvider);
    }

    /// <summary>
    /// Renew a subscription to a sonos service
    /// </summary>
    /// <param name="uuid">UUID of the player</param>
    /// <param name="service">Sonos Service that wants the events</param>
    /// <param name="cancellationToken">CancellationToken to cancel the renew subscription request</param>
    /// <returns>true when the subscription was renewed successfully</returns>
    public async Task<bool> RenewSubscription(string uuid, SonosService service, CancellationToken cancellationToken = default)
    {
        var subscription = GetSubscription(uuid, service);
        if (subscription is null)
        {
            return false;
        }
        int timeoutInSeconds = 1800;

        var req = new HttpRequestMessage(new HttpMethod("SUBSCRIBE"), subscription.EventUri);
        req.Headers.Add("SID", subscription.Sid);
        req.Headers.Add("Timeout", $"Second-{timeoutInSeconds}");

        var response = await httpClient.SendAsync(req, cancellationToken);

        LogRenewedSubscription(uuid, service, response.IsSuccessStatusCode);

        if (!response.IsSuccessStatusCode)
        {
            
            // Try new subscription since this one failed
            return await Subscribe(uuid, service, subscription.EventUri, subscription.Callback, cancellationToken);
        }

        return true;
    }

    /// <summary>
    /// IHostedService Implementation to start the child web application
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        LogStartAsyncCalled();
        return _webApplication.StartAsync(cancellationToken);
    }

    /// <summary>
    /// IHostedService implementation to stop the child web application
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        LogStopAsyncCalled();
        // Unsubscribe from all events, without waiting for the result
        // Explicitly use CancellationToken.None to ensure we try to unsubscribe even when the main token is cancelled
        await UnsubscribeAll(CancellationToken.None);
        await _webApplication.StopAsync(cancellationToken);
    }

    /// <summary>
    /// Subscribe for events from this Sonos service
    /// </summary>
    /// <param name="uuid">UUID of the player</param>
    /// <param name="service">Sonos Service that wants the events</param>
    /// <param name="eventEndpoint">Endpoint of the events</param>
    /// <param name="callback">Action that is called when a new event is parsed</param>
    /// <param name="cancellationToken">CancellationToken to cancel the create subscription request</param>
    /// <returns>true when the subscription was made successfully</returns>
    public async Task<bool> Subscribe(string uuid, SonosService service, Uri eventEndpoint, Action<IServiceEvent> callback, CancellationToken cancellationToken = default)
    {
        int timeoutInSeconds = 1800;
        var callbackUri = GenerateCallback(uuid, service);
        LogSubscribeStart(uuid, service, callbackUri);
        var req = new HttpRequestMessage(new HttpMethod("SUBSCRIBE"), eventEndpoint);
        req.Headers.Add("callback", $"<{callbackUri}>");
        req.Headers.Add("NT", "upnp:event");
        req.Headers.Add("Timeout", $"Second-{timeoutInSeconds}");

        var response = await httpClient.SendAsync(req, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var e = new SonosException("Event subscription failed") { StatusCode = (int)response.StatusCode };
            LogSubscribingFailed(e, uuid, service, callbackUri);
            throw e;
        }

        var sid = response.Headers.TryGetValues("sid", out var values) ? values.FirstOrDefault() : null;
        if (sid is null || string.IsNullOrEmpty(sid))
        {
            var e = new SonosException("Event subscription failed, no subscription ID");
            LogSubscribingFailed(e, uuid, service, callbackUri);
            throw e;
        }

        this.subscriptions[$"{uuid}-{service}"] = new SonosEventSubscription(uuid, service, eventEndpoint, sid, callback);

        LogSubscribingFinished(uuid, service, sid);

        return true;
    }

    /// <summary>
    /// Cancel an existing subscription, so the events will stop
    /// </summary>
    /// <param name="uuid">UUID of the player</param>
    /// <param name="service">Sonos Service you subscribed to</param>
    /// <param name="cancellationToken">CancellationToken to cancel the cancel subscription request</param>
    /// <returns>true when the unsubscription request was successfull</returns>
    public async Task<bool> Unsubscribe(string uuid, SonosService service, CancellationToken cancellationToken = default)
    {
        LogUnsubscribeStart(uuid, service);
        var subscription = GetSubscription(uuid, service);
        if (subscription is null)
        {
            LogSubscriptionNotFound(uuid, service);
            return false;
        }

        var result = await SendUnsubscribeRequestAsync(subscription.EventUri, subscription.Sid, cancellationToken);
        LogUnsubscribeFinished(uuid, service, result);
        subscriptions.TryRemove($"{uuid}-{service}", out var _);
        return result;
    }

    /// <summary>
    /// Cancel all existing subscriptions, so the events will stop
    /// </summary>
    /// <param name="cancellationToken">CancellationToken to cancel the cancel subscription request</param>
    /// <returns>true when then ubsubscription request was successfull</returns>
    public async Task<bool> UnsubscribeAll(CancellationToken cancellationToken = default)
    {
        LogUnsubscribeAllStart();
        var result = false;
        foreach (var subscription in subscriptions.Values)
        {
            try
            {
                var success = await SendUnsubscribeRequestAsync(subscription.EventUri, subscription.Sid, cancellationToken);
                result = result || success;
                subscriptions.TryRemove($"{subscription.Uuid}-{subscription.Service}", out var _);
            }
            catch (Exception ex)
            {
                LogUnsubscribeFailed(ex, subscription.EventUri, subscription.Sid);
            }
        }
        return result;
    }

    internal void EmitEventLastChangeAsync<TXml, TEvent>(string uuid, Services.SonosService service, string? lastChange) where TXml : class, IParsedEvent<TEvent> where TEvent : class, IServiceEvent
    {
        if (lastChange is null)
        {
            return;
        }
        var serializer = new XmlSerializer(typeof(TXml));
        using var eventReader = new StringReader(lastChange);
        var data = (IParsedEvent<TEvent>?)serializer.Deserialize(eventReader);

        TEvent? actualEvent = data?.GetEvent();
        logger.LogDebug("{service} event received {@event}", service, actualEvent);
        EmitParsedEvent(uuid, service, actualEvent);
    }

    internal void EmitParsedEvent(string uuid, SonosService service, IServiceEvent? serviceEvent)
    {
        LogEventParsed(uuid, service, serviceEvent);

        var subscription = GetSubscription(uuid, service);
        if (serviceEvent is not null && subscription is not null)
        {
            subscription.Callback.Invoke(serviceEvent);
        }
    }

    internal void EmitZoneGroupTopologyEvent(string uuid, Dictionary<string, string>? properties)
    {
        //logger?.LogDebug("Zone event {@properties}", properties);
        var eventData = ZoneGroupTopologyEvent.FromDictionary(properties);
        EmitParsedEvent(uuid, SonosService.ZoneGroupTopology, eventData);
    }

    internal async Task<IResult> HandleEventAsync([FromRoute] string uuid, [FromRoute] SonosService service, HttpContext ctx)
    {
        try
        {
            using var reader = new StreamReader(ctx.Request.Body);
            var body = await reader.ReadToEndAsync();
            LogRawXml(uuid, service, body);
            using var stringReader = new StringReader(body);
            var serializer = new XmlSerializer(typeof(PropertyCollection));
            var result = (PropertyCollection?)serializer.Deserialize(stringReader);

            if (result is null || result.property is null) // result?.property?[0].LastChange is null
            {
                LogEventEmpty(uuid, service);
                return Results.Ok();
            }
            var eventData = result?.GetEventProperties();

            switch (service)
            {
                //case Services.SonosServices.Unknown:
                //    break;
                case SonosService.AlarmClock:
                    EmitParsedEvent(uuid, service, AlarmClockEvent.FromDictionary(eventData));
                    break;

                case SonosService.AudioIn:
                    EmitParsedEvent(uuid, service, AudioInEvent.FromDictionary(eventData));
                    break;

                case SonosService.AVTransport:
                    EmitEventLastChangeAsync<AVTransportEventRoot, AVTransportEvent>(uuid, SonosService.AVTransport, result?.property[0].LastChange);
                    break;

                case SonosService.ConnectionManager:
                    EmitParsedEvent(uuid, service, ConnectionManagerEvent.FromDictionary(eventData));
                    break;

                case SonosService.ContentDirectory:
                    EmitParsedEvent(uuid, service, ContentDirectoryEvent.FromDictionary(eventData));
                    break;

                case SonosService.DeviceProperties:
                    EmitParsedEvent(uuid, service, DevicePropertiesEvent.FromDictionary(eventData));
                    break;

                case SonosService.GroupManagement:
                    EmitParsedEvent(uuid, service, GroupManagementEvent.FromDictionary(eventData));
                    break;

                case SonosService.GroupRenderingControl:
                    EmitParsedEvent(uuid, service, GroupRenderingControlEvent.FromDictionary(eventData));
                    break;

                case SonosService.HTControl:
                    EmitParsedEvent(uuid, service, HTControlEvent.FromDictionary(eventData));
                    break;

                case SonosService.MusicServices:
                    EmitParsedEvent(uuid, service, MusicServicesEvent.FromDictionary(eventData));
                    break;
                //case Services.SonosService.QPlay:
                //    // QPLAY does not have events
                //    EmitParsedEvent(uuid, service, QPlayService.FromDictionary(eventData));
                //    break;
                case SonosService.Queue:
                    EmitParsedEvent(uuid, service, QueueEvent.FromDictionary(eventData));
                    break;

                case SonosService.RenderingControl:
                    EmitEventLastChangeAsync<RenderingControlEventRoot, RenderingControlEvent>(uuid, SonosService.RenderingControl, result?.property[0].LastChange);
                    break;

                case SonosService.SystemProperties:
                    EmitParsedEvent(uuid, service, SystemPropertiesEvent.FromDictionary(eventData));
                    break;

                case SonosService.VirtualLineIn:
                    EmitParsedEvent(uuid, service, VirtualLineInEvent.FromDictionary(eventData));
                    break;

                case SonosService.ZoneGroupTopology:
                    EmitZoneGroupTopologyEvent(uuid, eventData);
                    break;

                default:
                    logger?.LogDebug("Event for {service} was not parsed {@event}", service, eventData);
                    break;
            }

            //var body = await reader.ReadToEndAsync();
            return Results.Ok();
        }
        catch (Exception ex)
        {
            LogEventParsingError(ex, uuid, service);
        }

        return Results.StatusCode(500);
    }

    internal async Task<IResult> HandleTestAsync(HttpContext ctx)
    {
        using var reader = new StreamReader(ctx.Request.Body);
        var body = await reader.ReadToEndAsync();
        throw new NotImplementedException();
    }

    private WebApplication CreateWebApplication(int port, ILoggerProvider? loggerProvider = null)
    {

        var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions
        {
            ApplicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            Args = new[]
            {
                $"--urls=http://*:{port}/"
            },
        });
        builder.Configuration.Sources.Clear();

        builder.WebHost.ConfigureKestrel(builder =>
        {
            builder.Listen(IPAddress.Any, port);
        });
        builder.WebHost.UseKestrelCore();

        builder.Services.AddRouting();
        
        builder.Logging.ClearProviders();
        if (loggerProvider != null)
        {
            builder.Logging.AddProvider(loggerProvider);
        }

        var app = builder.Build();
        app.Urls.Add($"http://*:{port}/");
        app
            .MapMethods("/event/{uuid}/{service}", new[] { "NOTIFY" }, HandleEventAsync);
        //.Accepts<Models.PropertySet>("text/xml");
        app.MapGet("/", () => "Sonos-net Event Service");

        return app;
    }

    private string GenerateCallback(string uuid, SonosService service)
    {
        
        return new Uri(new Uri($"http://{options.Host ?? "sonosevents.local"}:{options.Port}/event/"), $"{uuid}/{service}").ToString();
    }

    private SonosEventSubscription? GetSubscription(string uuid, SonosService service) =>
        subscriptions.TryGetValue($"{uuid}-{service}", out var result) ? result : null;

    private async Task<bool> SendUnsubscribeRequestAsync(Uri eventUri, string sid, CancellationToken cancellationToken)
    {
        var req = new HttpRequestMessage(new HttpMethod("UNSUBSCRIBE"), eventUri);
        req.Headers.Add("SID", sid);

        var response = await httpClient.SendAsync(req, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    [LoggerMessage(EventId = 21, Level = LogLevel.Information, Message = "StartAsync Called")]
    private partial void LogStartAsyncCalled();

    [LoggerMessage(EventId = 22, Level = LogLevel.Information, Message = "StopAsync Called")]
    private partial void LogStopAsyncCalled();

    [LoggerMessage(EventId = 201, Level = LogLevel.Debug, Message = "Empty event received {Uuid}/{Service}")]
    private partial void LogEventEmpty(string uuid, SonosService service);

    [LoggerMessage(EventId = 200, Level = LogLevel.Debug, Message = "Event parsed {Uuid}/{Service} {Data}")]
    private partial void LogEventParsed(string uuid, SonosService service, dynamic? data);

    [LoggerMessage(EventId = 202, Level = LogLevel.Warning, Message = "Error parsing event {Uuid}/{Service}")]
    private partial void LogEventParsingError(Exception e, string uuid, SonosService service);

    [LoggerMessage(EventId = 300, Level = LogLevel.Trace, Message = "Event XML for {Uuid}/{Service}\r\n{Xml}")]
    private partial void LogRawXml(string uuid, SonosService service, string xml);

    [LoggerMessage(EventId = 100, Level = LogLevel.Debug, Message = "Renewed subscription to {Uuid}/{Service} success: {Success}")]
    private partial void LogRenewedSubscription(string uuid, SonosService service, bool success);
    [LoggerMessage(EventId = 110, Level = LogLevel.Debug, Message = "Subscribe request {Uuid}/{Service}, callback: {CallbackUri}")]
    private partial void LogSubscribeStart(string uuid, SonosService service, string callbackUri);

    [LoggerMessage(EventId = 111, Level = LogLevel.Warning, Message = "Subscribing failed {Uuid}/{Service}, callback: {CallbackUri}")]
    private partial void LogSubscribingFailed(Exception e, string uuid, SonosService service, string callbackUri);

    [LoggerMessage(EventId = 112, Level = LogLevel.Information, Message = "Subscribing finished {Uuid}/{Service}, sid: {Sid}")]
    private partial void LogSubscribingFinished(string uuid, SonosService service, string sid);
    [LoggerMessage(EventId = 121, Level = LogLevel.Warning, Message = "Subscription not found {Uuid}/{Service}")]
    private partial void LogSubscriptionNotFound(string uuid, SonosService service);

    [LoggerMessage(EventId = 140, Level = LogLevel.Information, Message = "Unsubscribe all services")]
    private partial void LogUnsubscribeAllStart();

    [LoggerMessage(EventId = 141, Level = LogLevel.Warning, Message = "Unsubscribing failed {EventUri} {Sid}")]
    private partial void LogUnsubscribeFailed(Exception e, Uri eventUri, string sid);

    [LoggerMessage(EventId = 122, Level = LogLevel.Information, Message = "Unsubscribe finished {Uuid}/{Service} success {Success}")]
    private partial void LogUnsubscribeFinished(string uuid, SonosService service, bool success);

    [LoggerMessage(EventId = 120, Level = LogLevel.Debug, Message = "Unsubscribe request {Uuid}/{Service}")]
    private partial void LogUnsubscribeStart(string uuid, SonosService service);

}