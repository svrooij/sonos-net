using System;
using System.Collections.Generic;
using System.Text;

using Sonos.Base.Metadata;

namespace Sonos.Base.Music.Models;

/// <summary>
/// Represents a request for playback information for a specific media item or track.
/// </summary>
/// <remarks>Either the <paramref name="TrackId"/> or both the <paramref name="ServiceId"/> or the <paramref name="MediaId"/> are required</remarks>
/// <param name="TrackId">The unique identifier of the track for which playback information is requested. Can be null if not applicable.</param>
/// <param name="ServiceId">The identifier of the media service associated with the request. Can be null if the service is not specified.</param>
/// <param name="MediaId">The unique identifier of the media item for which playback information is requested. Can be null if not applicable.</param>
public record MediaPlaybackInformationRequest(string? TrackId, ushort? ServiceId, string? MediaId);
