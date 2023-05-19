using Xunit;
using Sonos.Base.Music;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Sonos.Base.Music.Models;
using Sonos.Base.Music.Soap;
using System.IO;

namespace Sonos.Base.Tests.Music
{
    public class MusicClientTests
    {
        private readonly IConfiguration _configuration;
        private readonly MusicClient _tuneIn;

        private const string _getMetadataXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:s=""http://www.sonos.com/Services/1.1"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Header>
    <s:context>
      <s:timeZone>+01:00</s:timeZone>
    </s:context>
    <s:credentials>
      <s:deviceId>test</s:deviceId>
    </s:credentials>
  </soap:Header>
  <soap:Body>
    <s:getMetadata>
      <s:id>root</s:id>
      <s:index>0</s:index>
      <s:count>100</s:count>
    </s:getMetadata>
  </soap:Body>
</soap:Envelope>";

        public MusicClientTests(IConfiguration configuration)
        {
            _configuration = configuration;
            _tuneIn = new MusicClient(new MusicClientOptions
            {
                BaseUri = "https://legato.radiotime.com/Radio.asmx",
                DeviceId = _configuration.GetValue<string>("SONOS_DEVICE_ID"),
                HouseholdId = _configuration.GetValue<string>("SONOS_HOUSEHOLD_ID"),
            });
        }

        [Fact]
        public void SoapFactory_GeneratesGetMetadataXml()
        {
            using var stream = SoapFactory.GenerateXmlStream("getMetadata", new SoapHeader
            {
                Context = new SoapHeaderContext { TimeZone = "+01:00" },
                Credentials = new SoapHeaderCredentials { DeviceId = "test" }
            }, new GetMetadataRequest { Id = "root" });

            using var reader = new StreamReader(stream);
            var xml = reader.ReadToEnd();

            Assert.Equal(_getMetadataXml, xml);
        }

        [Fact()]
        public async Task TuneInClient_LoadsRoot()
        {
            var obj = await _tuneIn.GetMetadataAsync(new Base.Music.Models.GetMetadataRequest { Id = "root" });


            Assert.NotNull(obj);
            Assert.NotNull(obj?.Result);
            Assert.Equal(7, obj?.Result?.Count);
            Assert.Equal(7, obj?.Result?.MediaCollection?.Length);

            var firstItem = obj?.Result?.MediaCollection?[0];
            Assert.NotNull(firstItem);
            Assert.NotNull(firstItem.AlbumArtURI);
            Assert.StartsWith("http", firstItem.AlbumArtURI);

        }

        [Fact]
        public async Task TuneInClient_LoadsCategoryY1()
        {
            var result = await _tuneIn.GetMetadataAsync(new GetMetadataRequest { Id = "y1" });
            Assert.NotNull(result);

            Assert.Equal(37, result.Result.Total);

            var firstItemasCollection = result.Result.MediaCollection?[0];
            Assert.NotNull(firstItemasCollection);
            Assert.NotNull(firstItemasCollection?.AlbumArtURI);
        }

        [Fact]
        public async Task TuneInClient_LoadsStationsFromY1()
        {
            var obj = await _tuneIn.GetMetadataAsync(new GetMetadataRequest { Id = "c57943:station", Count = 10 });


            Assert.NotNull(obj);
            Assert.NotNull(obj?.Result);
            Assert.Equal(10, obj?.Result?.Count);

        }

        [Fact]
        public async Task TuneInClient_LoadsChannelMetadata()
        {
            var result = await _tuneIn.GetMetadataAsync(new GetMetadataRequest { Id = "s76590", Recursive = true });
            Assert.NotNull(result);
        }

    }
}
