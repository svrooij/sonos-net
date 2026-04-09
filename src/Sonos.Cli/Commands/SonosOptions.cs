using Albatross.CommandLine.Annotations;

namespace Sonos.Cli.Commands;

[Verb<AlarmsCommandHandler>("alarms", Description = "List all alarms in the sonos speaker")]
[Verb<ZonesCommandHandler>("zones", Description = "List your speaker groups")]
public record class SonosOptions
{
    [Argument(Description = "Sonos host address")]
    public required string Host { get; set; }
}