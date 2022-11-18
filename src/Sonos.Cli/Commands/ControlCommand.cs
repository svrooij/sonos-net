using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Sonos.Cli.Commands;

public class ControlCommand
{
    public enum ControlAction
    {
        Stop = 0,
        Play = 1,
        Pause = 2,
        Next = 3,
        Previous = 4,
    }

    public static Command GetCommand()
    {
        var command = new Command("control", "Control a speaker")
        {
            new Argument<ControlAction>("action")
        };
        command.Handler = CommandHandler.Create<ControlCommandOptions, IHost>(Run);

        return command;
    }

    private static async Task Run(ControlCommandOptions options, IHost host)
    {
        var logger = host.Services.GetRequiredService<ILogger<ControlCommand>>();
        logger.LogDebug("Execute control command {ip} {action}", options.Host, options.Action);
        var sonos = host.CreateSonosDeviceWithOptions(options);
        switch (options.Action)
        {
            case ControlAction.Stop:
                await sonos.Stop();
                break;

            case ControlAction.Play:
                await sonos.Play();
                break;

            case ControlAction.Pause:
                await sonos.Pause();
                break;

            case ControlAction.Next:
                await sonos.Next();
                break;

            case ControlAction.Previous:
                await sonos.Previous();
                break;
        }
    }

    public class ControlCommandOptions : BaseOptions
    {
        public ControlAction Action { get; set; }
    }
}