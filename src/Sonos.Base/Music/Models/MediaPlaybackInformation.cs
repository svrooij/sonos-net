using System;
using System.Collections.Generic;
using System.Text;

using Sonos.Base.Metadata;

namespace Sonos.Base.Music.Models;

public record MediaPlaybackInformation(string TrackUri, DidlTrack Metadata);
