
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Sonos.Base;
using Sonos.Web.SonosServices;

namespace Sonos.Web;

internal static class SonosDeviceApi
{
    public static void MapSonosApi(this WebApplication webApplication)
    {
        webApplication.MapGet("/getaa", AlbumArtProxy)
            .ExcludeFromDescription();
        //.WithSummary("Album art proxy")
        //.WithDescription("Proxy to get album art from Sonos devices")
        //.Produces(200)
        //.Produces(400)
        //.Produces(404);
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
            .WithGroupName("sonos-zones")
            .WithOpenApi();

        groups.MapGet("/", GetZones)
            .WithSummary("Get zones")
            .WithDescription("Get a list of all sonos groups")
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
            .WithGroupName("sonos-speakers")
            .WithOpenApi();

        controls.MapGet("/", GetSpeakers)
            .WithSummary("Get speakers")
            .WithDescription("Get all known speaker UUIDs")
            .Produces<IEnumerable<string>>(200); ;

        controls.MapPost("/{speakerId}/next", Next)
            .WithSummary(nameof(Next))
            .WithDescription("Play next song")
            .Produces<bool>(200);

        controls.MapPost("/{speakerId}/pause", Pause)
            .WithSummary(nameof(Pause))
            .WithDescription("Pause speaker playback")
            .Produces<bool>(200);

        controls.MapPost("/{speakerId}/play", Play)
            .WithSummary(nameof(Play))
            .WithDescription("Start speaker playback")
            .Produces<bool>(200);

        controls.MapPost("/{speakerId}/previous", Previous)
            .WithSummary(nameof(Previous))
            .WithDescription("Play previous song")
            .Produces<bool>(200);

        controls.MapGet("/{speakerId}/status", Status)
            .WithSummary(nameof(Status))
            .WithDescription("Player status")
            .Produces<Sonos.Base.Models.SonosEvent>(200);

        controls.MapPost("/{speakerId}/stop", Stop)
            .WithSummary(nameof(Stop))
            .WithDescription("Stop speaker playback")
            .Produces<bool>(200);

        controls.MapPost("/{speakerId}/toggle", TogglePlayback)
            .WithSummary("Toggle playback")
            .WithDescription("Toggle speaker playback")
            .Produces<bool>(200);

        controls.MapGet("/{speakerId}/volume", GetVolume)
            .WithSummary("Get volume")
            .WithDescription("Get current main channel volume")
            .Produces<int>(200);

        controls.MapPost("/{speakerId}/volume", SetVolume)
            .WithSummary("Set volume")
            .WithDescription("Set main channel volume")
            .Produces<bool>(200);
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

    private static IResult Status(string speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        
        return Results.Ok(device.Status);
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

    private static async Task<IResult> GetVolume(string speakerId, string? channel, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.RenderingControlService.GetVolume(channel ?? "Master", cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> SetVolume([FromRoute]string speakerId, [FromBody] int volume, [FromServices]SonosManager sonosManager, CancellationToken cancellationToken)
    {
        if (volume < 0 || volume > 100)
        {
            return Results.BadRequest("Volume must be between 0 and 100");
        }
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return SonosResults.DeviceNotFoundResult(speakerId);
        }
        var result = await device.RenderingControlService.SetVolume(new Base.Services.RenderingControlService.SetVolumeRequest { Channel = "Master", DesiredVolume = volume }, cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> AlbumArtProxy(HttpContext context, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        if (!context.Request.QueryString.HasValue)
        {
            return Results.Problem("Missing query string", statusCode: 400);
        }

        var device = sonosManager.GetSonosDevice(sonosManager.GetDeviceUuids().First());
        var result = await device!.GetAlbumArtAsync(context.Request.QueryString.Value!, cancellationToken);

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
            .WithSummary("Patch alarm")
            .WithDescription("Patch an existing alarm")
            .Produces<bool>(200);
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
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
}
