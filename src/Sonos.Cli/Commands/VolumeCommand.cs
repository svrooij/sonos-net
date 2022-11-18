using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Sonos.Cli.Commands;

public class VolumeCommand
{
    public static Command GetCommand()
    {
        var command = new Command("volume", "Show or set Sonos Volume")
        {
            new Option<int?>("--newVolume", "Set the volume"),
            new Option<string>("--channel", "Get/set volume for different channel than 'Master'"),
        };
        command.Handler = CommandHandler.Create<VolumeCommandOptions, IHost>(Run);
        return command;
    }

    private static async Task Run(VolumeCommandOptions options, IHost host)
    {
        var logger = host.Services.GetRequiredService<ILogger<VolumeCommand>>();
        logger.LogDebug("Execute volume command {host}", options.Host);
        var sonos = host.CreateSonosDeviceWithOptions(options);
        if (options.NewVolume.HasValue)
        {
            await sonos.RenderingControlService.SetVolume(new Base.Services.RenderingControlService.SetVolumeRequest { Channel = options.Channel, DesiredVolume = options.NewVolume.Value, InstanceID = 0 });
            CommandHelpers.WriteJson(options.NewVolume.Value);
        }
        else
        {
            CommandHelpers.WriteJson(await sonos.RenderingControlService.GetVolume(options.Channel));
        }
    }

    public class VolumeCommandOptions : BaseOptions
    {
        public int? NewVolume { get; set; }
        public string Channel { get; set; } = "Master";
    }
}