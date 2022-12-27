/*
 * Sonos-net SystemPropertiesService event parsing
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
/// SystemProperties is set to might emit these properties in events
/// </summary>
public partial class SystemPropertiesEvent: SystemPropertiesService.ISystemPropertiesEvent {
    public string? CustomerID { get; init; }

    public string? ThirdPartyHash { get; init; }

    public int? UpdateID { get; init; }

    public int? UpdateIDX { get; init; }

    public int? VoiceUpdateID { get; init; }

    internal static SystemPropertiesEvent? FromDictionary(Dictionary<string, string>? dic)
    {
        if (dic is null) {
            return null;
        }
        return new SystemPropertiesEvent {
            CustomerID = dic.TryGetString(nameof(CustomerID)),
            ThirdPartyHash = dic.TryGetString(nameof(ThirdPartyHash)),
            UpdateID = dic.TryGetInt(nameof(UpdateID)),
            UpdateIDX = dic.TryGetInt(nameof(UpdateIDX)),
            VoiceUpdateID = dic.TryGetInt(nameof(VoiceUpdateID)),
        };
    }
}

