namespace Sonos.Web.Blazor.Models;

public class SonosEvent
{
    public string? GroupId { get; set; }
    public string? GroupName { get; set; }
    public bool IsPlaying { get; set; }
    public bool Muted { get; set; }
    public string? TransportState { get; set; }
    public int? Volume { get; set; }

    public string? CurrentTrackUri { get; set; }
    public SonosTrack? CurrentTrack { get; set; }

    public string? NextTrackUri { get; set; }
    public SonosTrack? NextTrack { get; set; }
    public string? AVTransportUri { get; set; }
    public SonosTrack? AVTransportMetadata { get; set; }
}

public static class GeneratedClassExtensions
{
    public static SonosEvent ToSonosEvent(this Sonos.Web.Blazor.Client.Models.SonosEvent sonosEvent)
    {
        return new SonosEvent
        {
            GroupId = sonosEvent.GroupId,
            GroupName = sonosEvent.GroupName,
            IsPlaying = sonosEvent.IsPlaying == true,
            Muted = sonosEvent.Muted == true,
            TransportState = sonosEvent.TransportState,
            Volume = sonosEvent.Volume,
            CurrentTrackUri = sonosEvent.CurrentTrackUri,
            CurrentTrack = sonosEvent.CurrentTrack?.SonosTrack != null ? sonosEvent.CurrentTrack.SonosTrack.ToSonosTrack() : null,
            NextTrackUri = sonosEvent.NextTrackUri,
            NextTrack = sonosEvent.NextTrack?.SonosTrack != null ? sonosEvent.NextTrack.SonosTrack.ToSonosTrack() : null,
            AVTransportUri = sonosEvent.AvTransportUri,
            AVTransportMetadata = sonosEvent.AvTransportMetadata?.SonosTrack != null ? sonosEvent.AvTransportMetadata.SonosTrack.ToSonosTrack() : null
        };
    }

    public static SonosTrack ToSonosTrack(this Sonos.Web.Blazor.Client.Models.SonosTrack track)
    {
        return new SonosTrack
        {
            Album = track.Album,
            AlbumArtUri = track.AlbumArtUri,
            Creator = track.Creator,
            Id = track.Id,
            ParentId = track.ParentId,
            Podcast = track.Podcast,
            Title = track.Title,
            StreamContent = track.StreamContent
        };
    }
}
