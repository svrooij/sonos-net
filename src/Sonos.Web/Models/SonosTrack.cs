namespace Sonos.Web.Models;
/// <summary>
/// Track description
/// </summary>
public class SonosTrack
{
    /// <summary>
    /// Track ID, meaning unknown
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Parent ID, sometimes the playlist ID, but not always
    /// </summary>
    public int? ParentId { get; set; }
    /// <summary>
    /// Title of the song
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Artist of the song
    /// </summary>
    public string? Creator { get; set; }
    /// <summary>
    /// Name of the album
    /// </summary>
    public string? Album { get; set; }
    /// <summary>
    /// Kind of music streamming, e.g. "object.item.audioItem.musicTrack" or "object.item.audioItem.podcastEpisode"
    /// </summary>
    public string? Class { get; set; }
    /// <summary>
    /// Absolute or relative URI to the album art
    /// </summary>
    public string? AlbumArtUri { get; set; }
    /// <summary>
    /// Name of the podcast
    /// </summary>
    public string? Podcast { get; set; }
    /// <summary>
    /// Tags of the song, not sure if ever used
    /// </summary>
    public string? Tags { get; set; }
    //public SonosTrackResource? Resource { get; set; }
    //public SonosTrackVirtualLineIn? VitualLineIn { get; set; }
    /// <summary>
    /// Stream content value, used for streaming tracks. Parsed to title and creator if those are empty.
    /// </summary>
    public string? StreamContent { get; set; }

    internal static SonosTrack FromDidlTrack(Sonos.Base.Metadata.DidlTrack track)
    {
        var title = track.Title;
        var creator = track.Creator;
        if (!string.IsNullOrEmpty(track.StreamContent?.Value) && track.StreamContent.Value.IndexOf('-') > 0)
        {
            var splitted = track.StreamContent.Value.Split('-', 2, StringSplitOptions.RemoveEmptyEntries);

            title ??= splitted[1].Trim();
            creator ??= splitted[0].Trim();
        }
        return new SonosTrack
        {
            Id = int.TryParse(track.Id, out var id) ? id : (int?)null,
            ParentId = int.TryParse(track.ParentId, out var parentId) ? parentId : (int?)null,
            Title = title,
            Creator = creator,
            Album = track.Album,
            Class = track.Class,
            AlbumArtUri = track.AlbumArtUri,
            Podcast = track.Podcast,
            Tags = track.Tags,
            StreamContent = track.StreamContent?.Value
        };
    }
}
