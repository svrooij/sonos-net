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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sonos.Base.Services;

public class SonosServiceOptions
{
    /// <summary>
    /// Base URI of the device, eg. http://ip:1400/
    /// </summary>
    public Uri DeviceUri { get; set; }

    /// <summary>
    /// Device UUID, eg. RINCON_AAAAAAAAAA01400
    /// </summary>
    public string Uuid { get; set; }

    /// <summary>
    /// Pre-configured HTTP Client
    /// </summary>
    public IHttpClientFactory? HttpClientFactory { get; set; }

    /// <summary>
    /// Set logger factor to enable logging
    /// </summary>
    public ILoggerFactory? LoggerFactory { get; set; }

    /// <summary>
    /// Optional event bus that has to be set for receiving events.
    /// </summary>
    public ISonosEventBus? EventBus { get; set; }

    public static SonosServiceOptions CreateWithProvider(Uri deviceUri, string? uuid, IServiceProvider? provider)
    {
        return new SonosServiceOptions
        {
            DeviceUri = deviceUri,
            Uuid = uuid ?? Guid.NewGuid().ToString(),
            HttpClientFactory = provider?.GetService<IHttpClientFactory>(),
            LoggerFactory = provider?.GetService<ILoggerFactory>(),
            EventBus = provider?.GetService<ISonosEventBus>()
        };
    }
}
