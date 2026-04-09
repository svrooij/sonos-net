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

    internal void UpdateCoordinator(SonosDevice? newCoordinator, string groupId, string groupName)
    {
        if (newCoordinator?.Uuid != coordinator?.Uuid)
        {
            coordinator = newCoordinator;
            logger?.LogInformation("Updated coordinator for {Device} to {Coordinator}", this, coordinator?.ToString() ?? "itself");
        }
        Status ??= new ();

        Status.GroupId = groupId;
        Status.GroupName = groupName;

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
        GroupName = options.GroupName ?? "Not specified";
    }

    internal SonosServiceOptions ServiceOptions { get; private set; }

    public async Task LoadUuid(CancellationToken cancellationToken = default)
    {
        if (!Uuid.StartsWith("RINCON"))
        {
            var attributes = await DevicePropertiesService.GetZoneInfo();
            Uuid = $"RINCON_{attributes.MACAddress.Replace(":","")}0{this.ServiceOptions.DeviceUri.Port}";
        }
    }

    public async Task<bool> QueueNotification(NotificationOptions notificationOptions, CancellationToken cancellationToken = default)
    {
        if (subscribedToEvents && notificationOptions.OnlyWhenPlaying && (Status?.IsPlaying != true))
        {
            logger?.LogInformation("Skipping notification on {Device} because it is not playing", this.ToString());
            return false;
        }

        if (notificationOptions.Volume < 1 || notificationOptions.Volume > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(NotificationOptions.Volume), "Volume must be between 1 and 100");
        }
        await LoadUuid(cancellationToken);
        // Lazy initialize the WebSocket when we need it for notifications, as not all users will use this feature and it is not needed for other operations
        SonosWebSocket ??= new SonosWebSocket(ServiceOptions);
        await SonosWebSocket.QueueNoticiationAsync(Uuid, notificationOptions.SoundUri.ToString(), notificationOptions.Volume, cancellationToken);
        return true;
    }

    public Task<bool> Next(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Next(cancellationToken);

    public Task<bool> Pause(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Pause(cancellationToken);

    public Task<bool> Play(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Play(cancellationToken);

    public Task<bool> Previous(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Previous(cancellationToken);

    public async Task<bool> SetTransportUri(SetTransportUriRequest request, CancellationToken cancellationToken = default)
    {
        var metadata = request.Metadata;
        //if (string.IsNullOrEmpty(metadata))
        //{
        //    metadata = await Coordinator.MetadataService.GetMetadataForUri(request.TransportUri, cancellationToken);
        //}
        return await AVTransportService.SetAVTransportURI(new AVTransportService.SetAVTransportURIRequest { CurrentURI = request.TransportUri, CurrentURIMetaData = metadata}, cancellationToken);
    }

    public Task<bool> Stop(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Stop(cancellationToken);

    public async Task<bool> SwitchToLineIn(CancellationToken cancellationToken = default)
    {
        await LoadUuid(cancellationToken);
        return await AVTransportService.SetAVTransportURI(new AVTransportService.SetAVTransportURIRequest { CurrentURI = $"x-rincon-stream:{Uuid}", CurrentURIMetaData = "" }, cancellationToken);
    }

    public async Task<bool> SwitchToQueue(CancellationToken cancellationToken = default)
    {
        await LoadUuid(cancellationToken);
        return await AVTransportService.SetAVTransportURI(new AVTransportService.SetAVTransportURIRequest { CurrentURI = $"x-rincon-queue:{Uuid}#0", CurrentURIMetaData = "" }, cancellationToken);
    }

    public async Task<bool> SwitchToSpdif(CancellationToken cancellationToken = default)
    {
        await LoadUuid(cancellationToken);
        return await AVTransportService.SetAVTransportURI(new AVTransportService.SetAVTransportURIRequest { CurrentURI = $"x-sonos-htastream:{Uuid}:spdif", CurrentURIMetaData = "" }, cancellationToken);
    }

    public async Task<bool> TogglePlayback(CancellationToken cancellationToken = default)
    {
        // Fast path if events are used and we have a status
        if (Status?.TransportState is not null)
        {
            if (Status.IsPlaying == true)
            {
                return await Coordinator.Pause(cancellationToken);
            }
            else
            {
                return await Coordinator.Play(cancellationToken);
            }
        }
        var transportInfo = await Coordinator.AVTransportService.GetTransportInfo(cancellationToken);
        
        return AVTransportService.TransportState.IsPlaying(transportInfo.CurrentTransportState) switch
        {
            true => await Coordinator.Pause(cancellationToken),
            false => await Coordinator.Play(cancellationToken),
            null => false
        };
    }

    public Models.SonosEvent? Status { get; private set; }
    public event EventHandler<Models.SonosEvent>? OnStatusChanged;
    private bool subscribedToEvents = false;

    public async Task SubscribeToEvents(CancellationToken cancellationToken = default)
    {
        if (!subscribedToEvents)
        {
            AVTransportService.OnEvent += AVTransportService_OnEvent;
            RenderingControlService.OnEvent += RenderingControlService_OnEvent;
            subscribedToEvents = true;
        }
        
        await AVTransportService.SubscribeForEventsAsync(cancellationToken);
        await RenderingControlService.SubscribeForEventsAsync(cancellationToken);
    }

    private void RenderingControlService_OnEvent(object? sender, RenderingControlService.IRenderingControlEvent e)
    {
        Status ??= new ();
        if (e.Volume is not null)
        {
            Status.Volume = e.Volume["Master"];
        }

        if (e.Mute is not null)
        {
            Status.Muted = e.Mute["Master"];
        }

        OnStatusChanged?.Invoke(this, Status);
    }

    private void AVTransportService_OnEvent(object? sender, AVTransportService.IAVTransportEvent e)
    {
        Status ??= new ();
        if (e.TransportState is not null)
        {
            Status.TransportState = e.TransportState;
        }

        if (e.AVTransportURI is not null && e.AVTransportURI != Status.AVTransportUri)
        {
            Status.AVTransportUri = e.AVTransportURI;
            if (e.AVTransportURIMetaDataObject?.Items is not null)
            {
                Status.AVTransportMetadata = e.AVTransportURIMetaDataObject.Items.FirstOrDefault();
            }
        }

        if (e.CurrentTrackURI is not null && e.CurrentTrackURI != Status.CurrentTrackUri)
        {
            Status.CurrentTrackUri = e.CurrentTrackURI;
            if (e.CurrentTrackMetaDataObject?.Items.Any() == true)
            {
                Status.CurrentTrack = e.CurrentTrackMetaDataObject.Items.FirstOrDefault();
            }
        }

        if (e.NextTrackURI is not null && e.NextTrackURI != Status.NextTrackUri)
        {
            Status.NextTrackUri = e.NextTrackURI;
            if (e.NextTrackMetaDataObject?.Items.Any() == true)
            {
                Status.NextTrack = e.NextTrackMetaDataObject.Items.FirstOrDefault();
            }
        }
        OnStatusChanged?.Invoke(this, Status);
    }

    //public async Task<HttpResponseMessage> GetAlbumArtAsync(string queryString, CancellationToken cancellationToken = default)
    //{
    //    var baseUri = new Uri(ServiceOptions.DeviceUri, $"/getaa?{queryString.TrimStart('?')}");
    //    var fullUri = new Uri(baseUri, queryString);
    //    var httpClient = ServiceOptions.ServiceProvider.GetHttpClient();
    //    return await httpClient.GetAsync(fullUri, cancellationToken);
    //}

    public Uri GenerateAlbumArtUrl(string queryString)
    {
        var baseUri = new Uri(ServiceOptions.DeviceUri, $"/getaa?{queryString.TrimStart('?')}");
        return new Uri(baseUri, queryString);
    }

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