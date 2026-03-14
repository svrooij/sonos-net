namespace Sonos.Web.Models;

/// <summary>
/// Represents a request to switch to a specific media stream within a service.
/// </summary>
public class SwitchToStreamRequest
{
    /// <summary>
    /// Id of the music service to switch to
    /// </summary>
    public int ServiceId { get; set; } = default!;

    /// <summary>
    /// Id of the media item to switch to.
    /// </summary>
    public string MediaId { get; set; } = default!;

    /// <summary>
    /// Start playback immediately after switching to the stream. If false, the stream will be loaded but not played until the user initiates playback.
    /// </summary>
    public bool Play { get; set; }
}
