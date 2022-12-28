using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Sonos.Base.Events.Http.Models;
using Sonos.Base.Events.Http.Parsing;
using Sonos.Base.Services;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace Sonos.Base.Events.Http;

public partial class SonosEventReceiver : IHostedService, ISonosEventBus
{
    private readonly HttpClient httpClient;
    private readonly ILogger logger;
    private readonly SonosEventReceiverOptions options;
    private readonly ConcurrentBag<SonosEventSubscription> subscriptions = new ConcurrentBag<SonosEventSubscription>();
    private WebApplication WebApplication;

    public SonosEventReceiver(HttpClient httpClient, ILogger<SonosEventReceiver>? logger = null, IOptions<SonosEventReceiverOptions>? settings = null)
    {
        this.logger = logger ?? (ILogger)NullLogger.Instance;
        this.httpClient = httpClient;
        options = settings?.Value ?? new SonosEventReceiverOptions();
        WebApplication = CreateWebApplication(options.Port);
    }

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

        return response.IsSuccessStatusCode;
    }

    public Task RunAsync(CancellationToken cancellationToken)
    {
        return WebApplication.RunAsync(token: cancellationToken);
    }

    public void RunBlocked()
    {
        WebApplication.Run();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return WebApplication.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await UnsubscribeAll(cancellationToken);
        await WebApplication.StopAsync(cancellationToken);
    }

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

        this.subscriptions.Add(new SonosEventSubscription(uuid, service, eventEndpoint, sid, callback));

        LogSubscribingFinished(uuid, service, sid);

        return true;
    }

    public async Task<bool> Unsubscribe(string uuid, SonosService service, CancellationToken cancellationToken = default)
    {
        LogUnsubscribeStart(uuid, service);
        var subscription = GetSubscription(uuid, service);
        if (subscription is null)
        {
            LogSubscriptionNotFound(uuid, service);
            return false;
        }

        var result = await Unsubscribe(subscription.EventUri, subscription.Sid, cancellationToken);
        LogUnsubscribeFinished(uuid, service, result);
        return result;
    }

    public async Task<bool> UnsubscribeAll(CancellationToken cancellationToken = default)
    {
        LogUnsubscribeAllStart();
        var result = false;
        while (subscriptions.TryTake(out var sub))
        {
            try
            {
                result = await Unsubscribe(sub.EventUri, sub.Sid, cancellationToken) || result;
            }
            catch (Exception ex)
            {
                LogUnsubscribeFailed(ex, sub.EventUri, sub.Sid);
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
        logger?.LogDebug("{service} event received {@event}", service, actualEvent);
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

    private WebApplication CreateWebApplication(int port)
    {
        //var builder = WebApplication.CreateBuilder(new WebApplicationOptions {
        //    Args = new[]
        //    {
        //        $"--urls=http://*:{port}/"
        //    }
        //});
        var builder = WebApplication.CreateBuilder();

        //builder.Logging.AddJsonConsole();
        //builder.Logging.AddConsole();
        var app = builder.Build();
        app.Urls.Add($"http://*:{port}/");
        //app.Urls.Add($"http://0.0.0.0:{port}/");
        //app.Urls.Add($"http://localhost:{port}/");

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

    private SonosEventSubscription? GetSubscription(string uuid, SonosService service) => subscriptions.FirstOrDefault(s => s.Uuid == uuid && s.Service == service);

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

    [LoggerMessage(EventId = 140, Level = LogLevel.Debug, Message = "Unsubscribe all services")]
    private partial void LogUnsubscribeAllStart();

    [LoggerMessage(EventId = 141, Level = LogLevel.Warning, Message = "Unsubscribing failed {EventUri} {Sid}")]
    private partial void LogUnsubscribeFailed(Exception e, Uri eventUri, string sid);

    [LoggerMessage(EventId = 122, Level = LogLevel.Information, Message = "Unsubscribe finished {Uuid}/{Service} success {Success}")]
    private partial void LogUnsubscribeFinished(string uuid, SonosService service, bool success);

    [LoggerMessage(EventId = 120, Level = LogLevel.Debug, Message = "Unsubscribe request {Uuid}/{Service}")]
    private partial void LogUnsubscribeStart(string uuid, SonosService service);
    private async Task<bool> Unsubscribe(Uri eventUri, string sid, CancellationToken cancellationToken)
    {
        var req = new HttpRequestMessage(new HttpMethod("UNSUBSCRIBE"), eventUri);
        req.Headers.Add("SID", sid);

        var response = await httpClient.SendAsync(req, cancellationToken);

        return response.IsSuccessStatusCode;
    }
}