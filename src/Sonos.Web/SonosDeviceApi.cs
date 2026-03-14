using System.ComponentModel;

using Microsoft.AspNetCore.Mvc;
using Sonos.Base;
using Sonos.Base.Music;
using Sonos.Web.Filters;
using Sonos.Web.SonosServices;
namespace Sonos.Web;


internal static class SonosDeviceApi
{
    public static void MapSonosApi(this WebApplication webApplication)
    {
        webApplication.MapGet("/getaa", AlbumArtProxy)
            .ExcludeFromDescription();
        webApplication.UsePathBase("/api");
        webApplication.MapSonosZones();
        webApplication.MapSonosControls();
        
        var sonosServiceGroups = webApplication.MapServicesApi();
        sonosServiceGroups.MapServiceExtensions();

        
    }

    private static void MapSonosZones(this WebApplication api)
    {
        var groups = api
            .MapGroup("/api/zones")
            .WithTags("Sonos Zones")
            .WithGroupName("sonos-zones");

        groups.MapGet("/", GetZones)
            .AddOpenApiOperationTransformer((operation, _, _) =>
            {
                operation.Summary = "Get zones";
                operation.Description = "Get a list of all sonos groups";
                operation.Responses ??= new();
                operation.Responses["200"].Description = "Success response with a list of sonos groups";
                return Task.CompletedTask;
            })
            .Produces<IEnumerable<Models.SonosGroup>>();
    }

    private static IResult GetZones(SonosManager sonosManager)
    {
        var groups = sonosManager
            .GetGroups()
            .Select(g => new Models.SonosGroup(g.GroupId,
                g.GroupName,
                new Models.SonosDevice(g.Coordinator.Uuid, g.Coordinator.Name),
                g.Members?.Select(m => new Models.SonosDevice(m.Uuid, m.Name)).ToArray())
        );


        return Results.Ok(groups);
    }

    private static void MapSonosControls(this WebApplication api)
    {
        var controls = api.MapGroup("/api/speakers")
            .WithTags("Sonos Speakers")
            .WithGroupName("sonos-speakers");

        controls.MapGet("/", GetSpeakers)
            .AddOpenApiOperationTransformer((operation, _, _) =>
            {
                operation.Summary = "Get all speakers";
                operation.Description = "Get a list of know speaker ids";
                operation.Responses ??= new();
                operation.Responses["200"].Description = "Success response with an array of speaker ids";
                return Task.CompletedTask;
            })
            .Produces<IEnumerable<string>>(200); ;



        controls.MapPost("/{speakerId}/next", Next)
            .AddOpenApiOperationTransformer((operation, _, _) =>
            {
                operation.Summary = "Next";
                operation.Description = "Go to next song";
                operation.Responses ??= new();
                operation.Responses["200"].Description = "Ok";
                return Task.CompletedTask;
            })
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapPost("/{speakerId}/notify", PlayNotification)
            .AddOpenApiOperationTransformer((operation, _, _) =>
            {
                operation.Summary = "Play notification";
                operation.Description = "Queue a notification and resume regular playback";
                operation.Responses ??= new();
                operation.Responses["200"].Description = "Ok";
                return Task.CompletedTask;
            })
            .Produces<bool>(200)
            .ProducesProblem(404);

        controls.MapPost("/{speakerId}/pause", Pause)
            .WithSummary(nameof(Pause))
            .WithDescription("Pause speaker playback")
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapPost("/{speakerId}/play", Play)
            .WithSummary(nameof(Play))
            .WithDescription("Start speaker playback")
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapPost("/{speakerId}/previous", Previous)
            .WithSummary(nameof(Previous))
            .WithDescription("Play previous song")
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapPost("/{speakerId}/settransporturi", SetTransportUri)
            .WithSummary(nameof(SetTransportUri))
            .WithDescription("Set the transport uri, with automatic metadata generation")
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapGet("/{speakerId}/status", Status)
            .WithSummary(nameof(Status))
            .WithDescription("Player status")
            .Produces<Sonos.Web.Models.SonosEvent>(200)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(404); // If status is null, return 204 No Content

        controls.MapPost("/{speakerId}/stop", Stop)
            .WithSummary(nameof(Stop))
            .WithDescription("Stop speaker playback")
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapPost("/{speakerId}/switchtolinein", SwitchToLineIn)
            .WithSummary(nameof(SwitchToLineIn))
            .WithDescription("Switch to line in (only supported devices)")
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapPost("/{speakerId}/switchtostream", SwitchToStream)
            .WithSummary(nameof(SwitchToStream))
            .WithDescription("Try to switch to a stream from a service")
            .Produces<bool>(204)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapPost("/{speakerId}/switchtoqueue", SwitchToQueue)
            .WithSummary(nameof(SwitchToQueue))
            .WithDescription("Switch to queue")
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapPost("/{speakerId}/switchtotv", SwitchToTv)
            .WithSummary(nameof(SwitchToTv))
            .WithDescription("Switch to tv input (only supported devices)")
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();



        controls.MapPost("/{speakerId}/toggle", TogglePlayback)
            .WithSummary("Toggle playback")
            .WithDescription("Toggle speaker playback")
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapGet("/{speakerId}/volume", VolumeGet)
            .WithSummary("Volume Get")
            .WithDescription("Get current main channel volume")
            .Produces<int>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();

        controls.MapPost("/{speakerId}/volume", VolumeSet)
            .WithSummary("Volume set")
            .WithDescription("Set main channel volume")
            .Produces<bool>(200)
            .ProducesValidationProblem(400)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();
    }

