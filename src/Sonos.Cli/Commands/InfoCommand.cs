using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Sonos.Cli.Commands;

public class InfoCommand
{
    public enum SonosInfo
    {
        Position = 1,
        Transport = 2,

        //Volume = 3,
        Media = 4,
    }

    public static Command GetCommand()
    {
        var command = new Command("info", "Show speaker info")
        {
            new Argument<SonosInfo>("info")
        };
        command.Handler = CommandHandler.Create<InfoCommandOptions, IHost>(Run);
        return command;
    }

    private static async Task Run(InfoCommandOptions options, IHost host)
    {
        var logger = host.Services.GetRequiredService<ILogger<InfoCommand>>();
        logger.LogDebug("Execute info command {host} {info}", options.Host, options.Info);
        var sonos = host.CreateSonosDeviceWithOptions(options);
        switch (options.Info)
        {
            case SonosInfo.Position:
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetPositionInfoAsync());
                break;

            case SonosInfo.Transport:
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetTransportInfoAsync());
                break;
            //case SonosInfo.Volume:
            //CommandHelpers.WriteJson(await sonos.RenderingControlService.Async());
            //break;
            case SonosInfo.Media:
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetMediaInfoAsync());
                break;
        }
    }

    public class InfoCommandOptions : BaseOptions
    {
        public SonosInfo Info { get; set; }
    }
}