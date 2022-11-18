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

using Microsoft.Extensions.Logging;
using Sonos.Base.Services;
using System.Collections.Concurrent;

namespace Sonos.Base
{
    public class SonosManager
    {
        private readonly ConcurrentDictionary<string, SonosDeviceGroup> groups;
        private ZoneGroupTopologyService? zoneGroupTopologyService;
        private readonly HttpClient httpClient;
        private readonly ILogger? logger;
        private readonly ILoggerFactory? loggerFactory;

        public SonosManager(HttpClient? httpClient = null, ILoggerFactory? loggerFactory = null)
        {
            groups = new ConcurrentDictionary<string, SonosDeviceGroup>();
            this.httpClient = httpClient ?? new HttpClient();
            this.logger = loggerFactory?.CreateLogger<SonosManager>();
            this.loggerFactory = loggerFactory;
        }

        public async Task InitializeFromDevice(Uri deviceUri, CancellationToken cancellationToken = default)
        {
            logger?.LogDebug("Initialize manager from device {deviceUri}", deviceUri);
            if (zoneGroupTopologyService == null)
            {
                zoneGroupTopologyService = new ZoneGroupTopologyService(new SonosServiceOptions { DeviceUri = deviceUri, HttpClient = httpClient, LoggerFactory = loggerFactory });
                var zoneState = await zoneGroupTopologyService.GetZoneGroupState(cancellationToken);

                foreach (var zone in zoneState.ParsedState.ZoneGroups)
                {
                    var coordinator = new SonosDevice(new SonosDeviceOptions
                    {
                        DeviceUri = zone.CoordinatorMember.BaseUri,
                        Uuid = zone.CoordinatorMember.UUID,
                        DeviceName = zone.CoordinatorMember.ZoneName,
                        GroupName = zone.GroupName,
                        HttpClient = httpClient,
                        LoggerFactory = loggerFactory,
                    });

                    groups.TryAdd(zone.ID, new SonosDeviceGroup
                    {
                        Coordinator = coordinator,
                        GroupName = zone.GroupName,
                        Members = zone.Members.Select(member => new SonosDevice(new SonosDeviceOptions
                        {
                            DeviceUri = member.BaseUri,
                            Uuid = member.UUID,
                            DeviceName = member.ZoneName,
                            GroupName = coordinator.GroupName,
                            HttpClient = httpClient,
                            LoggerFactory = loggerFactory,
                            Coordinator = coordinator
                        })).ToArray()
                    });
                }
            }
        }

        public IReadOnlyCollection<SonosDeviceGroup> GetGroups() => groups.Values.ToArray();
    }
}