using Xunit;
using Sonos.Base.Music;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Sonos.Base.Music.Models;
using Sonos.Base.Music.Soap;
using System.IO;
using System;
using Xunit.DependencyInjection;

namespace Sonos.Base.Tests.Music
{
    [Startup(typeof(Startup))]
    public class SpotifyMusicClientTests
    {
        //private const string SkipReason = "No real tests";
        private const string SkipReason = null;
        private readonly IConfiguration _configuration;
        private readonly MusicClient _musicClient;

        string key;
        string token;

        public SpotifyMusicClientTests(IConfiguration configuration)
        {
            _configuration = configuration;
            key = _configuration.GetValue<string?>("SONOS:SPOTIFYKEY") ?? Guid.NewGuid().ToString();
            token = _configuration.GetValue<string?>("SONOS:SPOTIFYTOKEN") ?? Guid.NewGuid().ToString();
            _musicClient = new MusicClient(MusicClientHelpers.CreateOptions(
                baseUri: "https://spotify-v5.ws.sonos.com/smapi",
                serviceId: 9,
                AuthenticationType.AppLink,
                deviceId: _configuration.GetValue<string>("SONOS_DEVICE_ID"),
                householdId: _configuration.GetValue<string>("SONOS_HOUSEHOLD_ID"), key: key, token: token));

            
        }

        [Fact]
        public async Task MusicClient_LoadsAppLink()
        {
            var result = await _musicClient.GetAppLinkAsync();
            Assert.NotNull(result);
            Assert.NotNull(result.AuthorizeAccount);
            Assert.NotNull(result.AuthorizeAccount.AppUrlStringId);
            Assert.NotNull(result.AuthorizeAccount.DeviceLink);
            Assert.StartsWith("https://", result.AuthorizeAccount.DeviceLink.RegistrationUrl);
        }

        [Fact]
        public async Task MusicClient_LoadsDeviceAuthCode()
        {
            var linkCode = "2IQJVQ";
            var result = await _musicClient.GetDeviceAuthTokenAsync(linkCode);
            Assert.NotNull(result);
        }


        [Theory(Skip = SkipReason)]
        [InlineData("root", true)]
        //[InlineData("y1", true)]
        //[InlineData("y1:popular",false)]
        public async Task MusicClient_LoadsMetadataForId(string id, bool collection)
        {
            var obj = await _musicClient.GetMetadataAsync(new Base.Music.Models.GetMetadataRequest { Id = id, Count = 10 });


            Assert.NotNull(obj);
            Assert.True(obj.Count > 0);

            if (collection)
            {
                Assert.NotNull(obj.MediaCollection);
                Assert.Null(obj.MediaMetadata);
                Assert.Equal(obj.Count, obj.MediaCollection?.Length);
            } else
            {
                Assert.NotNull(obj.MediaMetadata);
                Assert.Null(obj.MediaCollection);
                Assert.Equal(obj.Count, obj.MediaMetadata?.Length);
            }
        }

    }
}
