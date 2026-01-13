using Albatross.CommandLine;
using Microsoft.Extensions.Logging;

using Sonos.Base;

namespace Sonos.Cli.Commands;

public class ZonesCommandHandler : IAsyncCommandHandler
{
    private readonly SonosOptions _options;
    private readonly ILogger<ZonesCommandHandler> _logger;
    private readonly ISonosServiceProvider _sonosServiceProvider;

    public ZonesCommandHandler(SonosOptions options, ILogger<ZonesCommandHandler> logger, ISonosServiceProvider sonosServiceProvider)
    {
        _options = options;
        _logger = logger;
        _sonosServiceProvider = sonosServiceProvider;
    }

    public async Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Execute List Zones {host}", _options.Host);
        var sonos = new SonosDevice(new SonosDeviceOptions(new Uri($"http://{_options.Host}:1400/"), _sonosServiceProvider));
        CommandHelpers.WriteJson((await sonos.ZoneGroupTopologyService.GetZoneGroupState(cancellationToken))?.ParsedState?.ZoneGroups);
        return 0;
    }
}