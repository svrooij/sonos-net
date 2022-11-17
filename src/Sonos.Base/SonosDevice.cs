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

public partial class SonosDevice
{
    private readonly Uri deviceUri;
    private readonly HttpClient httpClient;
    public string Uuid { get; private set; }

    public SonosDevice(Uri deviceUri, string? uuid = null, HttpClient? httpClient = null, ILoggerFactory? loggerFactory = null)
    {
        this.deviceUri = deviceUri;
        this.httpClient = httpClient ?? new HttpClient();
        this.Uuid = uuid ?? Guid.NewGuid().ToString();
        ServiceOptions = new SonosServiceOptions
        {
            DeviceUri = deviceUri,
            HttpClient = httpClient,
            LoggerFactory = loggerFactory,
        };
    }

    internal SonosServiceOptions ServiceOptions { get; private set; }

    #region Shortcuts
    public Task<bool> Next(CancellationToken cancellationToken = default) => this.AVTransportService.Next(cancellationToken);
    public Task<bool> Pause(CancellationToken cancellationToken = default) => this.AVTransportService.Pause(cancellationToken);
    public Task<bool> Play(CancellationToken cancellationToken = default) => this.AVTransportService.Play(cancellationToken);
    public Task<bool> Previous(CancellationToken cancellationToken = default) => this.AVTransportService.Previous(cancellationToken);
    public Task<bool> Stop(CancellationToken cancellationToken = default) => this.AVTransportService.Stop(cancellationToken);
    #endregion
}
