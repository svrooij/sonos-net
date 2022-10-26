namespace Sonos.Base.Services;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// GroupManagementService  - Services related to groups
/// </summary>
public partial class GroupManagementService : SonosBaseService {
  /// <summary>
  /// Create a new GroupManagementService
  /// </summary>
  /// <param name="sonosUri">Base URL of the speaker</param>
  /// <param name="httpClient">Optionally, a custom HttpClient.</param>
  public GroupManagementService(Uri sonosUri, HttpClient? httpClient): base("GroupManagement", "/GroupManagement/Control", "/GroupManagement/Event", sonosUri, httpClient) {}


  /// <summary>
  /// AddMember
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>AddMemberResponse</returns>
  public Task<AddMemberResponse> AddMember(AddMemberRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<AddMemberRequest, AddMemberResponse>("AddMember", request, cancellationToken);

  /// <summary>
  /// RemoveMember
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> RemoveMember(RemoveMemberRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RemoveMemberRequest>("RemoveMember", request, cancellationToken);

  /// <summary>
  /// ReportTrackBufferingResult
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> ReportTrackBufferingResult(ReportTrackBufferingResultRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<ReportTrackBufferingResultRequest>("ReportTrackBufferingResult", request, cancellationToken);

  /// <summary>
  /// SetSourceAreaIds
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> SetSourceAreaIds(SetSourceAreaIdsRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetSourceAreaIdsRequest>("SetSourceAreaIds", request, cancellationToken);

  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class BaseRequest {
    [System.Xml.Serialization.XmlNamespaceDeclarations]
    public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
      new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:GroupManagement:1"), });
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class AddMemberRequest : BaseRequest {

    public string MemberID { get; set; }

    public int BootSeq { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlType("AddMemberResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:GroupManagement:1")]
  public class AddMemberResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string CurrentTransportSettings { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string CurrentURI { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string GroupUUIDJoined { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public bool ResetVolumeAfter { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string VolumeAVTransportURI { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class RemoveMemberRequest : BaseRequest {

    public string MemberID { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class ReportTrackBufferingResultRequest : BaseRequest {

    public string MemberID { get; set; }

    public int ResultCode { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class SetSourceAreaIdsRequest : BaseRequest {

    public string DesiredSourceAreaIds { get; set; }
  }
}