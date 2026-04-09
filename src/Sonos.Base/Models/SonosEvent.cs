using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sonos.Base.Services;

namespace Sonos.Base.Models;
public class SonosEvent
{
    public string? GroupId { get; internal set; }
    public string? GroupName { get; internal set; }

    public Metadata.DidlTrack? CurrentTrack { get; internal set; }
    public string? CurrentTrackUri { get; internal set; }

    public string? AVTransportUri{ get; internal set; }
    public Metadata.DidlTrack? AVTransportMetadata { get; internal set; }

    public Metadata.DidlTrack? NextTrack { get; internal set; }
    public string? NextTrackUri { get; internal set; }
    public bool? Muted { get; internal set; }
    public int? Volume { get; internal set; }
    public string? TransportState { get; internal set; }
    public bool? IsPlaying => AVTransportService.TransportState.IsPlaying(TransportState);


}
