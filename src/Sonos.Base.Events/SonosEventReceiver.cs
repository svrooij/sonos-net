using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sonos.Base.Events.Models;
using Sonos.Base.Events.Parsing;
using Sonos.Base.Services;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace Sonos.Base.Events;

public class SonosEventReceiver : IHostedService, ISonosEventBus
{
    private readonly HttpClient httpClient;
    private readonly ILogger? logger;
    private readonly ConcurrentBag<SonosEventSubscription> subscriptions = new ConcurrentBag<SonosEventSubscription>();
    private WebApplication WebApplication;

    public SonosEventReceiver(ILogger<SonosEventReceiver> logger, HttpClient httpClient, IOptions<SonosEventReceiverOptions>? settings = null)
    {
        this.logger = logger;
        this.httpClient = httpClient;
        var options = settings?.Value ?? new SonosEventReceiverOptions();
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
        logger?.LogDebug("Subscribing to events for {uuid} from {service}, callback: {callbackUri}", uuid, service, callbackUri);
        var req = new HttpRequestMessage(new HttpMethod("SUBSCRIBE"), eventEndpoint);
        req.Headers.Add("callback", $"<{callbackUri}>");
        req.Headers.Add("NT", "upnp:event");
        req.Headers.Add("Timeout", $"Second-{timeoutInSeconds}");

        var response = await httpClient.SendAsync(req, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new SonosException("Event subscription failed");
        }

        var sid = response.Headers.TryGetValues("sid", out var values) ? values.FirstOrDefault() : null;
        if (sid is null || string.IsNullOrEmpty(sid))
        {
            throw new SonosException("Event subscription failed, no subscription ID");
        }

        this.subscriptions.Add(new SonosEventSubscription(uuid, service, eventEndpoint, sid, callback));

        logger?.LogInformation("Subscribed for events for {uuid} from {service}, sid: {sid}", uuid, service, sid);

        return true;
    }

    public Task<bool> Unsubscribe(string uuid, SonosService service, CancellationToken cancellationToken = default)
    {
        logger?.LogDebug("Unsubscribing to events for {uuid} from {service}", uuid, service);
        var subscription = GetSubscription(uuid, service);
        if (subscription is null)
        {
            return Task.FromResult(false);
        }

        return Unsubscribe(subscription.EventUri, subscription.Sid, cancellationToken);
    }

    public async Task<bool> UnsubscribeAll(CancellationToken cancellationToken = default)
    {
        var result = false;
        while (subscriptions.TryTake(out var sub))
        {
            try
            {
                result = await Unsubscribe(sub.EventUri, sub.Sid, cancellationToken) || result;
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Error unsubscribing from {eventUri}", sub.EventUri);
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
        logger?.LogDebug("{service} event received {@event}", service, serviceEvent);

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

    internal async Task<IResult> HandleEventAsync([FromRoute] string uuid, [FromRoute] Sonos.Base.Services.SonosService service, HttpContext ctx)
    {
        try
        {
            using var reader = new StreamReader(ctx.Request.Body);
            var body = await reader.ReadToEndAsync();
            logger?.LogDebug("Got XML: \r\n{xml}", body);
            using var stringReader = new StringReader(body);
            var serializer = new XmlSerializer(typeof(PropertyCollection));
            var result = (PropertyCollection?)serializer.Deserialize(stringReader);

            if (result is null || result.property is null) // result?.property?[0].LastChange is null
            {
                logger?.LogWarning("Event is empty");
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
                    //EmitParsedEvent(uuid, service, ZoneGroupTopologyEvent.FromDictionary(eventData));
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
            logger?.LogWarning(ex, "Error parsing event");
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
        // TODO Generate from settings!
        return new Uri(new Uri("http://192.168.200.127:6329/event/"), $"{uuid}/{service}").ToString();
    }
    private SonosEventSubscription? GetSubscription(string uuid, SonosService service) => subscriptions.FirstOrDefault(s => s.Uuid == uuid && s.Service == service);

    private async Task<bool> Unsubscribe(Uri eventUri, string sid, CancellationToken cancellationToken)
    {
        var req = new HttpRequestMessage(new HttpMethod("UNSUBSCRIBE"), eventUri);
        req.Headers.Add("SID", sid);

        var response = await httpClient.SendAsync(req, cancellationToken);

        return response.IsSuccessStatusCode;
    }
}