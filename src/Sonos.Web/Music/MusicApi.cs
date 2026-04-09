using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

using Sonos.Base;
using Sonos.Base.Music;

namespace Sonos.Web.Music;

internal static class MusicApi
{
    internal static WebApplication MapMusicServices(this WebApplication app)
    {
        var music = app
            .MapGroup("/api/musicservices")
            .WithTags("Music Services")
            .WithGroupName("music-services");

        music.MapGet("/", GetMusicServices)
            .WithSummary("Get all music services")
            .WithDescription("Get a cached list of all sonos music services")
            .Produces<IEnumerable<Sonos.Base.Services.MusicServicesService.MusicService>>();

        music.MapGet("/enabled", GetEnabledMusicServices)
            .WithSummary("Get enabled music services")
            .WithDescription("Get a list off all music services that are enabled in Sonos Web")
            .Produces<IEnumerable<Sonos.Base.Services.MusicServicesService.MusicService>>();

        music.MapGet("{musicServiceId:int}", GetMusicServiceById)
            .WithSummary("Get a music service")
            .WithDescription("Get a music service by it's ID")
            .Produces<Sonos.Base.Services.MusicServicesService.MusicService>();

        music.MapGet("{musicServiceId:int}/trigger-login", TriggerMusicServiceLoginById)
            .WithSummary("Get login information")
            .WithDescription("Get login information from music service")
            .Produces<Sonos.Base.Music.DeviceLink>();

        music.MapGet("{musicServiceId:int}/complete-login", CompleteMusicServiceLoginById)
            .WithSummary("Complete login")
            .WithDescription("Complete music service login")
            .Produces<Sonos.Base.Music.DeviceAuthResponse>();

        music.MapGet("{musicServiceId:int}/browse", GetMusicServiceMetadata)
            .WithSummary("Get metadata")
            .WithDescription("Get metadata or browse music service")
            .Produces<Sonos.Base.Music.MediaList>();

        music.MapGet("{musicServiceId:int}/media-metadata", GetMusicServiceMediaMetadata)
            .AddOpenApiOperationTransformer((operation, _, _) =>
            {
                operation.Summary = "Media metadata";
                operation.Description = "Get the media metadata for a specific item";
                operation.Responses ??= new(); // Not sure if this is needed, since the `Responses` are required.
                operation.Responses["200"].Description = "Success response for media item";
                return Task.CompletedTask;

            })
            .Produces<Sonos.Base.Music.MediaMetadata>();

        return app;

    }

    private static async Task<IResult> GetMusicServices(SonosMusicManager sonosMusicManager, CancellationToken cancellationToken)
    {
        var services = await sonosMusicManager.GetAllMusicServicesAsync(cancellationToken);

        if (services == null)
        {
            return Results.NotFound("No music services found");
        }

        return Results.Ok(services);
    }

    private async static Task<IResult> GetMusicServiceById(int musicServiceId, SonosMusicManager sonosMusicManager, CancellationToken cancellationToken)
    {
        var services = await sonosMusicManager.GetAllMusicServicesAsync(cancellationToken);
        if (services == null)
        {
            return Results.NotFound("No music services found");
        }
        var service = services.FirstOrDefault(s => s.Id == musicServiceId);
        if (service == null)
        {
            return Results.NotFound($"Music service with ID {musicServiceId} not found");
        }
        return Results.Ok(service);
    }

    

    private static async Task<IResult> GetEnabledMusicServices(SonosMusicManager sonosMusicManager, CancellationToken cancellationToken)
    {
        var services = await sonosMusicManager.GetEnabledMusicServicesAsync(cancellationToken);
        if (services == null)
        {
            return Results.NotFound("No enabled music services found");
        }
        return Results.Ok(services);
    }

    private async static Task<IResult> TriggerMusicServiceLoginById(ushort musicServiceId, SonosMusicManager sonosMusicManager, CancellationToken cancellationToken)
    {
        var client = await sonosMusicManager.GetClientForServiceAsync(musicServiceId, cancellationToken);
        if (client == null)
        {
            return Results.NotFound($"Music service with ID {musicServiceId} not found");
        }

        var login = await client.GetLoginLinkAsync(cancellationToken);
        if (login == null)
        {
            return Results.BadRequest($"Music service with ID {musicServiceId} does not support login");
        }
        return Results.Ok(login);
    }

    private async static Task<IResult> CompleteMusicServiceLoginById(ushort musicServiceId, SonosMusicManager sonosMusicManager, [FromQuery]string linkCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(linkCode))
        {
            return Results.BadRequest("Link code is required");
        }
        var client = await sonosMusicManager.GetClientForServiceAsync(musicServiceId, cancellationToken);
        if (client == null)
        {
            return Results.NotFound($"Music service with ID {musicServiceId} not found");
        }

        var login = await client.GetDeviceAuthTokenAsync(linkCode, cancellationToken);
        return Results.Ok(login);
    }

    private async static Task<IResult> GetMusicServiceMetadata(ushort musicServiceId, SonosMusicManager sonosMusicManager, [FromQuery] string id = "root", [FromQuery] int index = 0, [FromQuery] int count = 100,  CancellationToken cancellationToken = default)
    {
        
        var client = await sonosMusicManager.GetClientForServiceAsync(musicServiceId, cancellationToken);
        if (client == null)
        {
            return Results.NotFound($"Music service with ID {id} not found");
        }

        var items = await client.GetMetadataAsync(id, index, count, cancellationToken: cancellationToken);
        return Results.Ok(items);
    }

    private async static Task<IResult> GetMusicServiceMediaMetadata(ushort musicServiceId, SonosMusicManager sonosMusicManager, [FromQuery] string id, CancellationToken cancellationToken)
    {
        var client = await sonosMusicManager.GetClientForServiceAsync(musicServiceId, cancellationToken);
        if (client == null)
        {
            return Results.NotFound($"Music service with ID {id} not found");
        }

        var result = await client.GetMediaMetadataAsync(id, cancellationToken);
        return Results.Ok(result.GetMediaMetadataResult);
    }
}
