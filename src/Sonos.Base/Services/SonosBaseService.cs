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

using Sonos.Base.Soap;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

public class SonosBaseService
{
    private readonly string ControlPath;
    private readonly string EventPath;
    private readonly string ServiceName;
    private readonly Uri BaseUri;
    private readonly HttpClient httpClient;

    internal SonosBaseService(string serviceName, string controlPath, string eventPath, SonosServiceOptions options)
    {
        this.ControlPath = controlPath;
        this.EventPath = eventPath;
        this.ServiceName = serviceName;
        this.BaseUri = options.DeviceUri;
        this.httpClient = options.HttpClient ?? new HttpClient();
    }

    internal async Task<bool> ExecuteRequest<TPayload>(TPayload payload, CancellationToken cancellationToken, [CallerMemberName] string? caller = null)
    {
        var request = SoapFactory.CreateRequest(this.BaseUri, this.ControlPath, payload, caller);
        var response = await httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponse(response, cancellationToken);
        }
        return true;
    }

    internal async Task<TOut> ExecuteRequest<TPayload, TOut>(TPayload payload, CancellationToken cancellationToken, [CallerMemberName] string? caller = null)
    {
        var request = SoapFactory.CreateRequest(this.BaseUri, this.ControlPath, payload, caller);
        var response = await httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponse(response, cancellationToken);
        }
        return await ParseResponse<TOut>(response, cancellationToken);
    }

    internal async Task HandleErrorResponse(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        // TODO HandleErrorResponse needs implementation
        // var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
        using var errorContent = await response.Content.ReadAsStreamAsync(cancellationToken);
        var error = SoapFactory.ParseFaultXml(errorContent);
        if (error is not null)
        {
            var code = error.UpnpErrorCode;
            string? message = (code is not null && ServiceErrors.ContainsKey((int)code)) ? ServiceErrors[(int)code].Message : null;
            throw new SonosServiceException(error.FaultCode, error.FaultString, code, message);
        }
        throw new Exception();
    }

    internal async Task<TOut> ParseResponse<TOut>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        using var xml = await response.Content.ReadAsStreamAsync(cancellationToken);
        return SoapFactory.ParseXml<TOut>(this.ServiceName, xml);
    }

    internal virtual Dictionary<int, SonosUpnpError> ServiceErrors { get => SonosUpnpError.DefaultErrors; }
}