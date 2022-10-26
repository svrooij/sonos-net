namespace Sonos.Base.Services;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// MusicServicesService  - Access to external music services, like Spotify or Youtube Music
/// </summary>
public partial class MusicServicesService : SonosBaseService {
  /// <summary>
  /// Create a new MusicServicesService
  /// </summary>
  /// <param name="sonosUri">Base URL of the speaker</param>
  /// <param name="httpClient">Optionally, a custom HttpClient.</param>
  public MusicServicesService(Uri sonosUri, HttpClient? httpClient): base("MusicServices", "/MusicServices/Control", "/MusicServices/Event", sonosUri, httpClient) {}


  /// <summary>
  /// GetSessionId
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>GetSessionIdResponse</returns>
  public Task<GetSessionIdResponse> GetSessionId(GetSessionIdRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetSessionIdRequest, GetSessionIdResponse>("GetSessionId", request, cancellationToken);

  /// <summary>
  /// Load music service list as xml
  /// </summary>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <remarks>Some libraries also support ListAndParseAvailableServices</remarks>
  /// <returns>ListAvailableServicesResponse</returns>
  public Task<ListAvailableServicesResponse> ListAvailableServices(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, ListAvailableServicesResponse>("ListAvailableServices", new BaseRequest(), cancellationToken);

  /// <summary>
  /// UpdateAvailableServices
  /// </summary>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> UpdateAvailableServices(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest>("UpdateAvailableServices", new BaseRequest(), cancellationToken);

  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class BaseRequest {
    [System.Xml.Serialization.XmlNamespaceDeclarations]
    public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
      new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:MusicServices:1"), });
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class GetSessionIdRequest : BaseRequest {

    public int ServiceId { get; set; }

    public string Username { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlTypeAttribute("GetSessionIdResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:MusicServices:1")]
  public class GetSessionIdResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string SessionId { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlTypeAttribute("ListAvailableServicesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:MusicServices:1")]
  public class ListAvailableServicesResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string AvailableServiceDescriptorList { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string AvailableServiceTypeList { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string AvailableServiceListVersion { get; set; }
  }
}