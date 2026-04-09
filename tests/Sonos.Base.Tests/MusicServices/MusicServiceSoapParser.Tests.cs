using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Sonos.Base.Tests.MusicServices;
public class MusicServiceSoapParser
{
    private const string getAppLinkResponseXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?><SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""><SOAP-ENV:Header/><SOAP-ENV:Body><ns2:getAppLinkResponse xmlns:ns2=""http://www.sonos.com/Services/1.1""><ns2:getAppLinkResult><ns2:authorizeAccount><ns2:appUrlStringId>SIGN_IN</ns2:appUrlStringId><ns2:deviceLink><ns2:regUrl>https://spotify-v5.ws.sonos.com/deviceLink/home?linkCode=SC2X69T</ns2:regUrl><ns2:linkCode>SC2X69T</ns2:linkCode><ns2:showLinkCode>false</ns2:showLinkCode></ns2:deviceLink></ns2:authorizeAccount><ns2:createAccount><ns2:appUrlStringId>CREATE_NEW</ns2:appUrlStringId></ns2:createAccount></ns2:getAppLinkResult></ns2:getAppLinkResponse></SOAP-ENV:Body></SOAP-ENV:Envelope>";

    [Fact]
    public void Parse_GetAppLinkResponseXml_CreatesCorrectObject()
    {
        var parsedObject = Sonos.Base.Music.Models.SoapParser.ParseXml<Sonos.Base.Music.GetAppLinkResponse>(getAppLinkResponseXml);
        Assert.NotNull(parsedObject);
        Assert.NotNull(parsedObject.GetAppLinkResult?.AuthorizeAccount);
        Assert.Equal("SIGN_IN", parsedObject.GetAppLinkResult?.AuthorizeAccount.AppUrlStringId);
        Assert.NotNull(parsedObject.GetAppLinkResult?.AuthorizeAccount.DeviceLink);
        Assert.Equal("SC2X69T", parsedObject.GetAppLinkResult?.AuthorizeAccount.DeviceLink.LinkCode);
        Assert.Equal("https://spotify-v5.ws.sonos.com/deviceLink/home?linkCode=SC2X69T", parsedObject.GetAppLinkResult?.AuthorizeAccount.DeviceLink.RegUrl);
        Assert.False(parsedObject.GetAppLinkResult?.AuthorizeAccount.DeviceLink.ShowLinkCode);
        Assert.NotNull(parsedObject.GetAppLinkResult?.CreateAccount);
        Assert.Equal("CREATE_NEW", parsedObject.GetAppLinkResult?.CreateAccount.AppUrlStringId);
    }

    private const string spotifyFault = @"<?xml version=""1.0"" encoding=""utf-8"" ?><SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""><SOAP-ENV:Header/><SOAP-ENV:Body><SOAP-ENV:Fault><faultcode xmlns:ns0=""SOAP-ENV"">ns0:Client.TokenRefreshRequired</faultcode><faultstring xml:lang=""en"">tokenRefreshRequired</faultstring><detail><refreshAuthTokenResult xmlns:ns2=""http://www.sonos.com/Services/1.1""><ns2:authToken>xxx</ns2:authToken><ns2:privateKey>aaa/NL/yyy</ns2:privateKey><ns2:userInfo><ns2:userIdHashCode>...</ns2:userIdHashCode><ns2:accountTier>paidPremium</ns2:accountTier><ns2:nickname>...</ns2:nickname></ns2:userInfo></refreshAuthTokenResult></detail></SOAP-ENV:Fault></SOAP-ENV:Body></SOAP-ENV:Envelope>";

    [Fact]
    public void Parse_SpotifyFault_ThrowsSmapiException()
    {
        var exception = Assert.Throws<Sonos.Base.Music.SmapiException>(() => Sonos.Base.Music.Models.SoapParser.ParseXml<Sonos.Base.Music.GetAppLinkResponse>(spotifyFault));
        Assert.Equal("tokenRefreshRequired", exception.FaultString);
        Assert.Equal("ns0:Client.TokenRefreshRequired", exception.FaultCode);
        Assert.NotNull(exception.RefreshAuthResult);
        Assert.Equal("xxx", exception.RefreshAuthResult.AuthToken);
        Assert.Equal("aaa/NL/yyy", exception.RefreshAuthResult.PrivateKey);
    }

