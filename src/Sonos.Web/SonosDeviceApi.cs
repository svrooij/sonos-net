using Sonos.Base;

namespace Sonos.Web;

public static class SonosDeviceApi
{
    public static void MapSonosApi(this WebApplication webApplication)
    {
        
        webApplication.MapSonosGroups();
    }

    private static void MapSonosGroups(this WebApplication api)
    {
        var groups = api
            .MapGroup("/api/groups")
            .WithTags("Sonos Groups")
            .WithGroupName("sonos-groups");

        groups.MapGet("/", GetGroups)
            .WithName(nameof(GetGroups))
            .Produces<IEnumerable<Models.SonosGroup>>()
            .WithOpenApi(op =>
            {
                op.Summary = "Sonos zones";
                op.Description = "Get all sonos zones";
                return op;
            });

        groups.MapPost("/{groupId}/toggle", TogglePlayback)
            .WithName(nameof(TogglePlayback))
            .Produces(200)
            .WithOpenApi(op =>
            {
                op.Summary = "Toggle playback";
                op.Description = "Toggle zone playback";
                return op;
            });
    }

    private static IResult GetGroups(SonosManager sonosManager)
    {
        var groups = sonosManager
            .GetGroupsDictionary()
            .Select(kv => new Models.SonosGroup(kv.Key, kv.Value.GroupName, kv.Value.Coordinator.DeviceName, kv.Value.Members?.Select(m => m.DeviceName).ToArray()));


        return Results.Ok(groups);
    }

    private static async Task<IResult> TogglePlayback(string? groupId, SonosManager sonosManager, CancellationToken cancellationToken)
    {
        var fixedId = groupId!.Replace("-", ":");
        var group = sonosManager.GetGroupById(fixedId);
        if (group is null)
        {
            return Results.NotFound();
        }
        var transportState = await group.Coordinator.AVTransportService.GetTransportInfo(cancellationToken);
        if (transportState.CurrentTransportState == "PLAYING")
        {
            await group.Coordinator.Pause(cancellationToken);
        } else
        {
            await group.Coordinator.Play(cancellationToken);
        }
        return Results.Ok();
    }
}
