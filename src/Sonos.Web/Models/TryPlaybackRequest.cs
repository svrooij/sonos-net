namespace Sonos.Web.Models;

/// <summary>
/// Represents a request try to start playback of a media item.
/// </summary>
/// <remarks>Submit either the <see cref="TrackUri"/> or both the <see cref="ServiceId"/> and the <see cref="MediaId"/>.</remarks>
public class TryPlaybackRequest
{
    /// <summary>
    /// Track uri of the item to switch to.
    /// </summary>
    public string? TrackUri { get; set; }
    /// <summary>
    /// Id of the music service to switch to
    /// </summary>
    public int? ServiceId { get; set; }

    /// <summary>
    /// Id of the media item to switch to.
    /// </summary>
    public string? MediaId { get; set; }

    public bool AddToStart { get; set; }
    public bool PlayAsNextSong { get; set; }

    /// <summary>
    /// Start playback immediately after the try play requests succeeds. If false, the media will be loaded but not played.
    /// </summary>
    public bool Play { get; set; }
}
