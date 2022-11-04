namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// AlarmClockService  - Control the sonos alarms and times
/// </summary>
public partial class AlarmClockService : SonosBaseService
{
    /// <summary>
    /// Create a new AlarmClockService
    /// </summary>
    /// <param name="sonosUri">Base URL of the speaker</param>
    /// <param name="httpClient">Optionally, a custom HttpClient.</param>
    public AlarmClockService(SonosServiceOptions options) : base("AlarmClock", "/AlarmClock/Control", "/AlarmClock/Event", options) { }


    /// <summary>
    /// Create a single alarm, all properties are required
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>CreateAlarmResponse</returns>
    public Task<CreateAlarmResponse> CreateAlarm(CreateAlarmRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<CreateAlarmRequest, CreateAlarmResponse>("CreateAlarm", request, cancellationToken);

    /// <summary>
    /// Delete an alarm
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> DestroyAlarm(DestroyAlarmRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<DestroyAlarmRequest>("DestroyAlarm", request, cancellationToken);

    /// <summary>
    /// GetDailyIndexRefreshTime
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetDailyIndexRefreshTimeResponse</returns>
    public Task<GetDailyIndexRefreshTimeResponse> GetDailyIndexRefreshTime(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetDailyIndexRefreshTimeResponse>("GetDailyIndexRefreshTime", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetFormat
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetFormatResponse</returns>
    public Task<GetFormatResponse> GetFormat(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetFormatResponse>("GetFormat", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetHouseholdTimeAtStamp
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetHouseholdTimeAtStampResponse</returns>
    public Task<GetHouseholdTimeAtStampResponse> GetHouseholdTimeAtStamp(GetHouseholdTimeAtStampRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetHouseholdTimeAtStampRequest, GetHouseholdTimeAtStampResponse>("GetHouseholdTimeAtStamp", request, cancellationToken);

    /// <summary>
    /// GetTimeNow
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetTimeNowResponse</returns>
    public Task<GetTimeNowResponse> GetTimeNow(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetTimeNowResponse>("GetTimeNow", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetTimeServer
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetTimeServerResponse</returns>
    public Task<GetTimeServerResponse> GetTimeServer(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetTimeServerResponse>("GetTimeServer", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetTimeZone
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetTimeZoneResponse</returns>
    public Task<GetTimeZoneResponse> GetTimeZone(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetTimeZoneResponse>("GetTimeZone", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetTimeZoneAndRule
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetTimeZoneAndRuleResponse</returns>
    public Task<GetTimeZoneAndRuleResponse> GetTimeZoneAndRule(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetTimeZoneAndRuleResponse>("GetTimeZoneAndRule", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetTimeZoneRule
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetTimeZoneRuleResponse</returns>
    public Task<GetTimeZoneRuleResponse> GetTimeZoneRule(GetTimeZoneRuleRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetTimeZoneRuleRequest, GetTimeZoneRuleResponse>("GetTimeZoneRule", request, cancellationToken);

    /// <summary>
    /// Get the AlarmList as XML
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Some libraries also provide a ListAndParseAlarms where the alarm list xml is parsed</remarks>
    /// <returns>ListAlarmsResponse</returns>
    public Task<ListAlarmsResponse> ListAlarms(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, ListAlarmsResponse>("ListAlarms", new BaseRequest(), cancellationToken);

    /// <summary>
    /// SetDailyIndexRefreshTime
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetDailyIndexRefreshTime(SetDailyIndexRefreshTimeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetDailyIndexRefreshTimeRequest>("SetDailyIndexRefreshTime", request, cancellationToken);

    /// <summary>
    /// SetFormat
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetFormat(SetFormatRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetFormatRequest>("SetFormat", request, cancellationToken);

    /// <summary>
    /// SetTimeNow
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetTimeNow(SetTimeNowRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetTimeNowRequest>("SetTimeNow", request, cancellationToken);

    /// <summary>
    /// SetTimeServer
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetTimeServer(SetTimeServerRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetTimeServerRequest>("SetTimeServer", request, cancellationToken);

    /// <summary>
    /// SetTimeZone
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetTimeZone(SetTimeZoneRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetTimeZoneRequest>("SetTimeZone", request, cancellationToken);

    /// <summary>
    /// Update an alarm, all parameters are required.
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Some libraries support PatchAlarm where you can update a single parameter</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> UpdateAlarm(UpdateAlarmRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<UpdateAlarmRequest>("UpdateAlarm", request, cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:AlarmClock:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class CreateAlarmRequest : BaseRequest
    {

        /// <summary>
        /// The start time as `hh:mm:ss`
        /// </summary>
        public string StartLocalTime { get; set; }

        /// <summary>
        /// The duration as `hh:mm:ss`
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Repeat this alarm on
        /// </summary>
        public string Recurrence { get; set; }

        /// <summary>
        /// Alarm enabled after creation
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The UUID of the speaker you want this alarm for
        /// </summary>
        public string RoomUUID { get; set; }

        /// <summary>
        /// The sound uri
        /// </summary>
        public string ProgramURI { get; set; }

        /// <summary>
        /// The sound metadata, can be empty string
        /// </summary>
        public string ProgramMetaData { get; set; }

        /// <summary>
        /// Generate ProgramMetaData xml from Didl data
        /// </summary>
        public Metadata.Didl ProgramMetaDataObject
        {
            set
            {
                ProgramMetaData = Metadata.DidlSerializer.SerializeMetadata(value);
            }
        }

        /// <summary>
        /// Alarm play mode
        /// </summary>
        public string PlayMode { get; set; }

        /// <summary>
        /// Volume between 0 and 100
        /// </summary>
        public int Volume { get; set; }

        /// <summary>
        /// Should grouped players also play the alarm?
        /// </summary>
        public bool IncludeLinkedZones { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("CreateAlarmResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class CreateAlarmResponse
    {

        /// <summary>
        /// The ID of the new alarm
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int AssignedID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class DestroyAlarmRequest : BaseRequest
    {

        /// <summary>
        /// The Alarm ID from ListAlarms
        /// </summary>
        public int ID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetDailyIndexRefreshTimeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class GetDailyIndexRefreshTimeResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentDailyIndexRefreshTime { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetFormatResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class GetFormatResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentTimeFormat { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentDateFormat { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetHouseholdTimeAtStampRequest : BaseRequest
    {

        public string TimeStamp { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetHouseholdTimeAtStampResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class GetHouseholdTimeAtStampResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string HouseholdUTCTime { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetTimeNowResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class GetTimeNowResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentUTCTime { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentLocalTime { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentTimeZone { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentTimeGeneration { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetTimeServerResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class GetTimeServerResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentTimeServer { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetTimeZoneResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class GetTimeZoneResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int Index { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool AutoAdjustDst { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetTimeZoneAndRuleResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class GetTimeZoneAndRuleResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int Index { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool AutoAdjustDst { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentTimeZone { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetTimeZoneRuleRequest : BaseRequest
    {

        public int Index { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetTimeZoneRuleResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class GetTimeZoneRuleResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string TimeZone { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("ListAlarmsResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AlarmClock:1")]
    public partial class ListAlarmsResponse
    {

        /// <summary>
        /// xml string, see remarks
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentAlarmList { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentAlarmListVersion { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetDailyIndexRefreshTimeRequest : BaseRequest
    {

        public string DesiredDailyIndexRefreshTime { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetFormatRequest : BaseRequest
    {

        public string DesiredTimeFormat { get; set; }

        public string DesiredDateFormat { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetTimeNowRequest : BaseRequest
    {

        public string DesiredTime { get; set; }

        public string TimeZoneForDesiredTime { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetTimeServerRequest : BaseRequest
    {

        public string DesiredTimeServer { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetTimeZoneRequest : BaseRequest
    {

        public int Index { get; set; }

        public bool AutoAdjustDst { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class UpdateAlarmRequest : BaseRequest
    {

        /// <summary>
        /// The ID of the alarm see ListAlarms
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The start time as `hh:mm:ss`
        /// </summary>
        public string StartLocalTime { get; set; }

        /// <summary>
        /// The duration as `hh:mm:ss`
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Repeat this alarm on
        /// </summary>
        public string Recurrence { get; set; }

        /// <summary>
        /// Alarm enabled after creation
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The UUID of the speaker you want this alarm for
        /// </summary>
        public string RoomUUID { get; set; }

        /// <summary>
        /// The sound uri
        /// </summary>
        public string ProgramURI { get; set; }

        /// <summary>
        /// The sound metadata, can be empty string
        /// </summary>
        public string ProgramMetaData { get; set; }

        /// <summary>
        /// Generate ProgramMetaData xml from Didl data
        /// </summary>
        public Metadata.Didl ProgramMetaDataObject
        {
            set
            {
                ProgramMetaData = Metadata.DidlSerializer.SerializeMetadata(value);
            }
        }

        /// <summary>
        /// Alarm play mode
        /// </summary>
        public string PlayMode { get; set; }

        /// <summary>
        /// Volume between 0 and 100
        /// </summary>
        public int Volume { get; set; }

        /// <summary>
        /// Should grouped players also play the alarm?
        /// </summary>
        public bool IncludeLinkedZones { get; set; }
    }

    private readonly static Dictionary<int, SonosUpnpError> serviceSpecificErrors = new Dictionary<int, SonosUpnpError>{
        { 801, new SonosUpnpError(801, "Duplicate alarm time") },
    };

    internal override Dictionary<int, SonosUpnpError> ServiceErrors => serviceSpecificErrors.Merge(base.ServiceErrors);
}
