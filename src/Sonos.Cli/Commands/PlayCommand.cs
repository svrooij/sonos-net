using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sonos.Base.Music;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;

namespace Sonos.Cli.Commands;

public class PlayCommand
{
    //public enum MusicCommandEnum
    //{
    //    Position = 1,
    //    Transport = 2,

    //    //Volume = 3,
    //    Media = 4,
    //    MusicClient = 5,
    //}

    public static Command GetCommand()
    {
        var playlistCommand = new Command("playlist", "Queue playlist from service") {
            new Argument<int>("serviceId", "ID of the service you wish stream an item from"),
            new Argument<string>("input", "ID of the playlist you wish to start")
        };
        playlistCommand.Handler = CommandHandler.Create<PlayCommandOptions, IHost, InvocationContext>(RunPlaylist);


        var streamCommand = new Command("stream", "Play stream from service") {
            new Argument<int>("serviceId", "ID of the service you wish stream an item from"),
            new Argument<string>("input", "ID of the stream you wish to start")
        };
        streamCommand.Handler = CommandHandler.Create<PlayCommandOptions, IHost, InvocationContext>(RunStream);

        //var loginCommand = new Command("login", "Login to music service") { new Argument<int>("serviceId", "ID of the service you wish to login to")};
        //loginCommand.Handler = CommandHandler.Create<MusicCommandOptions, IHost, InvocationContext>(RunLogin);

        //var browseCommand = new Command("browse", "Browse a music service") {
        //    new Argument<int>("serviceId", "ID of the service you wish to login to"),
        //    new Argument<string>("input",() => "root", "What do you want to browse"),
        //    new Option<bool?>("--json", "Output result as json")
        //};
        //browseCommand.Handler = CommandHandler.Create<MusicCommandOptions, IHost, InvocationContext>(RunBrowse);

        var command = new Command("play", "Play some music")
        {
            streamCommand,
            playlistCommand,
        };
        
        //command.Handler = CommandHandler.Create<MusicCommandOptions, IHost, InvocationContext>(Run);
        return command;
    }



    private static async Task RunStream(PlayCommandOptions options, IHost host, InvocationContext ctx)
    {
        var logger = host.Services.GetRequiredService<ILogger<MusicCommand>>();
        logger.LogDebug("Play stream {input} from {serviceId} on {host}", options.Input, options.ServiceId, options.Host);
        var sonos = host.CreateSonosDeviceWithOptions(options);
        var token = ctx.GetCancellationToken();

        await sonos.AVTransportService.SetAVTransportURIToStreamItem(options.ServiceId, options.Input ?? "", cancellationToken:  token);

        await Task.Delay(200, token);
        await sonos.AVTransportService.Play(token);
    }

    private static async Task RunPlaylist(PlayCommandOptions options, IHost host, InvocationContext ctx)
    {
        var logger = host.Services.GetRequiredService<ILogger<MusicCommand>>();
        logger.LogDebug("Play stream {input} from {serviceId} on {host}", options.Input, options.ServiceId, options.Host);
        var sonos = host.CreateSonosDeviceWithOptions(options);
        var token = ctx.GetCancellationToken();

        //await sonos.QueueService.RemoveAllTracks(new Base.Services.QueueService.RemoveAllTracksRequest { QueueID = 0, UpdateID = 0 }, token);
        await sonos.AVTransportService.RemoveAllTracksFromQueue(token);
        await sonos.AVTransportService.AddURIToQueuePlaylist(options.ServiceId, options.Input, cancellationToken: token);

        await Task.Delay(200, token);
        await sonos.AVTransportService.Play(token);
    }


    public class PlayCommandOptions : BaseOptions
    {
        //public MusicCommandEnum Action { get; set; }

        public int ServiceId { get; set; } = -1;
        public string? Input { get; set; }
    }
}