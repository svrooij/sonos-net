using Albatross.CommandLine;
using Microsoft.Extensions.Logging;

using Sonos.Base;

namespace Sonos.Cli.Commands;

public class AlarmsCommandHandler : IAsyncCommandHandler
{
    private readonly SonosOptions _options;
    private readonly ILogger _logger;
    private readonly ISonosServiceProvider _sonosServiceProvider;

    public AlarmsCommandHandler(SonosOptions options, ILogger<AlarmsCommandHandler> logger, ISonosServiceProvider sonosServiceProvider)
    {
        _options = options;
        _logger = logger;
        _sonosServiceProvider = sonosServiceProvider;
    }

    public async Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Execute alarms command {host}", _options.Host);
        var sonos = new SonosDevice(new SonosDeviceOptions(new Uri($"http://{_options.Host}:1400/"), _sonosServiceProvider));
        var response = await sonos.AlarmClockService.ListAlarms();
        CommandHelpers.WriteJson(response.Alarms);
        return 0;
    }
}