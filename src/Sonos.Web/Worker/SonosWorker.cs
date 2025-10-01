
using Microsoft.AspNetCore.SignalR;

using Sonos.Base;

namespace Sonos.Web.Worker;

/// <summary>
/// Sonos Worker is responsible for managing Sonos device subscriptions and emitting status updates via SignalR.
/// </summary>
public class SonosWorker : BackgroundService
{
    private readonly ILogger<SonosWorker> _logger;
    private readonly SonosManager _sonosManager;
    private readonly Uri _discoveryDevice;
    private readonly IHubContext<Hubs.PlayerStatusHub, Hubs.IPlayerStatusClient> _hubContext;
    private bool subscribed = false;
    /// <inheritdoc/>
    public SonosWorker(ILogger<SonosWorker> logger, SonosManager sonosManager, IConfiguration configuration, IHubContext<Hubs.PlayerStatusHub, Hubs.IPlayerStatusClient> hubContext)
    {
        _logger = logger;
        _sonosManager = sonosManager;
        _discoveryDevice = configuration.GetValue<Uri>("SONOS_HOST") ?? throw new ArgumentException("SONOS_HOST not in settings");
        _hubContext = hubContext;
    }

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _sonosManager.InitializeFromDevice(_discoveryDevice, stoppingToken);
        await Task.Delay(10_000, stoppingToken); // Wait 10 seconds to allow initial subscriptions
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Sonos Worker renew subscriptions: {time}", DateTimeOffset.Now);
            await _sonosManager.SubscribeToTopologyChanges(stoppingToken);

            var devices = _sonosManager.GetDeviceUuids();
            List<Task> tasks = new ();
            foreach (var item in devices)
            {
                var device = _sonosManager.GetSonosDevice(item);
                if (device != null)
                {
                    //tasks.Add(device.SubscribeToEvents(stoppingToken));
                    if (!subscribed)
                    {
                        device.OnStatusChanged += async (s, e) =>
                        {
                            await EmitStatusAsync(device.Uuid, e);
                        };
                    }
                }

                
            }

            subscribed = true;
            await Task.WhenAll(tasks);


            // Delay ten minutes
            await Task.Delay(60_000 * 10, stoppingToken);
        }

    }

    private async Task EmitStatusAsync(string uuid, Sonos.Base.Models.SonosEvent sonosEvent)
    {
        _logger.LogDebug("Emitting status for {uuid}: {@event}", uuid, sonosEvent);
        // Emit the status to all connected clients in the group identified by the device UUID
        // This ensures that only clients interested in this specific device receive the update, to keep it snappy.
        await _hubContext.Clients.Group(uuid).PlayerStatusChanged(uuid, sonosEvent);

    }


}
