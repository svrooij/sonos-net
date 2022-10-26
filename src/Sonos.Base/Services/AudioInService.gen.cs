namespace Sonos.Base.Services;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// AudioInService  - Control line in
/// </summary>
public partial class AudioInService : SonosBaseService {
  /// <summary>
  /// Create a new AudioInService
  /// </summary>
  /// <param name="sonosUri">Base URL of the speaker</param>
  /// <param name="httpClient">Optionally, a custom HttpClient.</param>
  public AudioInService(Uri sonosUri, HttpClient? httpClient): base("AudioIn", "/AudioIn/Control", "/AudioIn/Event", sonosUri, httpClient) {}


  /// <summary>
  /// GetAudioInputAttributes
  /// </summary>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>GetAudioInputAttributesResponse</returns>
  public Task<GetAudioInputAttributesResponse> GetAudioInputAttributes(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetAudioInputAttributesResponse>("GetAudioInputAttributes", new BaseRequest(), cancellationToken);

  /// <summary>
  /// GetLineInLevel
  /// </summary>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>GetLineInLevelResponse</returns>
  public Task<GetLineInLevelResponse> GetLineInLevel(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetLineInLevelResponse>("GetLineInLevel", new BaseRequest(), cancellationToken);

  /// <summary>
  /// SelectAudio
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> SelectAudio(SelectAudioRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SelectAudioRequest>("SelectAudio", request, cancellationToken);

  /// <summary>
  /// SetAudioInputAttributes
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> SetAudioInputAttributes(SetAudioInputAttributesRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetAudioInputAttributesRequest>("SetAudioInputAttributes", request, cancellationToken);

  /// <summary>
  /// SetLineInLevel
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> SetLineInLevel(SetLineInLevelRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetLineInLevelRequest>("SetLineInLevel", request, cancellationToken);

  /// <summary>
  /// StartTransmissionToGroup
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>StartTransmissionToGroupResponse</returns>
  public Task<StartTransmissionToGroupResponse> StartTransmissionToGroup(StartTransmissionToGroupRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<StartTransmissionToGroupRequest, StartTransmissionToGroupResponse>("StartTransmissionToGroup", request, cancellationToken);

  /// <summary>
  /// StopTransmissionToGroup
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> StopTransmissionToGroup(StopTransmissionToGroupRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<StopTransmissionToGroupRequest>("StopTransmissionToGroup", request, cancellationToken);

  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class BaseRequest {
    [System.Xml.Serialization.XmlNamespaceDeclarations]
    public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
      new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:AudioIn:1"), });
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlTypeAttribute("GetAudioInputAttributesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AudioIn:1")]
  public class GetAudioInputAttributesResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string CurrentName { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string CurrentIcon { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlTypeAttribute("GetLineInLevelResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AudioIn:1")]
  public class GetLineInLevelResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public int CurrentLeftLineInLevel { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public int CurrentRightLineInLevel { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class SelectAudioRequest : BaseRequest {

    public string ObjectID { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class SetAudioInputAttributesRequest : BaseRequest {

    public string DesiredName { get; set; }

    public string DesiredIcon { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class SetLineInLevelRequest : BaseRequest {

    public int DesiredLeftLineInLevel { get; set; }

    public int DesiredRightLineInLevel { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class StartTransmissionToGroupRequest : BaseRequest {

    public string CoordinatorID { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlTypeAttribute("StartTransmissionToGroupResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AudioIn:1")]
  public class StartTransmissionToGroupResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string CurrentTransportSettings { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class StopTransmissionToGroupRequest : BaseRequest {

    public string CoordinatorID { get; set; }
  }
}