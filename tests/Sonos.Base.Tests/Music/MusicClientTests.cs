using Moq;
using Sonos.Base.Music;
using Sonos.Base.Music.Models;
using Sonos.Base.Music.Soap;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sonos.Base.Tests.Music
{
    public class MusicClientTests
    {
        public MusicClientTests()
        {
        }

        [Fact]
        private void SoapFactory_GeneratesGetMetadataXml()
        {
            using var stream = SoapFactory.GenerateXmlStream("getMetadata", new SoapHeader
            {
                Context = new SoapHeaderContext { TimeZone = "+01:00" },
                Credentials = new SoapHeaderCredentials { DeviceId = "test" }
            }, new GetMetadataRequest { Id = "root" });

            using var reader = new StreamReader(stream);
            var xml = reader.ReadToEnd();

            Assert.Equal(MusicClientConstants._getMetadataRequestXml, xml);
        }

        //[Fact]
        private void SoapFactory_ParseXml_GetMetadataResponse_ReturnsObject()
        {
            var result = SoapFactory.ParseXml<GetMetadataResponse>("getMetadata", MusicClientConstants.getMetadataResponseXml);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(AuthenticationType.Anonymous)]
        [InlineData(AuthenticationType.AppLink)]
        [InlineData(AuthenticationType.DeviceLink)]
        public async Task GetMetadata_Credentials_CreatesCorrectRequest(AuthenticationType authenticationType)
        {
            var id = "root";
            string deviceId = Guid.NewGuid().ToString();
            string householdId = Guid.NewGuid().ToString();
            string key = Guid.NewGuid().ToString();
            string token = Guid.NewGuid().ToString();


            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockMusicServiceRequest("getMetadata", $"<u:id>{id}</u:id><u:index>0</u:index><u:count>100</u:count>", responseBody: MusicClientConstants.getMetadataResponseBody, deviceId: deviceId, authenticationType: authenticationType, key: key, token: token, householdId: householdId);

            var musicClient = new MusicClient(MusicClientHelpers.CreateOptions(deviceId: deviceId, authenticationType: authenticationType, token: token, key: key, householdId: householdId), new HttpClient(mockedHandler.Object));
            var result = await musicClient.GetMetadataAsync(new GetMetadataRequest { Id = id });

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);
            Assert.Equal(7, result.Total);
            Assert.Equal(2, result.MediaCollection?.Length);

        }
    }
}