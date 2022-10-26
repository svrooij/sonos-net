namespace Sonos.Base.Services;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// ZoneGroupTopologyService  - Zone config stuff, eg getting all the configured sonos zones
/// </summary>
public partial class ZoneGroupTopologyService : SonosBaseService {
  /// <summary>
  /// Create a new ZoneGroupTopologyService
  /// </summary>
  /// <param name="sonosUri">Base URL of the speaker</param>
  /// <param name="httpClient">Optionally, a custom HttpClient.</param>
  public ZoneGroupTopologyService(Uri sonosUri, HttpClient? httpClient): base("ZoneGroupTopology", "/ZoneGroupTopology/Control", "/ZoneGroupTopology/Event", sonosUri, httpClient) {}


  /// <summary>
  /// BeginSoftwareUpdate
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> BeginSoftwareUpdate(BeginSoftwareUpdateRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<BeginSoftwareUpdateRequest>("BeginSoftwareUpdate", request, cancellationToken);

  /// <summary>
  /// CheckForUpdate
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>CheckForUpdateResponse</returns>
  public Task<CheckForUpdateResponse> CheckForUpdate(CheckForUpdateRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<CheckForUpdateRequest, CheckForUpdateResponse>("CheckForUpdate", request, cancellationToken);

  /// <summary>
  /// Get information about the current Zone
  /// </summary>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>GetZoneGroupAttributesResponse</returns>
  public Task<GetZoneGroupAttributesResponse> GetZoneGroupAttributes(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetZoneGroupAttributesResponse>("GetZoneGroupAttributes", new BaseRequest(), cancellationToken);

  /// <summary>
  /// Get all the Sonos groups, (as XML)
  /// </summary>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <remarks>Some libraries also support GetParsedZoneGroupState that parses the xml for you.</remarks>
  /// <returns>GetZoneGroupStateResponse</returns>
  public Task<GetZoneGroupStateResponse> GetZoneGroupState(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetZoneGroupStateResponse>("GetZoneGroupState", new BaseRequest(), cancellationToken);

  /// <summary>
  /// RegisterMobileDevice
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> RegisterMobileDevice(RegisterMobileDeviceRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RegisterMobileDeviceRequest>("RegisterMobileDevice", request, cancellationToken);

  /// <summary>
  /// ReportAlarmStartedRunning
  /// </summary>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> ReportAlarmStartedRunning(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest>("ReportAlarmStartedRunning", new BaseRequest(), cancellationToken);

  /// <summary>
  /// ReportUnresponsiveDevice
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>Success boolean</returns>
  public Task<bool> ReportUnresponsiveDevice(ReportUnresponsiveDeviceRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<ReportUnresponsiveDeviceRequest>("ReportUnresponsiveDevice", request, cancellationToken);

  /// <summary>
  /// SubmitDiagnostics
  /// </summary>
  /// <param name="request">Body payload</param>
  /// <param name="cancellationToken">CancellationToken</param>
  /// <returns>SubmitDiagnosticsResponse</returns>
  public Task<SubmitDiagnosticsResponse> SubmitDiagnostics(SubmitDiagnosticsRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SubmitDiagnosticsRequest, SubmitDiagnosticsResponse>("SubmitDiagnostics", request, cancellationToken);

  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class BaseRequest {
    [System.Xml.Serialization.XmlNamespaceDeclarations]
    public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
      new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:ZoneGroupTopology:1"), });
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class BeginSoftwareUpdateRequest : BaseRequest {

    public string UpdateURL { get; set; }

    public int Flags { get; set; }

    public string ExtraOptions { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class CheckForUpdateRequest : BaseRequest {

    public string UpdateType { get; set; }

    public bool CachedOnly { get; set; }

    public string Version { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlTypeAttribute("CheckForUpdateResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ZoneGroupTopology:1")]
  public class CheckForUpdateResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string UpdateItem { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlTypeAttribute("GetZoneGroupAttributesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ZoneGroupTopology:1")]
  public class GetZoneGroupAttributesResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string CurrentZoneGroupName { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string CurrentZoneGroupID { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string CurrentZonePlayerUUIDsInGroup { get; set; }

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string CurrentMuseHouseholdId { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlTypeAttribute("GetZoneGroupStateResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ZoneGroupTopology:1")]
  public class GetZoneGroupStateResponse {

    /// <summary>
    /// xml string, see remarks
    /// </summary>
    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public string ZoneGroupState { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class RegisterMobileDeviceRequest : BaseRequest {

    public string MobileDeviceName { get; set; }

    public string MobileDeviceUDN { get; set; }

    public string MobileIPAndPort { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class ReportUnresponsiveDeviceRequest : BaseRequest {

    public string DeviceUUID { get; set; }

    public string DesiredAction { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlRoot(Namespace = "")]
  public class SubmitDiagnosticsRequest : BaseRequest {

    public bool IncludeControllers { get; set; }

    public string Type { get; set; }
  }

  [System.Serializable()]
  [System.Xml.Serialization.XmlTypeAttribute("SubmitDiagnosticsResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ZoneGroupTopology:1")]
  public class SubmitDiagnosticsResponse {

    [System.Xml.Serialization.XmlElement(Namespace = "")]
    public int DiagnosticID { get; set; }
  }
}