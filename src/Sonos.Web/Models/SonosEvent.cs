namespace Sonos.Web.Models;

/// <summary>
/// Object describing the current state of a Sonos device.
/// </summary>
public class SonosEvent
{
    /// <summary>
    /// Gets or sets the unique identifier for the group associated with this entity.
    /// </summary>
    public string? GroupId { get; set; }

    /// <summary>
    /// Name of the group
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the media is currently playing.
    /// </summary>
    public bool IsPlaying { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the audio output is muted.
    /// </summary>
    public bool Muted { get; set; }

    /// <summary>
    /// Current Transport State of the device, e.g. "PLAYING", "PAUSED_PLAYBACK", "STOPPED", etc.
    /// </summary>
    public string? TransportState { get; set; }

    /// <summary>
    /// Current Volume, value between 0 and 100.
    /// </summary>
    public int? Volume { get; set; }

    /// <summary>
    /// Internal URI of the current track.
    /// </summary>
    public string? CurrentTrackUri { get; set; }

    /// <summary>
    /// Track information of the current track
    /// </summary>
    public SonosTrack? CurrentTrack { get; set; }

    /// <summary>
    /// Current transport URI, queue, input, etc.
    /// </summary>
    public string? AVTransportUri { get; set; }

    /// <summary>
    /// Track information of the current track
    /// </summary>
    public SonosTrack? AVTransportMetadata { get; set; }

    /// <summary>
    /// Internal URI of the next track, if available.
    /// </summary>
    public string? NextTrackUri { get; set; }

    /// <summary>
    /// Track information of the next track, if available.
    /// </summary>
    public SonosTrack? NextTrack { get; set; }

    internal static SonosEvent? FromBaseEvent(Sonos.Base.Models.SonosEvent? sonosEvent)
    {
        if (sonosEvent == null)
        {
            return null;
        }
        return new SonosEvent
        {
            GroupId = sonosEvent.GroupId,
            GroupName = sonosEvent.GroupName,
            IsPlaying = sonosEvent.IsPlaying == true,
            Muted = sonosEvent.Muted == true,
            TransportState = sonosEvent.TransportState,
            Volume = sonosEvent.Volume,
            CurrentTrackUri = sonosEvent.CurrentTrackUri,
            CurrentTrack = sonosEvent.CurrentTrack != null ? SonosTrack.FromDidlTrack(sonosEvent.CurrentTrack) : null,
            NextTrackUri = sonosEvent.NextTrackUri,
            NextTrack = sonosEvent.NextTrack != null ? SonosTrack.FromDidlTrack(sonosEvent.NextTrack) : null,
            AVTransportUri = sonosEvent.AVTransportUri,
            AVTransportMetadata = sonosEvent.AVTransportMetadata != null ? SonosTrack.FromDidlTrack(sonosEvent.AVTransportMetadata) : null
        };
    }
}
