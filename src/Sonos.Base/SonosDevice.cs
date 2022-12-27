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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sonos.Base.Services;
using System.Net.Http;

public partial class SonosDevice : IDisposable
{
    private readonly Uri deviceUri;
    private readonly HttpClient httpClient;
    private SonosDevice? coordinator;

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

    public string DeviceName { get; init; }
    public string GroupName { get; init; }
    public string Uuid { get; private set; }

    public SonosDevice(Uri deviceUri, string? uuid = null, IServiceProvider? provider = null)
    {
        this.deviceUri = deviceUri;
        this.Uuid = uuid ?? Guid.NewGuid().ToString();

        if (provider is null)
        {
            this.httpClient = new HttpClient();
            ServiceOptions = new SonosServiceOptions
            {
                DeviceUri = deviceUri,
                Uuid = Uuid
            };
        } else
        {
            var httpClientFactory = provider.GetService<IHttpClientFactory>();
            var loggerFactory = provider.GetService<ILoggerFactory>();
            this.httpClient = httpClientFactory?.CreateClient()  ?? new HttpClient();
            ServiceOptions = new SonosServiceOptions
            {
                DeviceUri = deviceUri,
                Uuid = Uuid,
                HttpClientFactory = httpClientFactory,
                LoggerFactory = loggerFactory,
                EventBus = provider.GetService<ISonosEventBus>()
            };
        }

        
    }

    public SonosDevice(SonosDeviceOptions options)
    {
        ServiceOptions = (SonosServiceOptions)options;
        coordinator = options.Coordinator;
        DeviceName = options.DeviceName ?? "Not specified";
        Uuid = options.Uuid ?? Guid.NewGuid().ToString();
    }

    internal SonosServiceOptions ServiceOptions { get; private set; }

    #region Shortcuts

    public Task<bool> Next(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Next(cancellationToken);

    public Task<bool> Pause(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Pause(cancellationToken);

    public Task<bool> Play(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Play(cancellationToken);

    public Task<bool> Previous(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Previous(cancellationToken);

    public Task<bool> Stop(CancellationToken cancellationToken = default) => Coordinator.AVTransportService.Stop(cancellationToken);

    #endregion Shortcuts

    public override string ToString()
    {
        return $"SonosSpeaker {DeviceName} ({Uuid})";
    }

    public void Dispose()
    {
        DisposeServices();
    }
}