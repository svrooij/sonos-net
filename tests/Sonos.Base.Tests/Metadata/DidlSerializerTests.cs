using Xunit;

namespace Sonos.Base.Metadata.Tests;

public class DidlSerializerTests
{
    const string xmlSonosPlaylist = @"<DIDL-Lite xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:upnp=""urn:schemas-upnp-org:metadata-1-0/upnp/""
    xmlns:r=""urn:schemas-rinconnetworks-com:metadata-1-0/"" xmlns=""urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/"">
    <item id=""1006206cspotify%3aplaylist%3a37i9dQZF1DX4WYpdgoIcn6"" parentID=""10fe2664playlists"" restricted=""true"">
        <dc:title>Chill Hits</dc:title>
        <upnp:class>object.container.playlistContainer</upnp:class>
        <desc id=""cdudn"" nameSpace=""urn:schemas-rinconnetworks-com:metadata-1-0/"">SA_RINCON2311_X_#Svc2311-0-Token</desc>
    </item>
</DIDL-Lite>";
    [Fact]
    public void DeserializeMetadata_SonosPlaylist_ParsesExpectedValues()
    {
        var didl = DidlSerializer.DeserializeMetadata(xmlSonosPlaylist);
        Assert.NotNull(didl);
        Assert.Single(didl.Items);
        var item = didl.Items[0];
        Assert.Equal("1006206cspotify%3aplaylist%3a37i9dQZF1DX4WYpdgoIcn6", item.Id);
        Assert.Equal("10fe2664playlists", item.ParentId);
        Assert.True(item.Restricted);
        Assert.Equal("Chill Hits", item.Title);
    }

    //[Fact(Skip = "Desc is not parsed correctly")]
    [Fact]
    public void DeserializeMetadata_SonosPlaylist_ParsesDescElement()
    {
        var didl = DidlSerializer.DeserializeMetadata(xmlSonosPlaylist);
        Assert.NotNull(didl);
        Assert.Single(didl.Items);
        var item = didl.Items[0];

        Assert.NotNull(item.Desc);
        Assert.Equal("cdudn", item?.Desc?.Id);
        Assert.Equal("SA_RINCON2311_X_#Svc2311-0-Token", item?.Desc?.Value);
    }
}
