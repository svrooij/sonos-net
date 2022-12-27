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
using Sonos.Base.Services;
using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace Sonos.Base
{
    public class SonosManager
    {
        private readonly ConcurrentDictionary<string, SonosDeviceGroup> groups;
        private ZoneGroupTopologyService? zoneGroupTopologyService;
        private readonly IServiceProvider? provider;
        private readonly ILogger? logger;
        private readonly ILoggerFactory? loggerFactory;

        public SonosManager(IServiceProvider? provider = null)
        {
            groups = new ConcurrentDictionary<string, SonosDeviceGroup>();
            this.provider = provider;
            loggerFactory = provider?.GetService<ILoggerFactory>();
            logger = loggerFactory?.CreateLogger<SonosManager>();
        }

        public async Task InitializeFromDevice(Uri deviceUri, CancellationToken cancellationToken = default)
        {
            logger?.LogDebug("Initialize manager from device {deviceUri}", deviceUri);
            if (zoneGroupTopologyService == null)
            {
                zoneGroupTopologyService = new ZoneGroupTopologyService(SonosServiceOptions.CreateWithProvider(deviceUri, null, provider));
                var zoneState = await zoneGroupTopologyService.GetZoneGroupState(cancellationToken);

                foreach (var zone in zoneState.ParsedState.ZoneGroups)
                {
                    var coordinator = new SonosDevice(SonosDeviceOptions.CreateWithProvider(zone.CoordinatorMember.BaseUri, zone.CoordinatorMember.UUID, zone.CoordinatorMember.ZoneName, zone.GroupName, null, provider));
                    

                    groups.TryAdd(zone.ID, new SonosDeviceGroup
                    {
                        Coordinator = coordinator,
                        GroupName = zone.GroupName,
                        Members = zone.Members.Select(member => new SonosDevice(SonosDeviceOptions.CreateWithProvider(member.BaseUri, member.UUID, member.ZoneName, coordinator.GroupName, coordinator, provider))).ToArray()
                    });
                }
            }
        }

        public IReadOnlyCollection<SonosDeviceGroup> GetGroups() => groups.Values.ToArray();
    }
}