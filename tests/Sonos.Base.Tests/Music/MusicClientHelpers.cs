using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.Protected;
using Sonos.Base.Music;
using Sonos.Base.Music.Soap;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sonos.Base.Tests.Music
{
    internal static class MusicClientHelpers
    {
        private const string MusicActionHeader = "SOAP-Action";
        private const string MusicResponseFormat = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><s:Body><{0}Response xmlns=""http://www.sonos.com/Services/1.1"">{1}</{0}Response></s:Body></s:Envelope>";
        private const string MusicRequestFormat = @"<?xml version=""1.0"" encoding=""utf-8""?><s:Envelope xmlns:u=""http://www.sonos.com/Services/1.1"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Header>{0}</s:Header><s:Body><u:{1}>{2}</u:{1}></s:Body></s:Envelope>";
        private const string MusicRequestHeaderCredentialFormat = @"<u:credentials><u:deviceId>{0}</u:deviceId></u:credentials>";
        private const string MusicRequestHeaderCredentialTokenKeyHouseholdIdFormat = @"<u:credentials><u:deviceId>{0}</u:deviceId><u:loginToken><u:token>{1}</u:token><u:key>{2}</u:key><u:householdId>{3}</u:householdId></u:loginToken></u:credentials>";
        private const string MusicRequestHeaderContextFormat = @"<u:context><u:timeZone>{0}</u:timeZone></u:context>";

        internal static Mock<HttpClientHandler> MockMusicServiceRequest(this Mock<HttpClientHandler> mock, string action, string? requestBody = null, string responseBody = "", string baseUrl = TestHelpers.defaultUri, string timezone = "+01:00", string deviceId = "", AuthenticationType authenticationType = AuthenticationType.Anonymous, string? key = null, string? token = null, string? householdId = null)
        {
            string response = string.Format(MusicResponseFormat, action, responseBody);
            mock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.VerifyMusicServiceRequest(baseUrl, action, requestBody, timezone, deviceId, authenticationType, key, token, householdId)),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(response)
                });

            return mock;
        }

        internal static Mock<HttpClientHandler> MockMusicServiceRequest<TRequest>(this Mock<HttpClientHandler> mock, string action, TRequest? requestBody = default, string responseBody = "", string baseUrl = TestHelpers.defaultUri, string timezone = "+01:00", string deviceId = "", AuthenticationType authenticationType = AuthenticationType.Anonymous, string? key = null, string? token = null, string? householdId = null) where TRequest : class
        {
            string response = string.Format(MusicResponseFormat, action, responseBody);
            mock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.VerifyMusicServiceRequest<TRequest>(baseUrl, action, requestBody, deviceId, authenticationType, key, token, householdId)),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(response)
                });

            return mock;
        }

        internal static bool VerifyMusicServiceRequest(this HttpRequestMessage message, string baseUrl, string action, string? requestBody, string timezone, string deviceId, AuthenticationType authenticationType, string? key, string? token, string? householdId)
        {
            // TODO: Verify body in tests
            bool bodyChecked = false;
            if (!string.IsNullOrEmpty(requestBody))
            {
                var streamContent = message.Content as StreamContent;

                if (streamContent != null)
                {
                    var content = streamContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    var header = string.Format(MusicRequestHeaderContextFormat, timezone);
                    if (authenticationType == AuthenticationType.Anonymous)
                    {
                        header += string.Format(MusicRequestHeaderCredentialFormat, deviceId);
                    }
                    else if (authenticationType == AuthenticationType.AppLink || authenticationType == AuthenticationType.DeviceLink)
                    {
                        header += string.Format(MusicRequestHeaderCredentialTokenKeyHouseholdIdFormat, deviceId, token , key, householdId);

                    }

                    var expectedContent = string.Format(MusicRequestFormat, header, action, requestBody);
                    
                    bodyChecked = content.Equals(expectedContent);
#if DEBUG
                    Assert.Equal(expectedContent, content);
#endif
                }
            }
            return
                message.RequestUri == new Uri(baseUrl) &&
                message.Headers.ContainsHeaderWithValue(MusicActionHeader, $"http://www.sonos.com/Services/1.1#{action}") &&
                (requestBody == null || bodyChecked);
        }

        internal static bool VerifyMusicServiceRequest<TRequest>(this HttpRequestMessage message, string baseUrl, string action, TRequest? requestBody, string deviceId, AuthenticationType authenticationType, string? key, string? token, string? householdId) where TRequest : class
        {
            // TODO: Verify body in tests
            bool bodyChecked = false;
            if (requestBody != null)
            {
                var streamContent = message.Content as StreamContent;

                if (streamContent != null)
                {
                    var content = streamContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    var envelop = SoapFactory.ParseRequestXml<TRequest>(action, content);

                    Assert.Equivalent(requestBody, envelop.Body.Message);
                    if (AuthenticationType.AppLink == authenticationType || AuthenticationType.DeviceLink == authenticationType)
                    {
                        Assert.NotNull(envelop.Header.Credentials?.LoginToken);
                    }
                    bodyChecked = true;
                }
            }
            return
                message.RequestUri == new Uri(baseUrl) &&
                message.Headers.ContainsHeaderWithValue(MusicActionHeader, $"http://www.sonos.com/Services/1.1#{action}") &&
                (requestBody == null || bodyChecked);
        }

        internal static MusicClientOptions CreateOptions(string baseUri = TestHelpers.defaultUri, int serviceId = 1, AuthenticationType authenticationType = AuthenticationType.Anonymous, string? deviceId = null, string timezone = "+01:00", string? householdId = null, string? key = null, string? token = null)
        {
            IMusicClientCredentialStore? credentialStore = null;
            if (authenticationType == AuthenticationType.AppLink || authenticationType == AuthenticationType.DeviceLink)
            {
                if (deviceId == null)
                {
                    throw new ArgumentNullException(nameof(deviceId));
                }
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (token == null)
                {
                    throw new ArgumentNullException(nameof(token));
                }

                credentialStore = new MemoryMusicServiceAccountStore();
                credentialStore.SaveAccount(serviceId, key, token);
                //var mockedCredentials = new Mock<IMusicClientCredentialStore>();
                //mockedCredentials
                //    .Setup(x => x.GetAccountAsync(It.Is<int>((x) => x==1), It.IsAny<CancellationToken>()))
                //    .Returns(Task.FromResult<MusicClientAccount?>(new MusicClientAccount(key, token)));
                //credentialStore = mockedCredentials.Object;
            }

            return new MusicClientOptions { BaseUri = baseUri, ServiceId = serviceId, AuthenticationType = authenticationType, CredentialStore = credentialStore, DeviceId = deviceId, HouseholdId = householdId, TimeZone = timezone };
        }
    }
}