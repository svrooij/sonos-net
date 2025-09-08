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
        private readonly ConcurrentDictionary<string, SonosDevice> devices = new();
        private ZoneGroupTopologyService? zoneGroupTopologyService;
        private bool subscribed = false;
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

                    if (!devices.ContainsKey(coordinator.Uuid))
                    {
                        devices.TryAdd(coordinator.Uuid, coordinator);
                    }

                    var members = zone.Members.Select(member => new SonosDevice(new SonosDeviceOptions(member.BaseUri, provider, member.UUID, member.ZoneName, coordinator.GroupName, coordinator)));
                    foreach (var member in members)
                    {
                        if (!devices.ContainsKey(member.Uuid))
                        {
                            devices.TryAdd(member.Uuid, member);
                        }
                    }
                    groups.TryAdd(zone.ID, new SonosDeviceGroup
                    {
                        GroupId = zone.ID,
                        Coordinator = new SonosDeviceInfo { Name = coordinator.DeviceName, Uuid = coordinator.Uuid, Uri = zone.CoordinatorMember.BaseUri },
                        GroupName = zone.GroupName,
                        Members = zone.Members.Select(m => new SonosDeviceInfo { Name = m.ZoneName, Uuid = m.UUID, Uri = m.BaseUri }).ToList()
                    });
                }
            }
        }

        public async Task SubscribeToTopologyChanges(CancellationToken cancellationToken = default)
        {
            if (zoneGroupTopologyService == null)
            {
                throw new InvalidOperationException("Manager not initialized, call InitializeFromDevice first");
            }
            if (!subscribed)
            {
                zoneGroupTopologyService.OnEvent += ZoneGroupTopologyService_OnEvent;
                subscribed = true;
            }
            
            await zoneGroupTopologyService.SubscribeForEventsAsync(cancellationToken);
        }

        private void ZoneGroupTopologyService_OnEvent(object? sender, ZoneGroupTopologyService.IZoneGroupTopologyEvent e)
        {
            // TODO: handle topology changes
            logger?.LogInformation("Zones changed @{Zones}", e.ParsedState);
            if (e.ParsedState?.ZoneGroups == null)
            {
                return;
            }
            foreach (var zone in e.ParsedState.ZoneGroups)
            {
                var coordinator = GetSonosDevice(zone.Coordinator);
                if (coordinator == null)
                {
                    coordinator = new SonosDevice(new SonosDeviceOptions(zone.CoordinatorMember.BaseUri, provider, zone.CoordinatorMember.UUID, zone.CoordinatorMember.ZoneName, zone.GroupName, null));
                    devices.TryAdd(coordinator.Uuid, coordinator);
                }
                coordinator.UpdateCoordinator(null, zone.ID, zone.GroupName);

                groups[zone.ID] = new SonosDeviceGroup
                {
                    GroupId = zone.ID,
                    Coordinator = new SonosDeviceInfo { Name = coordinator.DeviceName, Uuid = coordinator.Uuid, Uri = zone.CoordinatorMember.BaseUri },
                    GroupName = zone.GroupName,
                    Members = zone.Members.Select(m => new SonosDeviceInfo { Name = m.ZoneName, Uuid = m.UUID, Uri = m.BaseUri }).ToList()
                };
                //groups.AddOrUpdate(zone.ID, new SonosDeviceGroup
                //{
                //    GroupId = zone.ID,
                //    Coordinator = new SonosDeviceInfo { Name = coordinator.DeviceName, Uuid = coordinator.Uuid, Uri = zone.CoordinatorMember.BaseUri },
                //    GroupName = zone.GroupName,
                //    Members = zone.Members.Select(m => new SonosDeviceInfo { Name = m.ZoneName, Uuid = m.UUID, Uri = m.BaseUri }).ToList()
                //}, (old, newGroup) => newGroup);

                foreach (var member in zone.Members)
                {
                    var device = GetSonosDevice(member.UUID);
                    if (device == null)
                    {
                        device = new SonosDevice(new SonosDeviceOptions(member.BaseUri, provider, member.UUID, member.ZoneName, coordinator.GroupName, coordinator));
                        devices.TryAdd(device.Uuid, device);
                    }
                    else
                    {
                        device.UpdateCoordinator(coordinator, zone.ID, zone.GroupName);
                    }
                }
            }

            var groupsToRemove = groups.Keys.Except(e.ParsedState.ZoneGroups.Select(z => z.ID)).ToList();
            foreach (var item in groupsToRemove)
            {
                groups.TryRemove(item, out _);
            }
        }

        public IReadOnlyCollection<SonosDeviceGroup> GetGroups() => groups.Values.ToArray();

        public SonosDevice? GetSonosDevice(string uuid)
        {
            // Some services respond the same for all speakers (like AlarmClock)
            if (uuid == "RINCON_00000000000001400")
            {
                var firstDevice = devices.Values.FirstOrDefault();
                if(firstDevice is not null)
                {
                    return firstDevice;
                }

            }
            if (devices.TryGetValue(uuid, out var device))
            {
                return device;
            }
            return null;
        }

        public IEnumerable<string> GetDeviceUuids() => devices.Keys;
    }
}