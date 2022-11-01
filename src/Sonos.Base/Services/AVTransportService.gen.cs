namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// AVTransportService  - Service that controls stuff related to transport (play/pause/next/special urls)
/// </summary>
public partial class AVTransportService : SonosBaseService
{
    /// <summary>
    /// Create a new AVTransportService
    /// </summary>
    /// <param name="sonosUri">Base URL of the speaker</param>
    /// <param name="httpClient">Optionally, a custom HttpClient.</param>
    public AVTransportService(SonosServiceOptions options): base("AVTransport", "/MediaRenderer/AVTransport/Control", "/MediaRenderer/AVTransport/Event", options) {}


    /// <summary>
    /// AddMultipleURIsToQueue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>AddMultipleURIsToQueueResponse</returns>
    public Task<AddMultipleURIsToQueueResponse> AddMultipleURIsToQueue(AddMultipleURIsToQueueRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<AddMultipleURIsToQueueRequest, AddMultipleURIsToQueueResponse>("AddMultipleURIsToQueue", request, cancellationToken);

    /// <summary>
    /// Adds songs to the SONOS queue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>In NORMAL play mode the songs are added prior to the specified `DesiredFirstTrackNumberEnqueued`.</remarks>
    /// <returns>AddURIToQueueResponse</returns>
    public Task<AddURIToQueueResponse> AddURIToQueue(AddURIToQueueRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<AddURIToQueueRequest, AddURIToQueueResponse>("AddURIToQueue", request, cancellationToken);

    /// <summary>
    /// AddURIToSavedQueue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>AddURIToSavedQueueResponse</returns>
    public Task<AddURIToSavedQueueResponse> AddURIToSavedQueue(AddURIToSavedQueueRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<AddURIToSavedQueueRequest, AddURIToSavedQueueResponse>("AddURIToSavedQueue", request, cancellationToken);

    /// <summary>
    /// BackupQueue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> BackupQueue(CancellationToken cancellationToken = default) =>  ExecuteRequest<BackupQueueRequest>("BackupQueue", new BackupQueueRequest(), cancellationToken);

    /// <summary>
    /// Leave the current group and revert to a single player.
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>BecomeCoordinatorOfStandaloneGroupResponse</returns>
    public Task<BecomeCoordinatorOfStandaloneGroupResponse> BecomeCoordinatorOfStandaloneGroup(CancellationToken cancellationToken = default) =>  ExecuteRequest<BecomeCoordinatorOfStandaloneGroupRequest, BecomeCoordinatorOfStandaloneGroupResponse>("BecomeCoordinatorOfStandaloneGroup", new BecomeCoordinatorOfStandaloneGroupRequest(), cancellationToken);

    /// <summary>
    /// BecomeGroupCoordinator
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> BecomeGroupCoordinator(BecomeGroupCoordinatorRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<BecomeGroupCoordinatorRequest>("BecomeGroupCoordinator", request, cancellationToken);

    /// <summary>
    /// BecomeGroupCoordinatorAndSource
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> BecomeGroupCoordinatorAndSource(BecomeGroupCoordinatorAndSourceRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<BecomeGroupCoordinatorAndSourceRequest>("BecomeGroupCoordinatorAndSource", request, cancellationToken);

    /// <summary>
    /// ChangeCoordinator
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> ChangeCoordinator(ChangeCoordinatorRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<ChangeCoordinatorRequest>("ChangeCoordinator", request, cancellationToken);

    /// <summary>
    /// ChangeTransportSettings
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> ChangeTransportSettings(ChangeTransportSettingsRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<ChangeTransportSettingsRequest>("ChangeTransportSettings", request, cancellationToken);

    /// <summary>
    /// Stop playing after set sleep timer or cancel
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator returns error code 800</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> ConfigureSleepTimer(ConfigureSleepTimerRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<ConfigureSleepTimerRequest>("ConfigureSleepTimer", request, cancellationToken);

    /// <summary>
    /// CreateSavedQueue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>CreateSavedQueueResponse</returns>
    public Task<CreateSavedQueueResponse> CreateSavedQueue(CreateSavedQueueRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<CreateSavedQueueRequest, CreateSavedQueueResponse>("CreateSavedQueue", request, cancellationToken);

    /// <summary>
    /// Delegates the coordinator role to another player in the same group
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator has no results - should be avoided.</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> DelegateGroupCoordinationTo(DelegateGroupCoordinationToRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<DelegateGroupCoordinationToRequest>("DelegateGroupCoordinationTo", request, cancellationToken);

    /// <summary>
    /// EndDirectControlSession
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> EndDirectControlSession(CancellationToken cancellationToken = default) =>  ExecuteRequest<EndDirectControlSessionRequest>("EndDirectControlSession", new EndDirectControlSessionRequest(), cancellationToken);

    /// <summary>
    /// Get crossfade mode
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator may return wrong value as only the coordinator value in a group</remarks>
    /// <returns>GetCrossfadeModeResponse</returns>
    public Task<GetCrossfadeModeResponse> GetCrossfadeMode(CancellationToken cancellationToken = default) =>  ExecuteRequest<GetCrossfadeModeRequest, GetCrossfadeModeResponse>("GetCrossfadeMode", new GetCrossfadeModeRequest(), cancellationToken);

    /// <summary>
    /// Get current transport actions such as Set, Stop, Pause, Play, X_DLNA_SeekTime, Next, X_DLNA_SeekTrackNr
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator returns only `Start` and `Stop` since it cannot control the stream.</remarks>
    /// <returns>GetCurrentTransportActionsResponse</returns>
    public Task<GetCurrentTransportActionsResponse> GetCurrentTransportActions(CancellationToken cancellationToken = default) =>  ExecuteRequest<GetCurrentTransportActionsRequest, GetCurrentTransportActionsResponse>("GetCurrentTransportActions", new GetCurrentTransportActionsRequest(), cancellationToken);

    /// <summary>
    /// GetDeviceCapabilities
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetDeviceCapabilitiesResponse</returns>
    public Task<GetDeviceCapabilitiesResponse> GetDeviceCapabilities(CancellationToken cancellationToken = default) =>  ExecuteRequest<GetDeviceCapabilitiesRequest, GetDeviceCapabilitiesResponse>("GetDeviceCapabilities", new GetDeviceCapabilitiesRequest(), cancellationToken);

    /// <summary>
    /// Get information about the current playing media (queue)
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetMediaInfoResponse</returns>
    public Task<GetMediaInfoResponse> GetMediaInfo(CancellationToken cancellationToken = default) =>  ExecuteRequest<GetMediaInfoRequest, GetMediaInfoResponse>("GetMediaInfo", new GetMediaInfoRequest(), cancellationToken);

    /// <summary>
    /// Get information about current position (position in queue and time in current song)
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetPositionInfoResponse</returns>
    public Task<GetPositionInfoResponse> GetPositionInfo(CancellationToken cancellationToken = default) =>  ExecuteRequest<GetPositionInfoRequest, GetPositionInfoResponse>("GetPositionInfo", new GetPositionInfoRequest(), cancellationToken);

    /// <summary>
    /// Get time left on sleeptimer.
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator returns error code 800</remarks>
    /// <returns>GetRemainingSleepTimerDurationResponse</returns>
    public Task<GetRemainingSleepTimerDurationResponse> GetRemainingSleepTimerDuration(CancellationToken cancellationToken = default) =>  ExecuteRequest<GetRemainingSleepTimerDurationRequest, GetRemainingSleepTimerDurationResponse>("GetRemainingSleepTimerDuration", new GetRemainingSleepTimerDurationRequest(), cancellationToken);

    /// <summary>
    /// GetRunningAlarmProperties
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetRunningAlarmPropertiesResponse</returns>
    public Task<GetRunningAlarmPropertiesResponse> GetRunningAlarmProperties(CancellationToken cancellationToken = default) =>  ExecuteRequest<GetRunningAlarmPropertiesRequest, GetRunningAlarmPropertiesResponse>("GetRunningAlarmProperties", new GetRunningAlarmPropertiesRequest(), cancellationToken);

    /// <summary>
    /// Get current transport status, speed and state such as PLAYING, STOPPED, PLAYING, PAUSED_PLAYBACK, TRANSITIONING, NO_MEDIA_PRESENT
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator always returns PLAYING</remarks>
    /// <returns>GetTransportInfoResponse</returns>
    public Task<GetTransportInfoResponse> GetTransportInfo(CancellationToken cancellationToken = default) =>  ExecuteRequest<GetTransportInfoRequest, GetTransportInfoResponse>("GetTransportInfo", new GetTransportInfoRequest(), cancellationToken);

    /// <summary>
    /// Get transport settings
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator returns the settings of it's queue</remarks>
    /// <returns>GetTransportSettingsResponse</returns>
    public Task<GetTransportSettingsResponse> GetTransportSettings(CancellationToken cancellationToken = default) =>  ExecuteRequest<GetTransportSettingsRequest, GetTransportSettingsResponse>("GetTransportSettings", new GetTransportSettingsRequest(), cancellationToken);

    /// <summary>
    /// Go to next song
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Possibly not supported at the moment see GetCurrentTransportActions</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> Next(CancellationToken cancellationToken = default) =>  ExecuteRequest<NextRequest>("Next", new NextRequest(), cancellationToken);

    /// <summary>
    /// NotifyDeletedURI
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> NotifyDeletedURI(NotifyDeletedURIRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<NotifyDeletedURIRequest>("NotifyDeletedURI", request, cancellationToken);

    /// <summary>
    /// Pause playback
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> Pause(CancellationToken cancellationToken = default) =>  ExecuteRequest<PauseRequest>("Pause", new PauseRequest(), cancellationToken);

    /// <summary>
    /// Start playing the set TransportURI
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> Play(PlayRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<PlayRequest>("Play", request, cancellationToken);

    /// <summary>
    /// Go to previous song
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Possibly not supported at the moment see GetCurrentTransportActions</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> Previous(CancellationToken cancellationToken = default) =>  ExecuteRequest<PreviousRequest>("Previous", new PreviousRequest(), cancellationToken);

    /// <summary>
    /// Flushes the SONOS queue.
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>If queue is already empty it throw error 804. Send to non-coordinator returns error code 800.</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> RemoveAllTracksFromQueue(CancellationToken cancellationToken = default) =>  ExecuteRequest<RemoveAllTracksFromQueueRequest>("RemoveAllTracksFromQueue", new RemoveAllTracksFromQueueRequest(), cancellationToken);

    /// <summary>
    /// RemoveTrackFromQueue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RemoveTrackFromQueue(RemoveTrackFromQueueRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<RemoveTrackFromQueueRequest>("RemoveTrackFromQueue", request, cancellationToken);

    /// <summary>
    /// Removes the specified range of songs from the SONOS queue.
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>RemoveTrackRangeFromQueueResponse</returns>
    public Task<RemoveTrackRangeFromQueueResponse> RemoveTrackRangeFromQueue(RemoveTrackRangeFromQueueRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<RemoveTrackRangeFromQueueRequest, RemoveTrackRangeFromQueueResponse>("RemoveTrackRangeFromQueue", request, cancellationToken);

    /// <summary>
    /// ReorderTracksInQueue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> ReorderTracksInQueue(ReorderTracksInQueueRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<ReorderTracksInQueueRequest>("ReorderTracksInQueue", request, cancellationToken);

    /// <summary>
    /// ReorderTracksInSavedQueue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>ReorderTracksInSavedQueueResponse</returns>
    public Task<ReorderTracksInSavedQueueResponse> ReorderTracksInSavedQueue(ReorderTracksInSavedQueueRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<ReorderTracksInSavedQueueRequest, ReorderTracksInSavedQueueResponse>("ReorderTracksInSavedQueue", request, cancellationToken);

    /// <summary>
    /// RunAlarm
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RunAlarm(RunAlarmRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<RunAlarmRequest>("RunAlarm", request, cancellationToken);

    /// <summary>
    /// Saves the current SONOS queue as a SONOS playlist and outputs objectID
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator returns error code 800</remarks>
    /// <returns>SaveQueueResponse</returns>
    public Task<SaveQueueResponse> SaveQueue(SaveQueueRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<SaveQueueRequest, SaveQueueResponse>("SaveQueue", request, cancellationToken);

    /// <summary>
    /// Seek track in queue, time delta or absolute time in song
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Returns error code 701 in case that content does not support Seek or send to non-coordinator</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> Seek(SeekRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<SeekRequest>("Seek", request, cancellationToken);

    /// <summary>
    /// Set the transport URI to a song, a stream, the queue, another player-rincon and a lot more
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>If set to another player RINCON, the player is grouped with that one.</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> SetAVTransportURI(SetAVTransportURIRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<SetAVTransportURIRequest>("SetAVTransportURI", request, cancellationToken);

    /// <summary>
    /// Set crossfade mode
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator returns error code 800. Same for content, which does not support crossfade mode.</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> SetCrossfadeMode(SetCrossfadeModeRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<SetCrossfadeModeRequest>("SetCrossfadeMode", request, cancellationToken);

    /// <summary>
    /// SetNextAVTransportURI
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetNextAVTransportURI(SetNextAVTransportURIRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<SetNextAVTransportURIRequest>("SetNextAVTransportURI", request, cancellationToken);

    /// <summary>
    /// Set the PlayMode
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Send to non-coordinator returns error code 712. If SONOS queue is not activated returns error code 712.</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> SetPlayMode(SetPlayModeRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<SetPlayModeRequest>("SetPlayMode", request, cancellationToken);

    /// <summary>
    /// Snooze the current alarm for some time.
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SnoozeAlarm(SnoozeAlarmRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<SnoozeAlarmRequest>("SnoozeAlarm", request, cancellationToken);

    /// <summary>
    /// StartAutoplay
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> StartAutoplay(StartAutoplayRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<StartAutoplayRequest>("StartAutoplay", request, cancellationToken);

    /// <summary>
    /// Stop playback
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> Stop(CancellationToken cancellationToken = default) =>  ExecuteRequest<StopRequest>("Stop", new StopRequest(), cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BaseRequest
    {
      [System.Xml.Serialization.XmlNamespaceDeclarations]
      public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
        new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:AVTransport:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class AddMultipleURIsToQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public int UpdateID { get; set; }

        public int NumberOfURIs { get; set; }

        public string EnqueuedURIs { get; set; }

        public string EnqueuedURIsMetaData { get; set; }

        public string ContainerURI { get; set; }

        public string ContainerMetaData { get; set; }

        public int DesiredFirstTrackNumberEnqueued { get; set; }

        public bool EnqueueAsNext { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("AddMultipleURIsToQueueResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class AddMultipleURIsToQueueResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int FirstTrackNumberEnqueued { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NumTracksAdded { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewQueueLength { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class AddURIToQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string EnqueuedURI { get; set; }

        public string EnqueuedURIMetaData { get; set; }

        /// <summary>
        /// use `0` to add at the end or `1` to insert at the beginning
        /// </summary>
        public int DesiredFirstTrackNumberEnqueued { get; set; }

        public bool EnqueueAsNext { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("AddURIToQueueResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class AddURIToQueueResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int FirstTrackNumberEnqueued { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NumTracksAdded { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewQueueLength { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class AddURIToSavedQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string ObjectID { get; set; }

        public int UpdateID { get; set; }

        public string EnqueuedURI { get; set; }

        public string EnqueuedURIMetaData { get; set; }

        public int AddAtIndex { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("AddURIToSavedQueueResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class AddURIToSavedQueueResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NumTracksAdded { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewQueueLength { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BackupQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BecomeCoordinatorOfStandaloneGroupRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("BecomeCoordinatorOfStandaloneGroupResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class BecomeCoordinatorOfStandaloneGroupResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string DelegatedGroupCoordinatorID { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string NewGroupID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BecomeGroupCoordinatorRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string CurrentCoordinator { get; set; }

        public string CurrentGroupID { get; set; }

        public string OtherMembers { get; set; }

        public string TransportSettings { get; set; }

        public string CurrentURI { get; set; }

        public string CurrentURIMetaData { get; set; }

        public string SleepTimerState { get; set; }

        public string AlarmState { get; set; }

        public string StreamRestartState { get; set; }

        public string CurrentQueueTrackList { get; set; }

        public string CurrentVLIState { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BecomeGroupCoordinatorAndSourceRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string CurrentCoordinator { get; set; }

        public string CurrentGroupID { get; set; }

        public string OtherMembers { get; set; }

        public string CurrentURI { get; set; }

        public string CurrentURIMetaData { get; set; }

        public string SleepTimerState { get; set; }

        public string AlarmState { get; set; }

        public string StreamRestartState { get; set; }

        public string CurrentAVTTrackList { get; set; }

        public string CurrentQueueTrackList { get; set; }

        public string CurrentSourceState { get; set; }

        public bool ResumePlayback { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class ChangeCoordinatorRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string CurrentCoordinator { get; set; }

        public string NewCoordinator { get; set; }

        public string NewTransportSettings { get; set; }

        public string CurrentAVTransportURI { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class ChangeTransportSettingsRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string NewTransportSettings { get; set; }

        public string CurrentAVTransportURI { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class ConfigureSleepTimerRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// Time to stop after, as `hh:mm:ss` or empty string to cancel
        /// </summary>
        public string NewSleepTimerDuration { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class CreateSavedQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string Title { get; set; }

        public string EnqueuedURI { get; set; }

        public string EnqueuedURIMetaData { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("CreateSavedQueueResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class CreateSavedQueueResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NumTracksAdded { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewQueueLength { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AssignedObjectID { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class DelegateGroupCoordinationToRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// uuid of the new coordinator - must be in same group
        /// </summary>
        public string NewCoordinator { get; set; }

        /// <summary>
        /// Should former coordinator rejoin the group?
        /// </summary>
        public bool RejoinGroup { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class EndDirectControlSessionRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetCrossfadeModeRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetCrossfadeModeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class GetCrossfadeModeResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool CrossfadeMode { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetCurrentTransportActionsRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetCurrentTransportActionsResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class GetCurrentTransportActionsResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string Actions { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetDeviceCapabilitiesRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetDeviceCapabilitiesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class GetDeviceCapabilitiesResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string PlayMedia { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string RecMedia { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string RecQualityModes { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetMediaInfoRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetMediaInfoResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class GetMediaInfoResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NrTracks { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string MediaDuration { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentURI { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentURIMetaData { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string NextURI { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string NextURIMetaData { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string PlayMedium { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string RecordMedium { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string WriteStatus { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetPositionInfoRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetPositionInfoResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class GetPositionInfoResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int Track { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string TrackDuration { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string TrackMetaData { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string TrackURI { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string RelTime { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AbsTime { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int RelCount { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int AbsCount { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetRemainingSleepTimerDurationRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetRemainingSleepTimerDurationResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class GetRemainingSleepTimerDurationResponse
    {

        /// <summary>
        /// Format hh:mm:ss or empty string if not set
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string RemainingSleepTimerDuration { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentSleepTimerGeneration { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetRunningAlarmPropertiesRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetRunningAlarmPropertiesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class GetRunningAlarmPropertiesResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int AlarmID { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string GroupID { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string LoggedStartTime { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetTransportInfoRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetTransportInfoResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class GetTransportInfoResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentTransportState { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentTransportStatus { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentSpeed { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetTransportSettingsRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetTransportSettingsResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class GetTransportSettingsResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string PlayMode { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string RecQualityMode { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class NextRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class NotifyDeletedURIRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string DeletedURI { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class PauseRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class PlayRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// Play speed usually 1, can be a fraction of 1
        /// </summary>
        public string Speed { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class PreviousRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RemoveAllTracksFromQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RemoveTrackFromQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string ObjectID { get; set; }

        public int UpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RemoveTrackRangeFromQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// Leave blank
        /// </summary>
        public int UpdateID { get; set; }

        /// <summary>
        /// between 1 and queue-length
        /// </summary>
        public int StartingIndex { get; set; }

        public int NumberOfTracks { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("RemoveTrackRangeFromQueueResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class RemoveTrackRangeFromQueueResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class ReorderTracksInQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public int StartingIndex { get; set; }

        public int NumberOfTracks { get; set; }

        public int InsertBefore { get; set; }

        public int UpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class ReorderTracksInSavedQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string ObjectID { get; set; }

        public int UpdateID { get; set; }

        public string TrackList { get; set; }

        public string NewPositionList { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("ReorderTracksInSavedQueueResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class ReorderTracksInSavedQueueResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int QueueLengthChange { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewQueueLength { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RunAlarmRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public int AlarmID { get; set; }

        public string LoggedStartTime { get; set; }

        public string Duration { get; set; }

        public string ProgramURI { get; set; }

        public string ProgramMetaData { get; set; }

        public string PlayMode { get; set; }

        public int Volume { get; set; }

        public bool IncludeLinkedZones { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SaveQueueRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// SONOS playlist title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Leave blank
        /// </summary>
        public string ObjectID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("SaveQueueResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AVTransport:1")]
    public partial class SaveQueueResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AssignedObjectID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SeekRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// What to seek
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Position of track in queue (start at 1) or `hh:mm:ss` for `REL_TIME` or `+/-hh:mm:ss` for `TIME_DELTA`
        /// </summary>
        public string Target { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetAVTransportURIRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// The new TransportURI - its a special SONOS format
        /// </summary>
        public string CurrentURI { get; set; }

        /// <summary>
        /// Track Metadata, see MetadataHelper.GuessTrack to guess based on track uri
        /// </summary>
        public string CurrentURIMetaData { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetCrossfadeModeRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public bool CrossfadeMode { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetNextAVTransportURIRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string NextURI { get; set; }

        public string NextURIMetaData { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetPlayModeRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// New playmode
        /// </summary>
        public string NewPlayMode { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SnoozeAlarmRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        /// <summary>
        /// Snooze time as `hh:mm:ss`, 10 minutes = 00:10:00
        /// </summary>
        public string Duration { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class StartAutoplayRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;

        public string ProgramURI { get; set; }

        public string ProgramMetaData { get; set; }

        public int Volume { get; set; }

        public bool IncludeLinkedZones { get; set; }

        public bool ResetVolumeAfter { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class StopRequest : BaseRequest
    {

        /// <summary>
        /// InstanceID should always be `0`
        /// </summary>
        public int InstanceID { get; set; } = 0;
    }
}
