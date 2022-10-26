namespace Sonos.Base.Services;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// ConnectionManagerService  - Services related to connections and protocols
/// </summary>
public partial class ConnectionManagerService : SonosBaseService {
  /// <summary>
  /// Create a new ConnectionManagerService
  /// </summary>
  /// <param name="sonosUri">Base URL of the speaker</param>
  /// <param name="httpClient">Optionally, a custom HttpClient.</param>
  public ConnectionManagerService(Uri sonosUri, HttpClient? httpClient): base("ConnectionManager", "/MediaRenderer/ConnectionManager/Control", "/MediaRenderer/ConnectionManager/Event", sonosUri, httpClient) {}


  /// <summary>
  /// GetCurrentConnectionIDs
  /// </summary>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>GetCurrentConnectionIDsResponse</returns>
  public Task<GetCurrentConnectionIDsResponse> GetCurrentConnectionIDs(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetCurrentConnectionIDsResponse>("GetCurrentConnectionIDs", new BaseRequest(), cancellationToken);

  /// <summary>
  /// GetCurrentConnectionInfo
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>GetCurrentConnectionInfoResponse</returns>
  public Task<GetCurrentConnectionInfoResponse> GetCurrentConnectionInfo(GetCurrentConnectionInfoRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetCurrentConnectionInfoRequest, GetCurrentConnectionInfoResponse>("GetCurrentConnectionInfo", request, cancellationToken);

  /// <summary>
  /// GetProtocolInfo
  /// </summary>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>GetProtocolInfoResponse</returns>
  public Task<GetProtocolInfoResponse> GetProtocolInfo(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetProtocolInfoResponse>("GetProtocolInfo", new BaseRequest(), cancellationToken);

  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class BaseRequest {
    [System.Xml.Serialization.XmlNamespaceDeclarations]
    public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
      new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:ConnectionManager:1"), });
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlType("GetCurrentConnectionIDsResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ConnectionManager:1")]
  public class GetCurrentConnectionIDsResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string ConnectionIDs { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class GetCurrentConnectionInfoRequest : BaseRequest {

    public int ConnectionID { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlType("GetCurrentConnectionInfoResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ConnectionManager:1")]
  public class GetCurrentConnectionInfoResponse {

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
  public class GetProtocolInfoResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string Source { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string Sink { get; set; }
  }
}