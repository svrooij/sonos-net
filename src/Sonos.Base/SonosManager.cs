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
        private readonly ISonosServiceProvider provider;
        private readonly ILogger? logger;

        public SonosManager(ISonosServiceProvider provider, ILogger<SonosManager>? logger = null)
        {
            groups = new ConcurrentDictionary<string, SonosDeviceGroup>();
            this.provider = provider;
            this.logger = logger ?? provider.CreateLogger<SonosManager>();
        }

        public async Task InitializeFromDevice(Uri deviceUri, CancellationToken cancellationToken = default)
        {
            logger?.LogDebug("Initialize manager from device {deviceUri}", deviceUri);
            if (zoneGroupTopologyService == null)
            {
                zoneGroupTopologyService = new ZoneGroupTopologyService(new SonosServiceOptions(deviceUri, provider));
                var zoneState = await zoneGroupTopologyService.GetZoneGroupState(cancellationToken);

                if (zoneState is null || zoneState.ParsedState is null)
                {
                    return;
                }

                foreach (var zone in zoneState.ParsedState.ZoneGroups)
                {
                    var coordinator = new SonosDevice(new SonosDeviceOptions(zone.CoordinatorMember.BaseUri, provider, zone.CoordinatorMember.UUID, zone.CoordinatorMember.ZoneName, zone.GroupName, null));


                    groups.TryAdd(zone.ID, new SonosDeviceGroup
                    {
                        Coordinator = coordinator,
                        GroupName = zone.GroupName,
                        Members = zone.Members.Select(member => new SonosDevice(new SonosDeviceOptions(member.BaseUri, provider, member.UUID, member.ZoneName, coordinator.GroupName, coordinator))).ToArray()
                    });
                }
            }
        }

        public IReadOnlyCollection<SonosDeviceGroup> GetGroups() => groups.Values.ToArray();
    }
}