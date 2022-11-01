namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// DevicePropertiesService  - Modify device properties, like LED status and stereo pairs
/// </summary>
public partial class DevicePropertiesService : SonosBaseService
{
    /// <summary>
    /// Create a new DevicePropertiesService
    /// </summary>
    /// <param name="sonosUri">Base URL of the speaker</param>
    /// <param name="httpClient">Optionally, a custom HttpClient.</param>
    public DevicePropertiesService(SonosServiceOptions options) : base("DeviceProperties", "/DeviceProperties/Control", "/DeviceProperties/Event", options) { }


    /// <summary>
    /// AddBondedZones
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> AddBondedZones(AddBondedZonesRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<AddBondedZonesRequest>("AddBondedZones", request, cancellationToken);

    /// <summary>
    /// AddHTSatellite
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> AddHTSatellite(AddHTSatelliteRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<AddHTSatelliteRequest>("AddHTSatellite", request, cancellationToken);

    /// <summary>
    /// Create a stereo pair (left, right speakers), right one becomes hidden
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>No all speakers support StereoPairs</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> CreateStereoPair(CreateStereoPairRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<CreateStereoPairRequest>("CreateStereoPair", request, cancellationToken);

    /// <summary>
    /// EnterConfigMode
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>EnterConfigModeResponse</returns>
    public Task<EnterConfigModeResponse> EnterConfigMode(EnterConfigModeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<EnterConfigModeRequest, EnterConfigModeResponse>("EnterConfigMode", request, cancellationToken);

    /// <summary>
    /// ExitConfigMode
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> ExitConfigMode(ExitConfigModeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<ExitConfigModeRequest>("ExitConfigMode", request, cancellationToken);

    /// <summary>
    /// GetAutoplayLinkedZones
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetAutoplayLinkedZonesResponse</returns>
    public Task<GetAutoplayLinkedZonesResponse> GetAutoplayLinkedZones(GetAutoplayLinkedZonesRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetAutoplayLinkedZonesRequest, GetAutoplayLinkedZonesResponse>("GetAutoplayLinkedZones", request, cancellationToken);

    /// <summary>
    /// GetAutoplayRoomUUID
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetAutoplayRoomUUIDResponse</returns>
    public Task<GetAutoplayRoomUUIDResponse> GetAutoplayRoomUUID(GetAutoplayRoomUUIDRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetAutoplayRoomUUIDRequest, GetAutoplayRoomUUIDResponse>("GetAutoplayRoomUUID", request, cancellationToken);

    /// <summary>
    /// GetAutoplayVolume
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetAutoplayVolumeResponse</returns>
    public Task<GetAutoplayVolumeResponse> GetAutoplayVolume(GetAutoplayVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetAutoplayVolumeRequest, GetAutoplayVolumeResponse>("GetAutoplayVolume", request, cancellationToken);

    /// <summary>
    /// Get the current button lock state
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetButtonLockStateResponse</returns>
    public Task<GetButtonLockStateResponse> GetButtonLockState(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetButtonLockStateResponse>("GetButtonLockState", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetButtonState
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetButtonStateResponse</returns>
    public Task<GetButtonStateResponse> GetButtonState(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetButtonStateResponse>("GetButtonState", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetHouseholdID
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetHouseholdIDResponse</returns>
    public Task<GetHouseholdIDResponse> GetHouseholdID(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetHouseholdIDResponse>("GetHouseholdID", new BaseRequest(), cancellationToken);

    /// <summary>
    /// Get the current LED state
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetLEDStateResponse</returns>
    public Task<GetLEDStateResponse> GetLEDState(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetLEDStateResponse>("GetLEDState", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetUseAutoplayVolume
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetUseAutoplayVolumeResponse</returns>
    public Task<GetUseAutoplayVolumeResponse> GetUseAutoplayVolume(GetUseAutoplayVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetUseAutoplayVolumeRequest, GetUseAutoplayVolumeResponse>("GetUseAutoplayVolume", request, cancellationToken);

    /// <summary>
    /// GetZoneAttributes
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetZoneAttributesResponse</returns>
    public Task<GetZoneAttributesResponse> GetZoneAttributes(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetZoneAttributesResponse>("GetZoneAttributes", new BaseRequest(), cancellationToken);

    /// <summary>
    /// Get information about this specific speaker
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetZoneInfoResponse</returns>
    public Task<GetZoneInfoResponse> GetZoneInfo(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetZoneInfoResponse>("GetZoneInfo", new BaseRequest(), cancellationToken);

    /// <summary>
    /// RemoveBondedZones
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RemoveBondedZones(RemoveBondedZonesRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RemoveBondedZonesRequest>("RemoveBondedZones", request, cancellationToken);

    /// <summary>
    /// RemoveHTSatellite
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RemoveHTSatellite(RemoveHTSatelliteRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RemoveHTSatelliteRequest>("RemoveHTSatellite", request, cancellationToken);

    /// <summary>
    /// RoomDetectionStartChirping
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>RoomDetectionStartChirpingResponse</returns>
    public Task<RoomDetectionStartChirpingResponse> RoomDetectionStartChirping(RoomDetectionStartChirpingRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RoomDetectionStartChirpingRequest, RoomDetectionStartChirpingResponse>("RoomDetectionStartChirping", request, cancellationToken);

    /// <summary>
    /// RoomDetectionStopChirping
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RoomDetectionStopChirping(RoomDetectionStopChirpingRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RoomDetectionStopChirpingRequest>("RoomDetectionStopChirping", request, cancellationToken);

    /// <summary>
    /// Separate a stereo pair
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>No all speakers support StereoPairs</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> SeparateStereoPair(SeparateStereoPairRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SeparateStereoPairRequest>("SeparateStereoPair", request, cancellationToken);

    /// <summary>
    /// SetAutoplayLinkedZones
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetAutoplayLinkedZones(SetAutoplayLinkedZonesRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetAutoplayLinkedZonesRequest>("SetAutoplayLinkedZones", request, cancellationToken);

    /// <summary>
    /// SetAutoplayRoomUUID
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetAutoplayRoomUUID(SetAutoplayRoomUUIDRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetAutoplayRoomUUIDRequest>("SetAutoplayRoomUUID", request, cancellationToken);

    /// <summary>
    /// SetAutoplayVolume
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetAutoplayVolume(SetAutoplayVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetAutoplayVolumeRequest>("SetAutoplayVolume", request, cancellationToken);

    /// <summary>
    /// Set the button lock state
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetButtonLockState(SetButtonLockStateRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetButtonLockStateRequest>("SetButtonLockState", request, cancellationToken);

    /// <summary>
    /// Set the LED state
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetLEDState(SetLEDStateRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetLEDStateRequest>("SetLEDState", request, cancellationToken);

    /// <summary>
    /// SetUseAutoplayVolume
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetUseAutoplayVolume(SetUseAutoplayVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetUseAutoplayVolumeRequest>("SetUseAutoplayVolume", request, cancellationToken);

    /// <summary>
    /// SetZoneAttributes
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetZoneAttributes(SetZoneAttributesRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetZoneAttributesRequest>("SetZoneAttributes", request, cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:DeviceProperties:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class AddBondedZonesRequest : BaseRequest
    {

        public string ChannelMapSet { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class AddHTSatelliteRequest : BaseRequest
    {

        public string HTSatChanMapSet { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class CreateStereoPairRequest : BaseRequest
    {

        /// <summary>
        /// example: `RINCON_B8E9375831C001400:LF,LF;RINCON_000E58FE3AEA01400:RF,RF`
        /// </summary>
        public string ChannelMapSet { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class EnterConfigModeRequest : BaseRequest
    {

        public string Mode { get; set; }

        public string Options { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("EnterConfigModeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class EnterConfigModeResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string State { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class ExitConfigModeRequest : BaseRequest
    {

        public string Options { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetAutoplayLinkedZonesRequest : BaseRequest
    {

        public string Source { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetAutoplayLinkedZonesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetAutoplayLinkedZonesResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool IncludeLinkedZones { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetAutoplayRoomUUIDRequest : BaseRequest
    {

        public string Source { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetAutoplayRoomUUIDResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetAutoplayRoomUUIDResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string RoomUUID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetAutoplayVolumeRequest : BaseRequest
    {

        public string Source { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetAutoplayVolumeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetAutoplayVolumeResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentVolume { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetButtonLockStateResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetButtonLockStateResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentButtonLockState { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetButtonStateResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetButtonStateResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string State { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetHouseholdIDResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetHouseholdIDResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentHouseholdID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetLEDStateResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetLEDStateResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentLEDState { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetUseAutoplayVolumeRequest : BaseRequest
    {

        public string Source { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetUseAutoplayVolumeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetUseAutoplayVolumeResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool UseVolume { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetZoneAttributesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetZoneAttributesResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentZoneName { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentIcon { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentConfiguration { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetZoneInfoResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class GetZoneInfoResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string SerialNumber { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string SoftwareVersion { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string DisplaySoftwareVersion { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string HardwareVersion { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string IPAddress { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string MACAddress { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CopyrightInfo { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string ExtraInfo { get; set; }

        /// <summary>
        /// SPDIF input, `0` not connected / `2` stereo / `7` Dolby 2.0 / `18` dolby 5.1 / `21` not listening / `22` silence
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int HTAudioIn { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int Flags { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RemoveBondedZonesRequest : BaseRequest
    {

        public string ChannelMapSet { get; set; }

        public bool KeepGrouped { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RemoveHTSatelliteRequest : BaseRequest
    {

        public string SatRoomUUID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RoomDetectionStartChirpingRequest : BaseRequest
    {

        public int Channel { get; set; }

        public int DurationMilliseconds { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("RoomDetectionStartChirpingResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:DeviceProperties:1")]
    public partial class RoomDetectionStartChirpingResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int PlayId { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool ChirpIfPlayingSwappableAudio { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RoomDetectionStopChirpingRequest : BaseRequest
    {

        public int PlayId { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SeparateStereoPairRequest : BaseRequest
    {

        /// <summary>
        /// example: `RINCON_B8E9375831C001400:LF,LF;RINCON_000E58FE3AEA01400:RF,RF`
        /// </summary>
        public string ChannelMapSet { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetAutoplayLinkedZonesRequest : BaseRequest
    {

        public bool IncludeLinkedZones { get; set; }

        public string Source { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetAutoplayRoomUUIDRequest : BaseRequest
    {

        public string RoomUUID { get; set; }

        public string Source { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetAutoplayVolumeRequest : BaseRequest
    {

        public int Volume { get; set; }

        public string Source { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetButtonLockStateRequest : BaseRequest
    {

        public string DesiredButtonLockState { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetLEDStateRequest : BaseRequest
    {

        public string DesiredLEDState { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetUseAutoplayVolumeRequest : BaseRequest
    {

        public bool UseVolume { get; set; }

        public string Source { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetZoneAttributesRequest : BaseRequest
    {

        public string DesiredZoneName { get; set; }

        public string DesiredIcon { get; set; }

        public string DesiredConfiguration { get; set; }
    }
}
