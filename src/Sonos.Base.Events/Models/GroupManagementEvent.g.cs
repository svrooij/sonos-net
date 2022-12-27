/*
 * Sonos-net GroupManagementService event parsing
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
/// GroupManagement is set to might emit these properties in events
/// </summary>
public partial class GroupManagementEvent: GroupManagementService.IGroupManagementEvent {
    public bool? GroupCoordinatorIsLocal { get; init; }

    public string? LocalGroupUUID { get; init; }

    public bool? ResetVolumeAfter { get; init; }

    public string? SourceAreaIds { get; init; }

    public string? VirtualLineInGroupID { get; init; }

    public string? VolumeAVTransportURI { get; init; }

    internal static GroupManagementEvent? FromDictionary(Dictionary<string, string>? dic)
    {
        if (dic is null) {
            return null;
        }
        return new GroupManagementEvent {
            GroupCoordinatorIsLocal = dic.TryGetBool(nameof(GroupCoordinatorIsLocal)),
            LocalGroupUUID = dic.TryGetString(nameof(LocalGroupUUID)),
            ResetVolumeAfter = dic.TryGetBool(nameof(ResetVolumeAfter)),
            SourceAreaIds = dic.TryGetString(nameof(SourceAreaIds)),
            VirtualLineInGroupID = dic.TryGetString(nameof(VirtualLineInGroupID)),
            VolumeAVTransportURI = dic.TryGetString(nameof(VolumeAVTransportURI)),
        };
    }
}

