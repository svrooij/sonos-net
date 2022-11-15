/*
 * Sonos-net ConnectionManagerService
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
/// ConnectionManagerService  - Services related to connections and protocols
/// </summary>
public partial class ConnectionManagerService : SonosBaseService
{
    /// <summary>
    /// Create a new ConnectionManagerService
    /// </summary>
    /// <param name="options">Service options</param>
    public ConnectionManagerService(SonosServiceOptions options) : base("ConnectionManager", "/MediaRenderer/ConnectionManager/Control", "/MediaRenderer/ConnectionManager/Event", options) { }


    /// <summary>
    /// GetCurrentConnectionIDs
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetCurrentConnectionIDsResponse</returns>
    public Task<GetCurrentConnectionIDsResponse> GetCurrentConnectionIDs(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetCurrentConnectionIDsResponse>(new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetCurrentConnectionInfo
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetCurrentConnectionInfoResponse</returns>
    public Task<GetCurrentConnectionInfoResponse> GetCurrentConnectionInfo(GetCurrentConnectionInfoRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetCurrentConnectionInfoRequest, GetCurrentConnectionInfoResponse>(request, cancellationToken);

    /// <summary>
    /// GetProtocolInfo
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetProtocolInfoResponse</returns>
    public Task<GetProtocolInfoResponse> GetProtocolInfo(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetProtocolInfoResponse>(new BaseRequest(), cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/ConnectionManager/Control", "ConnectionManager")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:ConnectionManager:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetCurrentConnectionIDsResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ConnectionManager:1")]
    public partial class GetCurrentConnectionIDsResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string ConnectionIDs { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/ConnectionManager/Control", "ConnectionManager", "GetCurrentConnectionInfo")]
    public class GetCurrentConnectionInfoRequest : BaseRequest
    {
        public int ConnectionID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetCurrentConnectionInfoResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ConnectionManager:1")]
    public partial class GetCurrentConnectionInfoResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int RcsID { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int AVTransportID { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string ProtocolInfo { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string PeerConnectionManager { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int PeerConnectionID { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string Direction { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string Status { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetProtocolInfoResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ConnectionManager:1")]
    public partial class GetProtocolInfoResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string Source { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string Sink { get; set; }
    }
}
