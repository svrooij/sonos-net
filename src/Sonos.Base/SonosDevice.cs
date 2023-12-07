/*
 * Sonos-net
 *
 * Repository https://github.com/svrooij/sonos-net
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Sonos.Base;

using Microsoft.Extensions.Logging;
using Sonos.Base.Services;
using Sonos.Base.Internal;
using System.Threading.Tasks;

public partial class SonosDevice : IDisposable, IAsyncDisposable
{
    private SonosDevice? coordinator;
    private SonosWebSocket? SonosWebSocket;
    protected readonly ILogger? logger;

    public SonosDevice Coordinator
    {
        get
        {
            return coordinator ?? this;
        }
        init
        {
            coordinator = value;
        }
    }

    public string DeviceName { get; private set; }
    public string GroupName { get; private set; }
    public string Uuid { get; private set; }

    public SonosDevice(SonosDeviceOptions options)
    {
        ServiceOptions = options;
        coordinator = options.Coordinator;
        DeviceName = options.DeviceName ?? "Not specified";
        Uuid = options.Uuid ?? Guid.NewGuid().ToString();
        logger = options.ServiceProvider.CreateLogger<SonosDevice>();
    }

    internal SonosServiceOptions ServiceOptions { get; private set; }
    
    public async Task LoadUuidAsync(CancellationToken cancellationToken = default)
    {
        if (!Uuid.StartsWith("RINCON"))
        {
            var attributes = await DevicePropertiesService.GetZoneInfoAsync(cancellationToken);
            Uuid = $"RINCON_{attributes.MACAddress.Replace(":","")}0{this.ServiceOptions.DeviceUri.Port}";
        }
    }

    public async Task<bool> QueueNotificationAsync(NotificationOptions notificationOptions, CancellationToken cancellationToken = default)
    {
        //TODO Check if speaker is playing else skip
        if (notificationOptions.Volume < 1 || notificationOptions.Volume > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(NotificationOptions.Volume), "Volume must be between 1 and 100");
        }
        await LoadUuidAsync(cancellationToken);
        if (SonosWebSocket is null)
        {
            SonosWebSocket = new SonosWebSocket(ServiceOptions);
        }
        await SonosWebSocket.QueueNoticiationAsync(Uuid, notificationOptions.SoundUri.ToString(), notificationOptions.Volume, cancellationToken);
        return true;
    }

    #region Shortcuts

    public Task<bool> NextAsync(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.NextAsync(cancellationToken);

    public Task<bool> PauseAsync(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.PauseAsync(cancellationToken);

    public Task<bool> PlayAsync(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.PlayAsync(cancellationToken);

    public Task<bool> PreviousAsync(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.PreviousAsync(cancellationToken);

    public Task<bool> StopAsync(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.StopAsync(cancellationToken);

    #endregion Shortcuts

    public override string ToString()
    {
        return $"SonosSpeaker {DeviceName} ({Uuid})";
    }

    public void Dispose()
    {
        DisposeServices();
        SonosWebSocket?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (SonosWebSocket is not null)
        {
            await SonosWebSocket.DisposeAsync();
            SonosWebSocket = null;
        }
    }
}