/*
 * Sonos-net MusicServicesService
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

namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// MusicServicesService  - Access to external music services, like Spotify or Youtube Music
/// </summary>
public partial class MusicServicesService : SonosBaseService<MusicServicesService.IMusicServicesEvent>
{
    /// <summary>
    /// Create a new MusicServicesService
    /// </summary>
    /// <param name="options">Service options</param>
    public MusicServicesService(SonosServiceOptions options) : base(SonosService.MusicServices, "/MusicServices/Control", "/MusicServices/Event", options) { }


    /// <summary>
    /// GetSessionId
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetSessionIdResponse</returns>
    public Task<GetSessionIdResponse> GetSessionId(GetSessionIdRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetSessionIdRequest, GetSessionIdResponse>(request, cancellationToken);

    /// <summary>
    /// Load music service list as xml
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Some libraries also support ListAndParseAvailableServices</remarks>
    /// <returns>ListAvailableServicesResponse</returns>
    public Task<ListAvailableServicesResponse> ListAvailableServices(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, ListAvailableServicesResponse>(new BaseRequest(), cancellationToken);

    /// <summary>
    /// UpdateAvailableServices
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> UpdateAvailableServices(CancellationToken cancellationToken = default) => ExecuteRequest(new BaseRequest(), cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MusicServices/Control", "MusicServices")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:MusicServices:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MusicServices/Control", "MusicServices", "GetSessionId")]
    public class GetSessionIdRequest : BaseRequest
    {
        public int ServiceId { get; set; }

        public string Username { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetSessionIdResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:MusicServices:1")]
    public partial class GetSessionIdResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string SessionId { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("ListAvailableServicesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:MusicServices:1")]
    public partial class ListAvailableServicesResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AvailableServiceDescriptorList { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AvailableServiceTypeList { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AvailableServiceListVersion { get; set; }
    }

    /// <summary>
    /// MusicServices is set to might emit these properties in events
    /// </summary>
    public partial interface IMusicServicesEvent : IServiceEvent
    {
        public int? ServiceId { get; }

        public string? ServiceListVersion { get; }

        public string? SessionId { get; }

        public string? Username { get; }
    }
}
