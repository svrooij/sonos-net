/*
 * Sonos-net
 *
 * Repository https://github.com/svrooij/sonos-net
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xunit;

namespace Sonos.Base.Metadata.Tests;

public class DidlSerializerTests
{
    private const string xmlSonosPlaylist = @"<DIDL-Lite xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:upnp=""urn:schemas-upnp-org:metadata-1-0/upnp/""
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

    private const string xmlRadioStation = @"<DIDL-Lite xmlns:dc=""http://purl.org/dc/elements/1.1/""
    xmlns:upnp=""urn:schemas-upnp-org:metadata-1-0/upnp/""
    xmlns:r=""urn:schemas-rinconnetworks-com:metadata-1-0/""
    xmlns=""urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/"">
    <item id=""-1"" parentID=""-1"" restricted=""true"">
        <res protocolInfo=""aac:*:application/octet-stream:*"">
            aac://http://stream.srg-ssr.ch/m/drs3/aacp_96</res>
        <r:streamContent>ANDRYY - BRUCHPILOT</r:streamContent>
        <dc:title>aacp_96</dc:title>
        <upnp:class>object.item</upnp:class>
    </item>
</DIDL-Lite>";

    [Fact]
    public void DeserializeMetadata_SonosRadioStream()
    {
        var didl = DidlSerializer.DeserializeMetadata(xmlRadioStation);
        Assert.NotNull(didl);
        Assert.Single(didl.Items);

        var item = didl.Items[0];

        Assert.Equal("aacp_96", item.Title);
        Assert.Equal("ANDRYY - BRUCHPILOT", item.StreamContent);
    }
}