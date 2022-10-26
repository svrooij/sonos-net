namespace Sonos.Base.Services;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// QPlayService  - Services related to Chinese Tencent Qplay service
/// </summary>
public partial class QPlayService : SonosBaseService {
  /// <summary>
  /// Create a new QPlayService
  /// </summary>
  /// <param name="sonosUri">Base URL of the speaker</param>
  /// <param name="httpClient">Optionally, a custom HttpClient.</param>
  public QPlayService(Uri sonosUri, HttpClient? httpClient): base("QPlay", "/QPlay/Control", "/QPlay/Event", sonosUri, httpClient) {}


  /// <summary>
  /// QPlayAuth
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>QPlayAuthResponse</returns>
  public Task<QPlayAuthResponse> QPlayAuth(QPlayAuthRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<QPlayAuthRequest, QPlayAuthResponse>("QPlayAuth", request, cancellationToken);

  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class BaseRequest {
    [System.Xml.Serialization.XmlNamespaceDeclarations]
    public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
      new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:QPlay:1"), });
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class QPlayAuthRequest : BaseRequest {

    public string Seed { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlType("QPlayAuthResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:QPlay:1")]
  public partial class QPlayAuthResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string Code { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string MID { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string DID { get; set; }
  }
}