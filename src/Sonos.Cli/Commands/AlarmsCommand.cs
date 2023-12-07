using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Sonos.Cli.Commands
{
    public class AlarmsCommand
    {
        public static Command GetCommand()
        {
            var command = new Command("alarms", "Manage Sonos alarms")
            {
            };
            command.Handler = CommandHandler.Create<BaseOptions, IHost>(Run);
            return command;
        }

        private static async Task Run(BaseOptions options, IHost host)
        {
            var logger = host.Services.GetRequiredService<ILogger<AlarmsCommand>>();
            logger.LogDebug("Execute alarms command {host}", options.Host);
            var sonos = host.CreateSonosDeviceWithOptions(options);
            var response = await sonos.AlarmClockService.ListAlarmsAsync();
            CommandHelpers.WriteJson(response.Alarms);
        }
    }
}