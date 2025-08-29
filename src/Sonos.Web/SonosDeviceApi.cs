using System.Text.RegularExpressions;

using Sonos.Base;

namespace Sonos.Web;

public static class SonosDeviceApi
{
    public static void MapSonosApi(this WebApplication webApplication)
    {

        webApplication.MapSonosZones();
        webApplication.MapSonosControls();
    }

    private static void MapSonosZones(this WebApplication api)
    {
        var groups = api
            .MapGroup("/api/zones")
            .WithTags("Sonos Zones")
            .WithGroupName("sonos-zones");

        groups.MapGet("/", GetZones)
            .WithName(nameof(GetZones))
            .Produces<IEnumerable<Models.SonosGroup>>()
            .WithOpenApi(op =>
            {
                op.Summary = "Sonos zones";
                op.Description = "Get all sonos zones";
                return op;
            });
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

        controls.MapGet("/", GetDevices)
            .WithName(nameof(GetDevices))
            .Produces<IEnumerable<string>>(200)
            .WithOpenApi(op =>
            {
                op.Summary = "Sonos speakers";
                op.Description = "Get all sonos speakers";
                return op;
            });

        controls.MapPost("/{speakerId}/next", Next)
            .WithName(nameof(Next))
            .Produces<bool>(200)
            .WithOpenApi(op =>
            {
                op.Summary = "Next song";
                op.Description = "Play next song";
                return op;
            });

        controls.MapPost("/{speakerId}/pause", Pause)
            .WithName(nameof(Pause))
            .Produces<bool>(200)
            .WithOpenApi(op =>
            {
                op.Summary = "Pause playback";
                op.Description = "Pause speaker playback";
                return op;
            });

        controls.MapPost("/{speakerId}/play", Play)
            .WithName(nameof(Play))
            .Produces<bool>(200)
            .WithOpenApi(op =>
            {
                op.Summary = "Start playback";
                op.Description = "Start speaker playback";
                return op;
            });

        controls.MapPost("/{speakerId}/previous", Previous)
            .WithName(nameof(Previous))
            .Produces<bool>(200)
            .WithOpenApi(op =>
            {
                op.Summary = "Previous song";
                op.Description = "Previous song";
                return op;
            });


        controls.MapPost("/{speakerId}/toggle", TogglePlayback)
            .WithName(nameof(TogglePlayback))
            .Produces<bool>(200)
            .WithOpenApi(op =>
            {
                op.Summary = "Toggle playback";
                op.Description = "Toggle speaker playback";
                return op;
            });

        controls.MapPost("/{speakerId}/stop", Stop)
            .WithName(nameof(Stop))
            .Produces<bool>(200)
            .WithOpenApi(op =>
            {
                op.Summary = "Stop playback";
                op.Description = "Stop speaker playback";
                return op;
            });
    }


    private static IResult GetDevices(SonosManager sonosManager)
    {
        var devices = sonosManager.GetDeviceUuids();
        return Results.Ok(devices);
    }

    private static async Task<IResult> Next(string? speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return Results.NotFound();
        }
        var result = await device.Next(cancellationToken);
        return Results.Ok(result);
    }


    private static async Task<IResult> Pause(string? speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return Results.NotFound();
        }
        var result = await device.Pause(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> Previous(string? speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return Results.NotFound();
        }
        var result = await device.Previous(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> Play(string? speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return Results.NotFound();
        }
        var result = await device.Play(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> TogglePlayback(string? speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return Results.NotFound();
        }
        var result = await device.TogglePlayback(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> Stop(string? speakerId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var device = sonosManager.GetSonosDevice(speakerId!);
        if (device is null)
        {
            return Results.NotFound();
        }
        var result = await device.Stop(cancellationToken);
        return Results.Ok(result);
    }


}
