
using Sonos.Base;

namespace Sonos.Web.Worker;

public class SonosWorker : BackgroundService
{
    private readonly ILogger<SonosWorker> _logger;
    private readonly SonosManager _sonosManager;
    private readonly Uri _discoveryDevice;
    public SonosWorker(ILogger<SonosWorker> logger, SonosManager sonosManager, IConfiguration configuration)
    {
        _logger = logger;
        _sonosManager = sonosManager;
        _discoveryDevice = configuration.GetValue<Uri>("SONOS_HOST") ?? throw new ArgumentException("SONOS_HOST not in settings");
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Sonos Worker running at: {time}", DateTimeOffset.Now);

            await _sonosManager.InitializeFromDevice(_discoveryDevice, stoppingToken);
            // Delay ten minutes
            Task.Delay(60_000 * 10, stoppingToken).Wait(stoppingToken);
        }
    }
}
