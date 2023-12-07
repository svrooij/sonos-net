/*
 * Sonos-net ZoneGroupTopologyService
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
/// ZoneGroupTopologyService  - Zone config stuff, eg getting all the configured sonos zones
/// </summary>
public partial class ZoneGroupTopologyService : SonosBaseService<ZoneGroupTopologyService.IZoneGroupTopologyEvent>
{
    /// <summary>
    /// Create a new ZoneGroupTopologyService
    /// </summary>
    /// <param name="options">Service options</param>
    public ZoneGroupTopologyService(SonosServiceOptions options) : base(SonosService.ZoneGroupTopology, "/ZoneGroupTopology/Control", "/ZoneGroupTopology/Event", options) { }


    /// <summary>
    /// BeginSoftwareUpdate
    /// </summary>
    /// <param name="request"><see cref="BeginSoftwareUpdateRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>Success boolean</returns>
    public Task<bool> BeginSoftwareUpdateAsync(BeginSoftwareUpdateRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync(request, cancellationToken, "BeginSoftwareUpdate");

    /// <summary>
    /// CheckForUpdate
    /// </summary>
    /// <param name="request"><see cref="CheckForUpdateRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>CheckForUpdateResponse</returns>
    public Task<CheckForUpdateResponse> CheckForUpdateAsync(CheckForUpdateRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync<CheckForUpdateRequest, CheckForUpdateResponse>(request, cancellationToken, "CheckForUpdate");

    /// <summary>
    /// Get information about the current Zone
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>GetZoneGroupAttributesResponse</returns>
    public Task<GetZoneGroupAttributesResponse> GetZoneGroupAttributesAsync(CancellationToken cancellationToken = default) => ExecuteRequestAsync<BaseRequest, GetZoneGroupAttributesResponse>(new BaseRequest(), cancellationToken, "GetZoneGroupAttributes");

    /// <summary>
    /// Get all the Sonos groups, (as XML)
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <remarks>Some libraries also support GetParsedZoneGroupState that parses the xml for you.</remarks>
    /// <returns>GetZoneGroupStateResponse</returns>
    public Task<GetZoneGroupStateResponse> GetZoneGroupStateAsync(CancellationToken cancellationToken = default) => ExecuteRequestAsync<BaseRequest, GetZoneGroupStateResponse>(new BaseRequest(), cancellationToken, "GetZoneGroupState");

    /// <summary>
    /// RegisterMobileDevice
    /// </summary>
    /// <param name="request"><see cref="RegisterMobileDeviceRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>Success boolean</returns>
    public Task<bool> RegisterMobileDeviceAsync(RegisterMobileDeviceRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync(request, cancellationToken, "RegisterMobileDevice");

    /// <summary>
    /// ReportAlarmStartedRunning
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>Success boolean</returns>
    public Task<bool> ReportAlarmStartedRunningAsync(CancellationToken cancellationToken = default) => ExecuteRequestAsync(new BaseRequest(), cancellationToken, "ReportAlarmStartedRunning");

    /// <summary>
    /// ReportUnresponsiveDevice
    /// </summary>
    /// <param name="request"><see cref="ReportUnresponsiveDeviceRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>Success boolean</returns>
    public Task<bool> ReportUnresponsiveDeviceAsync(ReportUnresponsiveDeviceRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync(request, cancellationToken, "ReportUnresponsiveDevice");

    /// <summary>
    /// SubmitDiagnostics
    /// </summary>
    /// <param name="request"><see cref="SubmitDiagnosticsRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>SubmitDiagnosticsResponse</returns>
    public Task<SubmitDiagnosticsResponse> SubmitDiagnosticsAsync(SubmitDiagnosticsRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync<SubmitDiagnosticsRequest, SubmitDiagnosticsResponse>(request, cancellationToken, "SubmitDiagnostics");

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/ZoneGroupTopology/Control", "ZoneGroupTopology")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:ZoneGroupTopology:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/ZoneGroupTopology/Control", "ZoneGroupTopology", "BeginSoftwareUpdate")]
    public class BeginSoftwareUpdateRequest : BaseRequest
    {
        public string UpdateURL { get; set; }

        public int Flags { get; set; }

        public string ExtraOptions { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/ZoneGroupTopology/Control", "ZoneGroupTopology", "CheckForUpdate")]
    public class CheckForUpdateRequest : BaseRequest
    {
        public string UpdateType { get; set; }

        public bool CachedOnly { get; set; }

        public string Version { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("CheckForUpdateResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ZoneGroupTopology:1")]
    public partial class CheckForUpdateResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string UpdateItem { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetZoneGroupAttributesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ZoneGroupTopology:1")]
    public partial class GetZoneGroupAttributesResponse
    {
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
    [System.Xml.Serialization.XmlType("GetZoneGroupStateResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ZoneGroupTopology:1")]
    public partial class GetZoneGroupStateResponse
    {
        /// <summary>
        /// xml string, see remarks
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string ZoneGroupState { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/ZoneGroupTopology/Control", "ZoneGroupTopology", "RegisterMobileDevice")]
    public class RegisterMobileDeviceRequest : BaseRequest
    {
        public string MobileDeviceName { get; set; }

        public string MobileDeviceUDN { get; set; }

        public string MobileIPAndPort { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/ZoneGroupTopology/Control", "ZoneGroupTopology", "ReportUnresponsiveDevice")]
    public class ReportUnresponsiveDeviceRequest : BaseRequest
    {
        public string DeviceUUID { get; set; }

        public string DesiredAction { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/ZoneGroupTopology/Control", "ZoneGroupTopology", "SubmitDiagnostics")]
    public class SubmitDiagnosticsRequest : BaseRequest
    {
        public bool IncludeControllers { get; set; }

        public string Type { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("SubmitDiagnosticsResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ZoneGroupTopology:1")]
    public partial class SubmitDiagnosticsResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int DiagnosticID { get; set; }
    }

    /// <summary>
    /// ZoneGroupTopology is set to might emit these properties in events
    /// </summary>
    public partial interface IZoneGroupTopologyEvent : IServiceEvent
    {
        public string? AlarmRunSequence { get; }

        public string? AreasUpdateID { get; }

        public string? AvailableSoftwareUpdate { get; }

        public int? DiagnosticID { get; }

        public string? MuseHouseholdId { get; }

        public string? NetsettingsUpdateID { get; }

        public string? SourceAreasUpdateID { get; }

        public string? ThirdPartyMediaServersX { get; }

        public string? ZoneGroupID { get; }

        public string? ZoneGroupName { get; }

        public string? ZoneGroupState { get; }

        public string? ZonePlayerUUIDsInGroup { get; }
    }

    /// <summary>
    /// BeginSoftwareUpdate
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    [Obsolete("This method is obsolete. Use BeginSoftwareUpdateAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<bool> BeginSoftwareUpdate(BeginSoftwareUpdateRequest request, CancellationToken cancellationToken = default) => BeginSoftwareUpdateAsync(request, cancellationToken);

    /// <summary>
    /// CheckForUpdate
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>CheckForUpdateResponse</returns>
    [Obsolete("This method is obsolete. Use CheckForUpdateAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<CheckForUpdateResponse> CheckForUpdate(CheckForUpdateRequest request, CancellationToken cancellationToken = default) => CheckForUpdateAsync(request, cancellationToken);

    /// <summary>
    /// Get information about the current Zone
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetZoneGroupAttributesResponse</returns>
    [Obsolete("This method is obsolete. Use GetZoneGroupAttributesAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<GetZoneGroupAttributesResponse> GetZoneGroupAttributes(CancellationToken cancellationToken = default) => GetZoneGroupAttributesAsync(cancellationToken);

    /// <summary>
    /// Get all the Sonos groups, (as XML)
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Some libraries also support GetParsedZoneGroupState that parses the xml for you.</remarks>
    /// <returns>GetZoneGroupStateResponse</returns>
    [Obsolete("This method is obsolete. Use GetZoneGroupStateAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<GetZoneGroupStateResponse> GetZoneGroupState(CancellationToken cancellationToken = default) => GetZoneGroupStateAsync(cancellationToken);

    /// <summary>
    /// RegisterMobileDevice
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    [Obsolete("This method is obsolete. Use RegisterMobileDeviceAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<bool> RegisterMobileDevice(RegisterMobileDeviceRequest request, CancellationToken cancellationToken = default) => RegisterMobileDeviceAsync(request, cancellationToken);

    /// <summary>
    /// ReportAlarmStartedRunning
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    [Obsolete("This method is obsolete. Use ReportAlarmStartedRunningAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<bool> ReportAlarmStartedRunning(CancellationToken cancellationToken = default) => ReportAlarmStartedRunningAsync(cancellationToken);

    /// <summary>
    /// ReportUnresponsiveDevice
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    [Obsolete("This method is obsolete. Use ReportUnresponsiveDeviceAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<bool> ReportUnresponsiveDevice(ReportUnresponsiveDeviceRequest request, CancellationToken cancellationToken = default) => ReportUnresponsiveDeviceAsync(request, cancellationToken);

    /// <summary>
    /// SubmitDiagnostics
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>SubmitDiagnosticsResponse</returns>
    [Obsolete("This method is obsolete. Use SubmitDiagnosticsAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<SubmitDiagnosticsResponse> SubmitDiagnostics(SubmitDiagnosticsRequest request, CancellationToken cancellationToken = default) => SubmitDiagnosticsAsync(request, cancellationToken);
}
