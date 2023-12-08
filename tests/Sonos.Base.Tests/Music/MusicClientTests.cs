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
            mockedHandler.MockMusicServiceRequest<GetMetadataRequest>("getMetadata", new GetMetadataRequest { Id = id }, responseBody: MusicClientConstants.getMetadataResponseBody, deviceId: deviceId, authenticationType: authenticationType, key: key, token: token, householdId: householdId);

            var musicClient = new MusicClient(MusicClientHelpers.CreateOptions(deviceId: deviceId, authenticationType: authenticationType, token: token, key: key, householdId: householdId), new HttpClient(mockedHandler.Object));
            var result = await musicClient.GetMetadataAsync(new GetMetadataRequest { Id = id });

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);
            Assert.Equal(7, result.Total);
            Assert.Equal(2, result.MediaCollection?.Length);

        }

        [Fact]
        public async Task GetDeviceAuthTokenAsync_CreatesCorrectRequestAndParsedResponse()
        {
            var linkCode = Guid.NewGuid().ToString();
            var deviceId = Guid.NewGuid().ToString();
            var householdId = Guid.NewGuid().ToString();
            var key = "";
            var token = "";

            var mockedHandler = new Mock<HttpClientHandler>();
            //mockedHandler.MockMusicServiceRequest("getMetadata", $"<u:householdId>{householdId}</u:householdId><u:linkCode>{linkCode}</u:linkCode><u:linkDeviceId>{deviceId}</u:linkDeviceId>", responseBody: MusicClientConstants.getDeviceAuthTokenResponse, deviceId: deviceId, authenticationType: AuthenticationType.AppLink, key: key, token: token, householdId: householdId);

            mockedHandler.MockMusicServiceRequest<GetDeviceAuthTokenRequest>("getDeviceAuthToken", requestBody: new GetDeviceAuthTokenRequest { HouseholdId = householdId, LinkCode = linkCode, LinkDeviceId = deviceId }, responseBody: MusicClientConstants.getDeviceAuthTokenResponse, authenticationType: AuthenticationType.AppLink, deviceId: deviceId, key: key, token: token, householdId: householdId); 

            var musicClient = new MusicClient(MusicClientHelpers.CreateOptions(deviceId: deviceId, authenticationType: AuthenticationType.AppLink, token: token, key: key, householdId: householdId), new HttpClient(mockedHandler.Object));
            var result = await musicClient.GetDeviceAuthTokenAsync(new GetDeviceAuthTokenRequest { HouseholdId = householdId, LinkCode = linkCode, LinkDeviceId = deviceId });
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMetadata_PicksUpTokenFromSoapFault()
        {
            var metadataId = "root";
            var deviceId = Guid.NewGuid().ToString();
            var householdId = Guid.NewGuid().ToString();
            var key = Guid.NewGuid().ToString();
            var token = Guid.NewGuid().ToString() ;

            var newKey = Guid.NewGuid().ToString();
            var newToken = Guid.NewGuid().ToString();

            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockMusicServiceRequest<GetMetadataRequest>("getMetadata", requestBody: new GetMetadataRequest { Id = metadataId }, responseBody: MusicClientConstants.RefreshTokenError(newKey, newToken), authenticationType: AuthenticationType.AppLink, deviceId: deviceId, key: key, token: token, householdId: householdId, httpStatusCode: System.Net.HttpStatusCode.Unauthorized, packResponse: false);

            var musicClient = new MusicClient(MusicClientHelpers.CreateOptions(deviceId: deviceId, authenticationType: AuthenticationType.AppLink, token: token, key: key, householdId: householdId), new HttpClient(mockedHandler.Object));
            await Assert.ThrowsAsync<MusicClientException>(() => musicClient.GetMetadataAsync(new GetMetadataRequest { Id = metadataId }));
        }

        [Fact]
        public void SoapFactory_ParsesFault()
        {
            var result = SoapFactory.ParseException(MusicClientConstants.FullRefreshMessage);
            Assert.NotNull(result?.Code);
            Assert.NotNull(result?.Message);
            Assert.NotNull(result?.Details?.NewToken);
        }
    }
}