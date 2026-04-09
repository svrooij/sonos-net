using System.ComponentModel;

namespace Sonos.Base
{
    /// <summary>
    /// Represents a request to set the transport URI and associated metadata for a media playback operation.
    /// </summary>
    /// <param name="TransportUri">The transport URI in the Sonos-specific format that identifies the media source to be played. This value cannot
    /// be null or empty.</param>
    /// <param name="Metadata">Optional metadata describing the media. If not specified, the system will attempt to infer the metadata
    /// automatically.</param>
    public record SetTransportUriRequest ([Description("Transport URI, special sonos format")] string TransportUri, [Description("optional metadata, will be guessed otherwise")]string Metadata = "");

}
