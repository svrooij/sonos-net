﻿/*
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

namespace Sonos.Base;

/// <summary>
/// Configure your Sonos Device accordingly
/// </summary>
public class SonosDeviceOptions : Sonos.Base.Services.SonosServiceOptions
{
    public string? DeviceName { get; init; }
    public string? GroupName { get; init; }
    public SonosDevice? Coordinator { get; init; }

    public static SonosDeviceOptions CreateWithProvider(Uri deviceUri, string? uuid, string? deviceName, string? groupName, SonosDevice? coordinator, IServiceProvider? provider)
    {
        return new SonosDeviceOptions
        {
            DeviceUri = deviceUri,
            Uuid = uuid ?? Guid.NewGuid().ToString(),
            DeviceName = deviceName,
            GroupName = groupName,
            Coordinator = coordinator,
            HttpClientFactory = provider?.GetService<IHttpClientFactory>(),
            LoggerFactory = provider?.GetService<ILoggerFactory>(),
            EventBus = provider?.GetService<ISonosEventBus>(),
        };
    }
}