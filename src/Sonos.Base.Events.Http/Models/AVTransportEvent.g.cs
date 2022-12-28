/*
 * Sonos-net AVTransportService event parsing
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

namespace Sonos.Base.Events.Http.Models;

using Sonos.Base.Services;


#nullable enable
/// <summary>
/// AVTransport is set to might emit these properties in events
/// </summary>
public partial class AVTransportEvent: AVTransportService.IAVTransportEvent {
    public int? AbsoluteCounterPosition { get; init; }

    public string? AbsoluteTimePosition { get; init; }

    public int? AlarmIDRunning { get; init; }

    public string? AlarmLoggedStartTime { get; init; }

    public bool? AlarmRunning { get; init; }

    public string? AVTransportURI { get; init; }

    public string? AVTransportURIMetaData { get; init; }

    /// <summary>
    /// Parsed version of AVTransportURIMetaData
    /// </summary>
    public Metadata.Didl? AVTransportURIMetaDataObject => Metadata.DidlSerializer.DeserializeMetadata(AVTransportURIMetaData);

    public bool? CurrentCrossfadeMode { get; init; }

    public string? CurrentMediaDuration { get; init; }

    public string? CurrentPlayMode { get; init; }

    public string? CurrentRecordQualityMode { get; init; }

    public int? CurrentSection { get; init; }

    public int? CurrentTrack { get; init; }

    public string? CurrentTrackDuration { get; init; }

    public string? CurrentTrackMetaData { get; init; }

    /// <summary>
    /// Parsed version of CurrentTrackMetaData
    /// </summary>
    public Metadata.Didl? CurrentTrackMetaDataObject => Metadata.DidlSerializer.DeserializeMetadata(CurrentTrackMetaData);

    public string? CurrentTrackURI { get; init; }

    public string? CurrentTransportActions { get; init; }

    public string? CurrentValidPlayModes { get; init; }

    public string? DirectControlAccountID { get; init; }

    public string? DirectControlClientID { get; init; }

    public bool? DirectControlIsSuspended { get; init; }

    public string? EnqueuedTransportURI { get; init; }

    public string? EnqueuedTransportURIMetaData { get; init; }

    /// <summary>
    /// Parsed version of EnqueuedTransportURIMetaData
    /// </summary>
    public Metadata.Didl? EnqueuedTransportURIMetaDataObject => Metadata.DidlSerializer.DeserializeMetadata(EnqueuedTransportURIMetaData);

    public string? LastChange { get; init; }

    public string? MuseSessions { get; init; }

    public string? NextAVTransportURI { get; init; }

    public string? NextAVTransportURIMetaData { get; init; }

    /// <summary>
    /// Parsed version of NextAVTransportURIMetaData
    /// </summary>
    public Metadata.Didl? NextAVTransportURIMetaDataObject => Metadata.DidlSerializer.DeserializeMetadata(NextAVTransportURIMetaData);

    public string? NextTrackMetaData { get; init; }

    /// <summary>
    /// Parsed version of NextTrackMetaData
    /// </summary>
    public Metadata.Didl? NextTrackMetaDataObject => Metadata.DidlSerializer.DeserializeMetadata(NextTrackMetaData);

    public string? NextTrackURI { get; init; }

    public int? NumberOfTracks { get; init; }

    public string? PlaybackStorageMedium { get; init; }

    public string? PossiblePlaybackStorageMedia { get; init; }

    public string? PossibleRecordQualityModes { get; init; }

    public string? PossibleRecordStorageMedia { get; init; }

    public int? QueueUpdateID { get; init; }

    public string? RecordMediumWriteStatus { get; init; }

    public string? RecordStorageMedium { get; init; }

    public int? RelativeCounterPosition { get; init; }

    public string? RelativeTimePosition { get; init; }

    public bool? RestartPending { get; init; }

    public int? SleepTimerGeneration { get; init; }

    public bool? SnoozeRunning { get; init; }

    public string? TransportErrorDescription { get; init; }

    public string? TransportErrorHttpCode { get; init; }

    public string? TransportErrorHttpHeaders { get; init; }

    public string? TransportErrorURI { get; init; }

    public string? TransportPlaySpeed { get; init; }

    public string? TransportState { get; init; }

    public string? TransportStatus { get; init; }

    internal static AVTransportEvent? FromDictionary(Dictionary<string, string>? dic)
    {
        if (dic is null) {
            return null;
        }
        return new AVTransportEvent {
            AbsoluteCounterPosition = dic.TryGetInt(nameof(AbsoluteCounterPosition)),
            AbsoluteTimePosition = dic.TryGetString(nameof(AbsoluteTimePosition)),
            AlarmIDRunning = dic.TryGetInt(nameof(AlarmIDRunning)),
            AlarmLoggedStartTime = dic.TryGetString(nameof(AlarmLoggedStartTime)),
            AlarmRunning = dic.TryGetBool(nameof(AlarmRunning)),
            AVTransportURI = dic.TryGetString(nameof(AVTransportURI)),
            AVTransportURIMetaData = dic.TryGetString(nameof(AVTransportURIMetaData)),
            CurrentCrossfadeMode = dic.TryGetBool(nameof(CurrentCrossfadeMode)),
            CurrentMediaDuration = dic.TryGetString(nameof(CurrentMediaDuration)),
            CurrentPlayMode = dic.TryGetString(nameof(CurrentPlayMode)),
            CurrentRecordQualityMode = dic.TryGetString(nameof(CurrentRecordQualityMode)),
            CurrentSection = dic.TryGetInt(nameof(CurrentSection)),
            CurrentTrack = dic.TryGetInt(nameof(CurrentTrack)),
            CurrentTrackDuration = dic.TryGetString(nameof(CurrentTrackDuration)),
            CurrentTrackMetaData = dic.TryGetString(nameof(CurrentTrackMetaData)),
            CurrentTrackURI = dic.TryGetString(nameof(CurrentTrackURI)),
            CurrentTransportActions = dic.TryGetString(nameof(CurrentTransportActions)),
            CurrentValidPlayModes = dic.TryGetString(nameof(CurrentValidPlayModes)),
            DirectControlAccountID = dic.TryGetString(nameof(DirectControlAccountID)),
            DirectControlClientID = dic.TryGetString(nameof(DirectControlClientID)),
            DirectControlIsSuspended = dic.TryGetBool(nameof(DirectControlIsSuspended)),
            EnqueuedTransportURI = dic.TryGetString(nameof(EnqueuedTransportURI)),
            EnqueuedTransportURIMetaData = dic.TryGetString(nameof(EnqueuedTransportURIMetaData)),
            LastChange = dic.TryGetString(nameof(LastChange)),
            MuseSessions = dic.TryGetString(nameof(MuseSessions)),
            NextAVTransportURI = dic.TryGetString(nameof(NextAVTransportURI)),
            NextAVTransportURIMetaData = dic.TryGetString(nameof(NextAVTransportURIMetaData)),
            NextTrackMetaData = dic.TryGetString(nameof(NextTrackMetaData)),
            NextTrackURI = dic.TryGetString(nameof(NextTrackURI)),
            NumberOfTracks = dic.TryGetInt(nameof(NumberOfTracks)),
            PlaybackStorageMedium = dic.TryGetString(nameof(PlaybackStorageMedium)),
            PossiblePlaybackStorageMedia = dic.TryGetString(nameof(PossiblePlaybackStorageMedia)),
            PossibleRecordQualityModes = dic.TryGetString(nameof(PossibleRecordQualityModes)),
            PossibleRecordStorageMedia = dic.TryGetString(nameof(PossibleRecordStorageMedia)),
            QueueUpdateID = dic.TryGetInt(nameof(QueueUpdateID)),
            RecordMediumWriteStatus = dic.TryGetString(nameof(RecordMediumWriteStatus)),
            RecordStorageMedium = dic.TryGetString(nameof(RecordStorageMedium)),
            RelativeCounterPosition = dic.TryGetInt(nameof(RelativeCounterPosition)),
            RelativeTimePosition = dic.TryGetString(nameof(RelativeTimePosition)),
            RestartPending = dic.TryGetBool(nameof(RestartPending)),
            SleepTimerGeneration = dic.TryGetInt(nameof(SleepTimerGeneration)),
            SnoozeRunning = dic.TryGetBool(nameof(SnoozeRunning)),
            TransportErrorDescription = dic.TryGetString(nameof(TransportErrorDescription)),
            TransportErrorHttpCode = dic.TryGetString(nameof(TransportErrorHttpCode)),
            TransportErrorHttpHeaders = dic.TryGetString(nameof(TransportErrorHttpHeaders)),
            TransportErrorURI = dic.TryGetString(nameof(TransportErrorURI)),
            TransportPlaySpeed = dic.TryGetString(nameof(TransportPlaySpeed)),
            TransportState = dic.TryGetString(nameof(TransportState)),
            TransportStatus = dic.TryGetString(nameof(TransportStatus)),
        };
    }
}
