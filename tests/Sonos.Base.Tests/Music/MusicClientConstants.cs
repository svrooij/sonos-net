using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Tests.Music
{
    internal class MusicClientConstants
    {
        internal const string _getMetadataRequestXml = @"<?xml version=""1.0"" encoding=""utf-8""?><s:Envelope xmlns:u=""http://www.sonos.com/Services/1.1"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Header><u:context><u:timeZone>+01:00</u:timeZone></u:context><u:credentials><u:deviceId>test</u:deviceId></u:credentials></s:Header><s:Body><u:getMetadata><u:id>root</u:id><u:index>0</u:index><u:count>100</u:count></u:getMetadata></s:Body></s:Envelope>";

        internal const string getMetadataResponseXml = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <s:Body>
    <getMetadataResponse xmlns=""http://www.sonos.com/Services/1.1"">
      <getMetadataResult>
        <index>0</index>
        <count>7</count>
        <total>7</total>
        <mediaCollection>
          <id>featured:c100003799</id>
          <title>TuneIn Recommends</title>
          <itemType>container</itemType>
          <authRequired>false</authRequired>
          <canPlay>false</canPlay>
          <canEnumerate>true</canEnumerate>
          <canCache>true</canCache>
          <homogeneous>false</homogeneous>
          <canAddToFavorite>false</canAddToFavorite>
          <canScroll>false</canScroll>
          <albumArtURI>http://cdn-albums.tunein.com/sonos/channel_legacy.png</albumArtURI>
          <releaseDate>0001-01-01T00:00:00</releaseDate>
        </mediaCollection>
        <mediaCollection>
          <id>y1</id>
          <title>Music</title>
          <itemType>container</itemType>
          <authRequired>false</authRequired>
          <canPlay>false</canPlay>
          <canEnumerate>true</canEnumerate>
          <canCache>true</canCache>
          <homogeneous>false</homogeneous>
          <canAddToFavorite>false</canAddToFavorite>
          <canScroll>false</canScroll>
          <albumArtURI>http://cdn-albums.tunein.com/sonos/music_legacy.png</albumArtURI>
          <releaseDate>0001-01-01T00:00:00</releaseDate>
        </mediaCollection>
        <mediaCollection>
          <id>c57922</id>
          <title>News &amp; Talk</title>
          <itemType>container</itemType>
          <authRequired>false</authRequired>
          <canPlay>false</canPlay>
          <canEnumerate>true</canEnumerate>
          <canCache>true</canCache>
          <homogeneous>false</homogeneous>
          <canAddToFavorite>false</canAddToFavorite>
          <canScroll>false</canScroll>
          <albumArtURI>http://cdn-albums.tunein.com/sonos/news_legacy.png</albumArtURI>
          <releaseDate>0001-01-01T00:00:00</releaseDate>
        </mediaCollection>
        <mediaCollection>
          <id>y5</id>
          <title>Sports</title>
          <itemType>container</itemType>
          <authRequired>false</authRequired>
          <canPlay>false</canPlay>
          <canEnumerate>true</canEnumerate>
          <canCache>true</canCache>
          <homogeneous>false</homogeneous>
          <canAddToFavorite>false</canAddToFavorite>
          <canScroll>false</canScroll>
          <albumArtURI>http://cdn-albums.tunein.com/sonos/sports_legacy.png</albumArtURI>
          <releaseDate>0001-01-01T00:00:00</releaseDate>
        </mediaCollection>
        <mediaCollection>
          <id>y2</id>
          <title>Talk</title>
          <itemType>container</itemType>
          <authRequired>false</authRequired>
          <canPlay>false</canPlay>
          <canEnumerate>true</canEnumerate>
          <canCache>true</canCache>
          <homogeneous>false</homogeneous>
          <canAddToFavorite>false</canAddToFavorite>
          <canScroll>false</canScroll>
          <albumArtURI>http://cdn-albums.tunein.com/sonos/talk_legacy.png</albumArtURI>
          <releaseDate>0001-01-01T00:00:00</releaseDate>
        </mediaCollection>
        <mediaCollection>
          <id>y4</id>
          <title>Trending</title>
          <itemType>container</itemType>
          <authRequired>false</authRequired>
          <canPlay>false</canPlay>
          <canEnumerate>true</canEnumerate>
          <canCache>true</canCache>
          <homogeneous>false</homogeneous>
          <canAddToFavorite>false</canAddToFavorite>
          <canScroll>false</canScroll>
          <albumArtURI>http://cdn-albums.tunein.com/sonos/trending_legacy.png</albumArtURI>
          <releaseDate>0001-01-01T00:00:00</releaseDate>
        </mediaCollection>
        <mediaCollection>
          <id>r0</id>
          <title>Location</title>
          <itemType>container</itemType>
          <authRequired>false</authRequired>
          <canPlay>false</canPlay>
          <canEnumerate>true</canEnumerate>
          <canCache>true</canCache>
          <homogeneous>false</homogeneous>
          <canAddToFavorite>false</canAddToFavorite>
          <canScroll>false</canScroll>
          <albumArtURI>http://cdn-albums.tunein.com/sonos/location_legacy.png</albumArtURI>
          <releaseDate>0001-01-01T00:00:00</releaseDate>
        </mediaCollection>
      </getMetadataResult>
    </getMetadataResponse>
  </s:Body>
</s:Envelope>";

        internal const string getMetadataResponseBody = @"<getMetadataResult> <index>0</index> <count>7</count> <total>7</total> <mediaCollection> <id>featured:c100003799</id> <title>TuneIn Recommends</title> <itemType>container</itemType> <authRequired>false</authRequired> <canPlay>false</canPlay> <canEnumerate>true</canEnumerate> <canCache>true</canCache> <homogeneous>false</homogeneous> <canAddToFavorite>false</canAddToFavorite> <canScroll>false</canScroll> <albumArtURI>http://cdn-albums.tunein.com/sonos/channel_legacy.png</albumArtURI> <releaseDate>0001-01-01T00:00:00</releaseDate> </mediaCollection> <mediaCollection> <id>y1</id> <title>Music</title> <itemType>container</itemType> <authRequired>false</authRequired> <canPlay>false</canPlay> <canEnumerate>true</canEnumerate> <canCache>true</canCache> <homogeneous>false</homogeneous> <canAddToFavorite>false</canAddToFavorite><canScroll>false</canScroll><albumArtURI>http://cdn-albums.tunein.com/sonos/music_legacy.png</albumArtURI><releaseDate>0001-01-01T00:00:00</releaseDate></mediaCollection></getMetadataResult>";

        internal const string getDeviceAuthTokenResponse = @"
            <getDeviceAuthTokenResult>
                <authToken>dfadfagsfdgsfgsgfsgfsgfsgfghdsgfjhgdfjfgtsfgdxsfghewagsdrte</authToken>
                <privateKey>sdfggfhdgjhdghsdfgsfg/dfafgasfg/fashgrfsh/fshfgshgh</privateKey>
                <userInfo>
                    <userIdHashCode>somehash</userIdHashCode>
                    <accountTier>paidPremium</accountTier>
                    <nickname>yeah_right</nickname>
                </userInfo>
            </getDeviceAuthTokenResult>";

        internal static string RefreshTokenError(string newKey, string newToken)
        {
            return string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Header />
    <soapenv:Body>
        <soapenv:Fault xmlns=""SOAP-ENV"">
            <faultcode xmlns="""">:Client.TokenRefreshRequired</faultcode>
            <faultstring xmlns="""" xml:lang=""en"">tokenRefreshRequired</faultstring>
            <detail xmlns="""">
                <refreshAuthTokenResult xmlns:ns2=""http://www.sonos.com/Services/1.1"">
                    <ns2:authToken>{0}</ns2:authToken>
                    <ns2:privateKey>{1}</ns2:privateKey>
                    <ns2:userInfo>
                        <ns2:userIdHashCode>somehash</ns2:userIdHashCode>
                        <ns2:accountTier>paidPremium</ns2:accountTier>
                        <ns2:nickname>yeah_right</ns2:nickname>
                    </ns2:userInfo>
                </refreshAuthTokenResult>
            </detail>
        </soapenv:Fault>
    </soapenv:Body>
</soapenv:Envelope>", newToken, newKey);
        }

        internal static string FullRefreshMessage = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Header />
    <soapenv:Body>
        <soapenv:Fault xmlns=""SOAP-ENV"">
            <faultcode xmlns="""">:Client.TokenRefreshRequired</faultcode>
            <faultstring xmlns="""" xml:lang=""en"">tokenRefreshRequired</faultstring>
            <detail xmlns="""">
                <refreshAuthTokenResult xmlns:ns2=""http://www.sonos.com/Services/1.1"">
                    <ns2:authToken>asgfgsfgsfgs</ns2:authToken>
                    <ns2:privateKey>sfghsdhdghjsgfd</ns2:privateKey>
                    <ns2:userInfo>
                        <ns2:userIdHashCode>rfgdshgfsxfg</ns2:userIdHashCode>
                        <ns2:accountTier>paidPremium</ns2:accountTier>
                        <ns2:nickname>dafdagfdshgdfg</ns2:nickname>
                    </ns2:userInfo>
                </refreshAuthTokenResult>
            </detail>
        </soapenv:Fault>
    </soapenv:Body>
</soapenv:Envelope>";
    }
}