    private const string spotifyGetMetadata = @"<?xml version=""1.0"" encoding=""utf-8"" ?><SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""><SOAP-ENV:Header/><SOAP-ENV:Body><ns2:getMetadataResponse xmlns:ns2=""http://www.sonos.com/Services/1.1""><ns2:getMetadataResult><ns2:index>0</ns2:index><ns2:count>5</ns2:count><ns2:total>5</ns2:total><ns2:mediaCollection><ns2:id>featured_pls</ns2:id><ns2:itemType>container</ns2:itemType><ns2:title>Popular Playlists</ns2:title><ns2:canPlay>false</ns2:canPlay><ns2:albumArtURI>https://spotify-static.ws.sonos.com/icons/featured_v4_legacy.png</ns2:albumArtURI></ns2:mediaCollection><ns2:mediaCollection><ns2:id>charts</ns2:id><ns2:itemType>collection</ns2:itemType><ns2:displayType>playlistGrid</ns2:displayType><ns2:title>Charts</ns2:title><ns2:canPlay>false</ns2:canPlay><ns2:albumArtURI>https://spotify-static.ws.sonos.com/icons/top_charts_v4_legacy.png</ns2:albumArtURI></ns2:mediaCollection><ns2:mediaCollection><ns2:id>new_releases</ns2:id><ns2:itemType>albumList</ns2:itemType><ns2:displayType>albumsList</ns2:displayType><ns2:title>New Releases</ns2:title><ns2:canPlay>false</ns2:canPlay><ns2:albumArtURI>https://spotify-static.ws.sonos.com/icons/new_releases_v4_legacy.png</ns2:albumArtURI></ns2:mediaCollection><ns2:mediaCollection><ns2:id>genres_and_moods</ns2:id><ns2:itemType>collection</ns2:itemType><ns2:title>Genres and Moods</ns2:title><ns2:canPlay>false</ns2:canPlay><ns2:albumArtURI>https://spotify-static.ws.sonos.com/icons/genres_and_moods_v4_legacy.png</ns2:albumArtURI></ns2:mediaCollection><ns2:mediaCollection><ns2:id>yourmusic_root</ns2:id><ns2:itemType>collection</ns2:itemType><ns2:title>Your Music</ns2:title><ns2:canPlay>false</ns2:canPlay><ns2:containsFavorite>true</ns2:containsFavorite><ns2:albumArtURI>https://spotify-static.ws.sonos.com/icons/your_music_v4_legacy.png</ns2:albumArtURI></ns2:mediaCollection></ns2:getMetadataResult></ns2:getMetadataResponse></SOAP-ENV:Body></SOAP-ENV:Envelope>";
    
    [Fact]

    public void Parse_SpotifyGetMetadata_CreatesCorrectObject()
    {
        var parsedObject = Sonos.Base.Music.Models.SoapParser.ParseXml<Sonos.Base.Music.GetMetadataResponse>(spotifyGetMetadata);
        Assert.NotNull(parsedObject);
        Assert.Equal(0, parsedObject.MetadataResult.Index);
        Assert.Equal(5, parsedObject.MetadataResult.Count);
        Assert.Equal(5, parsedObject.MetadataResult.Total);
        Assert.NotNull(parsedObject.MetadataResult.MediaCollection);
        Assert.Equal(5, parsedObject.MetadataResult.MediaCollection.Length);
        var firstItem = parsedObject.MetadataResult.MediaCollection[0];
        Assert.Equal("featured_pls", firstItem.Id);
        Assert.Equal("container", firstItem.ItemType);
        Assert.Equal("Popular Playlists", firstItem.Title);
        Assert.False(firstItem.CanPlay);
        Assert.Equal("https://spotify-static.ws.sonos.com/icons/featured_v4_legacy.png", firstItem.AlbumArtUri);
        var lastItem = parsedObject.MetadataResult.MediaCollection[4];
        Assert.Equal("yourmusic_root", lastItem.Id);
        Assert.Equal("collection", lastItem.ItemType);
        Assert.Equal("Your Music", lastItem.Title);
        Assert.False(lastItem.CanPlay);
        Assert.True(lastItem.ContainsFavorite);
        Assert.Equal("https://spotify-static.ws.sonos.com/icons/your_music_v4_legacy.png", lastItem.AlbumArtUri);
    }
}
