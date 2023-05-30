/*
 * Sonos-net RenderingControlService
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
/// RenderingControlService  - Volume related controls
/// </summary>
public partial class RenderingControlService : SonosBaseService<RenderingControlService.IRenderingControlEvent>
{
    /// <summary>
    /// Create a new RenderingControlService
    /// </summary>
    /// <param name="options">Service options</param>
    public RenderingControlService(SonosServiceOptions options) : base(SonosService.RenderingControl, "/MediaRenderer/RenderingControl/Control", "/MediaRenderer/RenderingControl/Event", options) { }


    /// <summary>
    /// Get bass level between -10 and 10
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetBassResponse</returns>
    public Task<GetBassResponse> GetBass(CancellationToken cancellationToken = default) => ExecuteRequest<GetBassRequest, GetBassResponse>(new GetBassRequest(), cancellationToken);

    /// <summary>
    /// Get equalizer value
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Not all EQ types are available on every speaker</remarks>
    /// <returns>GetEQResponse</returns>
    public Task<GetEQResponse> GetEQ(GetEQRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetEQRequest, GetEQResponse>(request, cancellationToken);

    /// <summary>
    /// GetHeadphoneConnected
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetHeadphoneConnectedResponse</returns>
    public Task<GetHeadphoneConnectedResponse> GetHeadphoneConnected(CancellationToken cancellationToken = default) => ExecuteRequest<GetHeadphoneConnectedRequest, GetHeadphoneConnectedResponse>(new GetHeadphoneConnectedRequest(), cancellationToken);

    /// <summary>
    /// Whether or not Loudness is on
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetLoudnessResponse</returns>
    public Task<GetLoudnessResponse> GetLoudness(GetLoudnessRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetLoudnessRequest, GetLoudnessResponse>(request, cancellationToken);

    /// <summary>
    /// GetMute
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetMuteResponse</returns>
    public Task<GetMuteResponse> GetMute(GetMuteRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetMuteRequest, GetMuteResponse>(request, cancellationToken);

    /// <summary>
    /// GetOutputFixed
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetOutputFixedResponse</returns>
    public Task<GetOutputFixedResponse> GetOutputFixed(CancellationToken cancellationToken = default) => ExecuteRequest<GetOutputFixedRequest, GetOutputFixedResponse>(new GetOutputFixedRequest(), cancellationToken);

    /// <summary>
    /// GetRoomCalibrationStatus
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetRoomCalibrationStatusResponse</returns>
    public Task<GetRoomCalibrationStatusResponse> GetRoomCalibrationStatus(CancellationToken cancellationToken = default) => ExecuteRequest<GetRoomCalibrationStatusRequest, GetRoomCalibrationStatusResponse>(new GetRoomCalibrationStatusRequest(), cancellationToken);

    /// <summary>
    /// GetSupportsOutputFixed
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetSupportsOutputFixedResponse</returns>
    public Task<GetSupportsOutputFixedResponse> GetSupportsOutputFixed(CancellationToken cancellationToken = default) => ExecuteRequest<GetSupportsOutputFixedRequest, GetSupportsOutputFixedResponse>(new GetSupportsOutputFixedRequest(), cancellationToken);

    /// <summary>
    /// Get treble
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetTrebleResponse</returns>
    public Task<GetTrebleResponse> GetTreble(CancellationToken cancellationToken = default) => ExecuteRequest<GetTrebleRequest, GetTrebleResponse>(new GetTrebleRequest(), cancellationToken);

    /// <summary>
    /// Get volume
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetVolumeResponse</returns>
    public Task<GetVolumeResponse> GetVolume(GetVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetVolumeRequest, GetVolumeResponse>(request, cancellationToken);

    /// <summary>
    /// GetVolumeDB
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetVolumeDBResponse</returns>
    public Task<GetVolumeDBResponse> GetVolumeDB(GetVolumeDBRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetVolumeDBRequest, GetVolumeDBResponse>(request, cancellationToken);

    /// <summary>
    /// GetVolumeDBRange
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetVolumeDBRangeResponse</returns>
    public Task<GetVolumeDBRangeResponse> GetVolumeDBRange(GetVolumeDBRangeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetVolumeDBRangeRequest, GetVolumeDBRangeResponse>(request, cancellationToken);

    /// <summary>
    /// RampToVolume
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>RampToVolumeResponse</returns>
    public Task<RampToVolumeResponse> RampToVolume(RampToVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RampToVolumeRequest, RampToVolumeResponse>(request, cancellationToken);

    /// <summary>
    /// ResetBasicEQ
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>ResetBasicEQResponse</returns>
    public Task<ResetBasicEQResponse> ResetBasicEQ(CancellationToken cancellationToken = default) => ExecuteRequest<ResetBasicEQRequest, ResetBasicEQResponse>(new ResetBasicEQRequest(), cancellationToken);

    /// <summary>
    /// ResetExtEQ
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> ResetExtEQ(ResetExtEQRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// RestoreVolumePriorToRamp
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RestoreVolumePriorToRamp(RestoreVolumePriorToRampRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// Set bass level, between -10 and 10
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetBass(SetBassRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// SetChannelMap
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetChannelMap(SetChannelMapRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// Set equalizer value for different types
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Not supported by all speakers, TV related</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> SetEQ(SetEQRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// Set loudness on / off
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetLoudness(SetLoudnessRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// SetMute
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetMute(SetMuteRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// SetOutputFixed
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetOutputFixed(SetOutputFixedRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// SetRelativeVolume
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>SetRelativeVolumeResponse</returns>
    public Task<SetRelativeVolumeResponse> SetRelativeVolume(SetRelativeVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetRelativeVolumeRequest, SetRelativeVolumeResponse>(request, cancellationToken);

    /// <summary>
    /// SetRoomCalibrationStatus
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetRoomCalibrationStatus(SetRoomCalibrationStatusRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// SetRoomCalibrationX
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetRoomCalibrationX(SetRoomCalibrationXRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// Set treble level
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetTreble(SetTrebleRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// SetVolume
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetVolume(SetVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    /// <summary>
    /// SetVolumeDB
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetVolumeDB(SetVolumeDBRequest request, CancellationToken cancellationToken = default) => ExecuteRequest(request, cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:RenderingControl:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetBass")]
    public class GetBassRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetBassResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetBassResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentBass { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetEQ")]
    public class GetEQRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// Allowed values `DialogLevel` (bool) / `MusicSurroundLevel` (-15/+15) /  `NightMode` (bool) / `SubGain` (-10/+10) / `SurroundEnable` (bool) / `SurroundLevel` (-15/+15) / `SurroundMode` (0 = ambient, 1 = full) / `HeightChannelLevel` (-10/+10)
        /// </summary>
        public string EQType { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetEQResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetEQResponse
    {
        /// <summary>
        /// Booleans return `1` / `0`, rest number as specified
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentValue { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetHeadphoneConnected")]
    public class GetHeadphoneConnectedRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetHeadphoneConnectedResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetHeadphoneConnectedResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool CurrentHeadphoneConnected { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetLoudness")]
    public class GetLoudnessRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetLoudnessResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetLoudnessResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool CurrentLoudness { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetMute")]
    public class GetMuteRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetMuteResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetMuteResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool CurrentMute { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetOutputFixed")]
    public class GetOutputFixedRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetOutputFixedResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetOutputFixedResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool CurrentFixed { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetRoomCalibrationStatus")]
    public class GetRoomCalibrationStatusRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetRoomCalibrationStatusResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetRoomCalibrationStatusResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool RoomCalibrationEnabled { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool RoomCalibrationAvailable { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetSupportsOutputFixed")]
    public class GetSupportsOutputFixedRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetSupportsOutputFixedResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetSupportsOutputFixedResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool CurrentSupportsFixed { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetTreble")]
    public class GetTrebleRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetTrebleResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetTrebleResponse
    {
        /// <summary>
        /// Number between -10 and 10
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentTreble { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetVolume")]
    public class GetVolumeRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetVolumeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetVolumeResponse
    {
        /// <summary>
        /// Number between 0 and 100
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentVolume { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetVolumeDB")]
    public class GetVolumeDBRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetVolumeDBResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetVolumeDBResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentVolume { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "GetVolumeDBRange")]
    public class GetVolumeDBRangeRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetVolumeDBRangeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class GetVolumeDBRangeResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int MinValue { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int MaxValue { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "RampToVolume")]
    public class RampToVolumeRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }

        public string RampType { get; set; }

        public int DesiredVolume { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public bool ResetVolumeAfter { get; set; }

        /// <remarks>Noting to see here, XmlSerializer issue, use 'ResetVolumeAfter'</remarks>
        [System.Xml.Serialization.XmlElement("ResetVolumeAfter")]
        public int _ResetVolumeAfter { get { return ResetVolumeAfter ? 1 : 0; } set { ResetVolumeAfter = value == 1; } }

        public string ProgramURI { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("RampToVolumeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class RampToVolumeResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int RampTime { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "ResetBasicEQ")]
    public class ResetBasicEQRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("ResetBasicEQResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class ResetBasicEQResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int Bass { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int Treble { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool Loudness { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int LeftVolume { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int RightVolume { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "ResetExtEQ")]
    public class ResetExtEQRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string EQType { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "RestoreVolumePriorToRamp")]
    public class RestoreVolumePriorToRampRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetBass")]
    public class SetBassRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public int DesiredBass { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetChannelMap")]
    public class SetChannelMapRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string ChannelMap { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetEQ")]
    public class SetEQRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// Allowed values `DialogLevel` (bool) / `MusicSurroundLevel` (-15/+15) /  `NightMode` (bool) / `SubGain` (-10/+10) / `SurroundEnable` (bool) / `SurroundLevel` (-15/+15) / `SurroundMode` (0 = ambient, 1 = full) / `HeightChannelLevel` (-10/+10)
        /// </summary>
        public string EQType { get; set; }

        /// <summary>
        /// Booleans required `1` for true or `0` for false, rest number as specified
        /// </summary>
        public int DesiredValue { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetLoudness")]
    public class SetLoudnessRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public bool DesiredLoudness { get; set; }

        /// <remarks>Noting to see here, XmlSerializer issue, use 'DesiredLoudness'</remarks>
        [System.Xml.Serialization.XmlElement("DesiredLoudness")]
        public int _DesiredLoudness { get { return DesiredLoudness ? 1 : 0; } set { DesiredLoudness = value == 1; } }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetMute")]
    public class SetMuteRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public bool DesiredMute { get; set; }

        /// <remarks>Noting to see here, XmlSerializer issue, use 'DesiredMute'</remarks>
        [System.Xml.Serialization.XmlElement("DesiredMute")]
        public int _DesiredMute { get { return DesiredMute ? 1 : 0; } set { DesiredMute = value == 1; } }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetOutputFixed")]
    public class SetOutputFixedRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        [System.Xml.Serialization.XmlIgnore]
        public bool DesiredFixed { get; set; }

        /// <remarks>Noting to see here, XmlSerializer issue, use 'DesiredFixed'</remarks>
        [System.Xml.Serialization.XmlElement("DesiredFixed")]
        public int _DesiredFixed { get { return DesiredFixed ? 1 : 0; } set { DesiredFixed = value == 1; } }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetRelativeVolume")]
    public class SetRelativeVolumeRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }

        public int Adjustment { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("SetRelativeVolumeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:RenderingControl:1")]
    public partial class SetRelativeVolumeResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewVolume { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetRoomCalibrationStatus")]
    public class SetRoomCalibrationStatusRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        [System.Xml.Serialization.XmlIgnore]
        public bool RoomCalibrationEnabled { get; set; }

        /// <remarks>Noting to see here, XmlSerializer issue, use 'RoomCalibrationEnabled'</remarks>
        [System.Xml.Serialization.XmlElement("RoomCalibrationEnabled")]
        public int _RoomCalibrationEnabled { get { return RoomCalibrationEnabled ? 1 : 0; } set { RoomCalibrationEnabled = value == 1; } }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetRoomCalibrationX")]
    public class SetRoomCalibrationXRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string CalibrationID { get; set; }

        public string Coefficients { get; set; }

        public string CalibrationMode { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetTreble")]
    public class SetTrebleRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// between -10 and 10
        /// </summary>
        public int DesiredTreble { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetVolume")]
    public class SetVolumeRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }

        public int DesiredVolume { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/RenderingControl/Control", "RenderingControl", "SetVolumeDB")]
    public class SetVolumeDBRequest : BaseRequest
    {
        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Channel { get; set; }

        public int DesiredVolume { get; set; }
    }

    /// <summary>
    /// RenderingControl is set to might emit these properties in events
    /// </summary>
    public partial interface IRenderingControlEvent : IServiceEvent
    {
        public string? AudioDelay { get; }

        public string? AudioDelayLeftRear { get; }

        public string? AudioDelayRightRear { get; }

        public int? Bass { get; }

        public string? DialogLevel { get; }

        public int? EQValue { get; }

        public bool? HeadphoneConnected { get; }

        public int? HeightChannelLevel { get; }

        public string? LastChange { get; }

        public bool? Loudness { get; }

        public string? MusicSurroundLevel { get; }

        public ChannelMapBool? Mute { get; }

        public bool? NightMode { get; }

        public bool? OutputFixed { get; }

        public string? PresetNameList { get; }

        public bool? RoomCalibrationAvailable { get; }

        public string? RoomCalibrationCalibrationMode { get; }

        public string? RoomCalibrationCoefficients { get; }

        public bool? RoomCalibrationEnabled { get; }

        public string? RoomCalibrationID { get; }

        public int? SpeakerSize { get; }

        public string? SubCrossover { get; }

        public bool? SubEnabled { get; }

        public string? SubGain { get; }

        public string? SubPolarity { get; }

        public bool? SupportsOutputFixed { get; }

        public bool? SurroundEnabled { get; }

        public string? SurroundLevel { get; }

        public string? SurroundMode { get; }

        public int? Treble { get; }

        public ChannelMapInt? Volume { get; }

        public int? VolumeDB { get; }
    }
}
