using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Sonos.Cli.Commands
{
    public class ZonesCommand
    {
        public static Command GetCommand()
        {
            var command = new Command("zones", "Control your speaker groups")
            {
            };
            var listCommand = new Command("list", "List your speaker groups");
            listCommand.Handler = CommandHandler.Create<BaseOptions, IHost>(RunListZones);
            command.Add(listCommand);
            return command;
        }

        private static async Task RunListZones(BaseOptions options, IHost host)
        {
            var logger = host.Services.GetRequiredService<ILogger<ZonesCommand>>();
            logger.LogDebug("Execute List Zones {host}", options.Host);
            var sonos = host.CreateSonosDeviceWithOptions(options);
            CommandHelpers.WriteJson((await sonos.ZoneGroupTopologyService.GetZoneGroupState()).ParsedState.ZoneGroups);
        }
    }
}