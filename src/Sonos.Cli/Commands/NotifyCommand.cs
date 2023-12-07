using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Sonos.Cli.Commands;

public class NotifyCommand
{
    public static Command GetCommand()
    {
        var command = new Command("notify", "Control a speaker")
        {
            new Argument<Uri>("sound", "Uri of the mp3 you want to play"),
            new Option<int>("--volume","The Sound volume")
        };
        command.Handler = CommandHandler.Create<NotifyCommandOptions, IHost>(Run);

        return command;
    }

    private static async Task Run(NotifyCommandOptions options, IHost host)
    {
        var logger = host.Services.GetRequiredService<ILogger<NotifyCommand>>();
        logger.LogDebug("Play notification on {ip} {sound}", options.Host, options.Sound);
        var sonos = host.CreateSonosDeviceWithOptions(options);

        await sonos.QueueNotificationAsync(new Base.NotificationOptions(options.Sound!, options.Volume));
    }

    public class NotifyCommandOptions : BaseOptions
    {
        public Uri? Sound { get; set; }
        public int Volume { get; set; } = 25;
    }
}