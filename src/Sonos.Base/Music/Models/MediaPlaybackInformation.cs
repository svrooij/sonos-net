using System;
using System.Collections.Generic;
using System.Text;

using Sonos.Base.Metadata;

namespace Sonos.Base.Music.Models;

public record MediaPlaybackInformation(PlaybackType PlaybackType, string TrackUri, DidlTrack Metadata);
public enum PlaybackType
{
    Unknown,
    Stream,
    Track,
    Container,
}