/*
 * Sonos-net ZoneGroupTopologyService event parsing
 *
 * File is generated by [@svrooij/sonos-docs](https://github.com/svrooij/sonos-api-docs/tree/main/generator/sonos-docs)
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

namespace Sonos.Base.Events.Models;

using Sonos.Base.Services;


#nullable enable
/// <summary>
/// ZoneGroupTopology is set to might emit these properties in events
/// </summary>
public partial class ZoneGroupTopologyEvent: ZoneGroupTopologyService.IZoneGroupTopologyEvent {
    public string? AlarmRunSequence { get; init; }

    public string? AreasUpdateID { get; init; }

    public string? AvailableSoftwareUpdate { get; init; }

    public int? DiagnosticID { get; init; }

    public string? MuseHouseholdId { get; init; }

    public string? NetsettingsUpdateID { get; init; }

    public string? SourceAreasUpdateID { get; init; }

    public string? ThirdPartyMediaServersX { get; init; }

    public string? ZoneGroupID { get; init; }

    public string? ZoneGroupName { get; init; }

    public string? ZoneGroupState { get; init; }

    public string? ZonePlayerUUIDsInGroup { get; init; }

    internal static ZoneGroupTopologyEvent? FromDictionary(Dictionary<string, string>? dic)
    {
        if (dic is null) {
            return null;
        }
        return new ZoneGroupTopologyEvent {
            AlarmRunSequence = dic.TryGetString(nameof(AlarmRunSequence)),
            AreasUpdateID = dic.TryGetString(nameof(AreasUpdateID)),
            AvailableSoftwareUpdate = dic.TryGetString(nameof(AvailableSoftwareUpdate)),
            DiagnosticID = dic.TryGetInt(nameof(DiagnosticID)),
            MuseHouseholdId = dic.TryGetString(nameof(MuseHouseholdId)),
            NetsettingsUpdateID = dic.TryGetString(nameof(NetsettingsUpdateID)),
            SourceAreasUpdateID = dic.TryGetString(nameof(SourceAreasUpdateID)),
            ThirdPartyMediaServersX = dic.TryGetString(nameof(ThirdPartyMediaServersX)),
            ZoneGroupID = dic.TryGetString(nameof(ZoneGroupID)),
            ZoneGroupName = dic.TryGetString(nameof(ZoneGroupName)),
            ZoneGroupState = dic.TryGetString(nameof(ZoneGroupState)),
            ZonePlayerUUIDsInGroup = dic.TryGetString(nameof(ZonePlayerUUIDsInGroup)),
        };
    }
}