    private static IResult GetSpeakers(SonosManager sonosManager)
    {
        var devices = sonosManager.GetDeviceUuids();
        return Results.Ok(devices);
    }

    private static async Task<IResult> Next(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.Next(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> Pause(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.Pause(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> Previous(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.Previous(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> Play(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.Play(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> SetTransportUri(string speakerId, [FromBody, Description("Mandatory body")] SetTransportUriRequest setTransportUriRequest, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.SetTransportUri(setTransportUriRequest, cancellationToken);
        return Results.Ok(result);
    }


    private static IResult Status(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        if (device.Status is null)
        {
            // This happens if the status is not received yet, for example right after startup or if the event subscription is not working.
            // In this case, return 204 No Content to indicate that the request was successful but there is no status available yet.
            return Results.NoContent();
        }
        var webStatus = Models.SonosEvent.FromBaseEvent(device.Status);
        return Results.Ok(webStatus);
    }

    private static async Task<IResult> Stop(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.Stop(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> SwitchToLineIn(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.SwitchToLineIn(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> SwitchToStream([FromRoute]string speakerId, [FromBody]Models.SwitchToStreamRequest switchToStreamRequest, [FromServices]SonosManager sonosManager, [FromServices]SonosMusicManager sonosMusicManager, CancellationToken cancellationToken = default)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var mediaPlaybackInfo = await sonosMusicManager.GetMediaPlaybackInformationAsync((ushort)switchToStreamRequest.ServiceId, switchToStreamRequest.MediaId, cancellationToken);
        await device.AVTransportService.SetAVTransportURI(new Base.Services.AVTransportService.SetAVTransportURIRequest
        {
            CurrentURI = mediaPlaybackInfo?.TrackUri!,
            CurrentURIMetaDataObject = new Base.Metadata.Didl(mediaPlaybackInfo!.Metadata)
        }, cancellationToken);
        if (switchToStreamRequest.Play)
        {
            await device.Play(cancellationToken);
        }
        return Results.NoContent();
    }

    private static async Task<IResult> SwitchToQueue(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.SwitchToQueue(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> SwitchToTv(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.SwitchToSpdif(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> TogglePlayback(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.TogglePlayback(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> VolumeGet(string speakerId, string? channel, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.RenderingControlService.GetVolume(channel ?? "Master", cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> VolumeSet([FromRoute]string speakerId, [FromBody] int volume, [FromServices]SonosManager sonosManager, CancellationToken cancellationToken)
    {
        if (volume < 0 || volume > 100)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]> { ["volume"] = new[] { "The volume must be between 0 and 100" } });
        }
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.RenderingControlService.SetVolume(new Base.Services.RenderingControlService.SetVolumeRequest { Channel = "Master", DesiredVolume = volume }, cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> PlayNotification([FromRoute] string speakerId, [FromBody] Sonos.Base.NotificationOptions notificationOptions, [FromServices] SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.QueueNotification(notificationOptions, cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> AlbumArtProxy(HttpContext context, [FromServices]SonosManager sonosManager, [FromServices]HttpClient httpClient, CancellationToken cancellationToken)
    {
        if (!context.Request.QueryString.HasValue)
        {
            return Results.Problem("Missing query string", statusCode: 400);
        }

        var device = sonosManager.GetSonosDevice(sonosManager.GetDeviceUuids().First());
        var albumArtUri = device!.GenerateAlbumArtUrl(context.Request.QueryString.Value!);
        var result = await httpClient.GetAsync(albumArtUri, cancellationToken);

        if (result.IsSuccessStatusCode)
        {
            // Set cache header for 168 hour (7 days)
            context.Response.Headers.CacheControl = "public,max-age=604800";
            var contentType = result.Content.Headers.ContentType?.MediaType ?? "image/jpeg";
            var content = await result.Content.ReadAsByteArrayAsync(cancellationToken);
            
            return Results.File(content, contentType);
        }
        else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return Results.NotFound();
        }
        else
        {
            return Results.Problem($"Error retrieving album art: {result.ReasonPhrase}", statusCode: (int)result.StatusCode);
        }
    }

    private static void MapServiceExtensions(this Dictionary<Sonos.Base.Services.SonosService, RouteGroupBuilder> groups)
    {
        groups[Sonos.Base.Services.SonosService.AlarmClock].MapPatch("alarms/{alarmId}", PatchAlarm)
            .AddOpenApiOperationTransformer((operation, _, _) =>
            {
                operation.Summary = "Patch Alarm";
                operation.Description = "Patch some properties of an alarm, not generated.";
                operation.Responses ??= new();
                operation.Responses["200"].Description = "Successfully patched an alarm";
                return Task.CompletedTask;
            })
            .Produces<bool>(200)
            .ProducesProblem(404)
            .AddSonosServiceExceptionFilter();
    }

    private static async Task<IResult> PatchAlarm(
        [FromRoute] string speakerId,
        [FromRoute] int alarmId,
        [FromBody] Sonos.Base.Services.AlarmClockService.PatchAlarmRequest patchAlarmRequest,
        [FromServices] SonosManager sonosManager,
        CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        if (alarmId < 0)
        {
            return Results.BadRequest("Invalid alarm ID");
        }
        try
        {
            patchAlarmRequest.ID = alarmId;
            var result = await device.AlarmClockService.PatchAlarm(patchAlarmRequest, cancellationToken);
            return Results.Ok(result);
        }
        catch (SonosException ex) when (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
        {
            return Results.Problem(ex.Message, statusCode: 404);
        }
    }
}
