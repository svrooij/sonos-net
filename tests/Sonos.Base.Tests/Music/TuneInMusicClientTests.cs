using Xunit;
using Sonos.Base.Music;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Sonos.Base.Music.Models;
using Sonos.Base.Music.Soap;
using System.IO;

namespace Sonos.Base.Tests.Music
{
    
    public class TuneInMusicClientTests
    {
        //private const string SkipReason = "No real tests";
        private const string SkipReason = null;
        private readonly IConfiguration _configuration;
        private readonly MusicClient _musicClient;

        public TuneInMusicClientTests(IConfiguration configuration)
        {
            _configuration = configuration;
            _musicClient = new MusicClient(new MusicClientOptions
            {
                BaseUri = "https://legato.radiotime.com/Radio.asmx",
                DeviceId = _configuration.GetValue<string>("SONOS_DEVICE_ID"),
                HouseholdId = _configuration.GetValue<string>("SONOS_HOUSEHOLD_ID"),
            });

            
        }


        [Theory(Skip = SkipReason)]
        [InlineData("root", true)]
        [InlineData("y1", true)]
        [InlineData("y1:popular",false)]
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
