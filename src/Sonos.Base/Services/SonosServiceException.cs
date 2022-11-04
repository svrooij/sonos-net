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

public class SonosServiceException : SonosException
{
    const string DefaultMessage = "Sonos service exception occurred";
    public string FaultCode { get; init; }
    public string FaultString { get; init; }
    public int? UpnpErrorCode { get; init; }
    public string? UpnpErrorMessage { get; init; }

    public SonosServiceException(string faultCode, string faultString, int? upnpErrorCode = null, string? upnpErrorMessage = null) : base(upnpErrorMessage ?? DefaultMessage)
    {
        FaultCode = faultCode;
        FaultString = faultString;
        UpnpErrorCode = upnpErrorCode;
        UpnpErrorMessage = upnpErrorMessage;
    }
}
