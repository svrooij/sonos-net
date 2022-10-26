namespace Sonos.Base.Services;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// GroupRenderingControlService  - Volume related controls for groups
/// </summary>
public partial class GroupRenderingControlService : SonosBaseService {
  /// <summary>
  /// Create a new GroupRenderingControlService
  /// </summary>
  /// <param name="sonosUri">Base URL of the speaker</param>
  /// <param name="httpClient">Optionally, a custom HttpClient.</param>
  public GroupRenderingControlService(Uri sonosUri, HttpClient? httpClient): base("GroupRenderingControl", "/MediaRenderer/GroupRenderingControl/Control", "/MediaRenderer/GroupRenderingControl/Event", sonosUri, httpClient) {}


  /// <summary>
  /// Get the group mute state.
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <remarks>Should be send to coordinator only</remarks>
  /// <returns>GetGroupMuteResponse</returns>
  public Task<GetGroupMuteResponse> GetGroupMute(CancellationToken cancellationToken = default) => ExecuteRequest<GetGroupMuteRequest, GetGroupMuteResponse>("GetGroupMute", new GetGroupMuteRequest(), cancellationToken);

  /// <summary>
  /// Get the group volume.
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <remarks>Should be send to coordinator only</remarks>
  /// <returns>GetGroupVolumeResponse</returns>
  public Task<GetGroupVolumeResponse> GetGroupVolume(CancellationToken cancellationToken = default) => ExecuteRequest<GetGroupVolumeRequest, GetGroupVolumeResponse>("GetGroupVolume", new GetGroupVolumeRequest(), cancellationToken);

  /// <summary>
  /// (Un-/)Mute the entire group
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <remarks>Should be send to coordinator only</remarks>
  /// <returns>Success boolean</returns>
  public Task<bool> SetGroupMute(SetGroupMuteRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetGroupMuteRequest>("SetGroupMute", request, cancellationToken);

  /// <summary>
  /// Change group volume. Players volume will be changed proportionally based on last snapshot
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <remarks>Should be send to coordinator only</remarks>
  /// <returns>Success boolean</returns>
  public Task<bool> SetGroupVolume(SetGroupVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetGroupVolumeRequest>("SetGroupVolume", request, cancellationToken);

  /// <summary>
  /// Relatively change group volume - returns final group volume. Players volume will be changed proportionally based on last snapshot
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <remarks>Should be send to coordinator only</remarks>
  /// <returns>SetRelativeGroupVolumeResponse</returns>
  public Task<SetRelativeGroupVolumeResponse> SetRelativeGroupVolume(SetRelativeGroupVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetRelativeGroupVolumeRequest, SetRelativeGroupVolumeResponse>("SetRelativeGroupVolume", request, cancellationToken);

  /// <summary>
  /// Creates a new group volume snapshot,  the volume ratio between all players. It is used by SetGroupVolume and SetRelativeGroupVolume
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <remarks>Should be send to coordinator only</remarks>
  /// <returns>Success boolean</returns>
  public Task<bool> SnapshotGroupVolume(CancellationToken cancellationToken = default) => ExecuteRequest<SnapshotGroupVolumeRequest>("SnapshotGroupVolume", new SnapshotGroupVolumeRequest(), cancellationToken);

  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class BaseRequest {
    [System.Xml.Serialization.XmlNamespaceDeclarations]
    public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
      new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:GroupRenderingControl:1"), });
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class GetGroupMuteRequest : BaseRequest {

    /// <summary>
    /// InstanceID should always be `0`
    /// </summary>
    public int InstanceID { get; set; } = 0;
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlType("GetGroupMuteResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:GroupRenderingControl:1")]
  public class GetGroupMuteResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public bool CurrentMute { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class GetGroupVolumeRequest : BaseRequest {

    /// <summary>
    /// InstanceID should always be `0`
    /// </summary>
    public int InstanceID { get; set; } = 0;
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlType("GetGroupVolumeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:GroupRenderingControl:1")]
  public class GetGroupVolumeResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public int CurrentVolume { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class SetGroupMuteRequest : BaseRequest {

    /// <summary>
    /// InstanceID should always be `0`
    /// </summary>
    public int InstanceID { get; set; } = 0;

    public bool DesiredMute { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class SetGroupVolumeRequest : BaseRequest {

    /// <summary>
    /// InstanceID should always be `0`
    /// </summary>
    public int InstanceID { get; set; } = 0;

    /// <summary>
    /// New volume between 0 and 100
    /// </summary>
    public int DesiredVolume { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class SetRelativeGroupVolumeRequest : BaseRequest {

    /// <summary>
    /// InstanceID should always be `0`
    /// </summary>
    public int InstanceID { get; set; } = 0;

    /// <summary>
    /// Number between -100 and +100
    /// </summary>
    public int Adjustment { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlType("SetRelativeGroupVolumeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:GroupRenderingControl:1")]
  public class SetRelativeGroupVolumeResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public int NewVolume { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class SnapshotGroupVolumeRequest : BaseRequest {

    /// <summary>
    /// InstanceID should always be `0`
    /// </summary>
    public int InstanceID { get; set; } = 0;
  }
}