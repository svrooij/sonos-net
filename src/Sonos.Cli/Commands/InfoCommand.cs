using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Invocation;
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
        MusicClient = 5,
        DeviceDescription = 6,
    }

    public static Command GetCommand()
    {
        var command = new Command("info", "Show speaker info")
        {
            new Argument<SonosInfo>("info")
        };
        command.Handler = CommandHandler.Create<InfoCommandOptions, IHost, InvocationContext>(Run);
        return command;
    }

    private static async Task Run(InfoCommandOptions options, IHost host, InvocationContext ctx)
    {
        var logger = host.Services.GetRequiredService<ILogger<InfoCommand>>();
        logger.LogDebug("Execute info command {host} {info}", options.Host, options.Info);
        var sonos = host.CreateSonosDeviceWithOptions(options);
        var token = ctx.GetCancellationToken();
        switch (options.Info)
        {
            case SonosInfo.Position:
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetPositionInfoAsync(token));
                break;

            case SonosInfo.Transport:
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetTransportInfoAsync(token));
                break;
            //case SonosInfo.Volume:
            //CommandHelpers.WriteJson(await sonos.RenderingControlService.Async());
            //break;
            case SonosInfo.Media:
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetMediaInfoAsync(token));
                break;
            case SonosInfo.MusicClient:
                var serial = await sonos.SystemPropertiesService.GetStringAsync("R_TrialZPSerial", token);
                var householdId = (await sonos.DevicePropertiesService.GetHouseholdIDAsync(token)).CurrentHouseholdID;
                CommandHelpers.WriteJson(new { serial, householdId });
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetMediaInfoAsync(token));
                break;
            case SonosInfo.DeviceDescription:
                CommandHelpers.WriteJson(await sonos.GetDeviceDescriptionAsync());
                break;
        }
    }

    public class InfoCommandOptions : BaseOptions
    {
        public SonosInfo Info { get; set; }
    }
}
