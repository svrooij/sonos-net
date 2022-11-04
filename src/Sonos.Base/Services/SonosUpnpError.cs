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

namespace Sonos.Base.Services;

public partial class SonosUpnpError
{
    public SonosUpnpError(int code, string message)
    {
        Code = code;
        Message = message;
    }
    public int Code { get; init; }
    public string Message { get; init; }
}

internal static class SonosUpnpErrorDictionaryExtensions
{
    internal static Dictionary<int, SonosUpnpError> Merge(this Dictionary<int, SonosUpnpError> input, Dictionary<int, SonosUpnpError> additionalErrors)
    {
        foreach (var item in additionalErrors)
        {
            input[item.Key] = item.Value;
        }
        return input;
    }
}
