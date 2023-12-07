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

using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Sonos.Base.Tests;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sonos.Base
{
    public static class TestHelpers
    {
        internal const string defaultUri = "http://localhost/";
        private const string SoapActionHeader = "soapaction";

        private const string SoapResponseFormat = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:{1}Response xmlns:u=""urn:schemas-upnp-org:service:{0}:1"">{2}</u:{1}Response></s:Body></s:Envelope>";
        private const string SoapRequestFormat = @"<?xml version=""1.0"" encoding=""utf-8""?><s:Envelope s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body><u:{1} xmlns:u=""urn:schemas-upnp-org:service:{0}:1"">{2}</u:{1}></s:Body></s:Envelope>";

        
        public static Mock<HttpClientHandler> MockDeviceDescription(this Mock<HttpClientHandler> mock, string deviceDescription, string baseUrl = defaultUri)
        {
            mock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.RequestUri == new Uri(new Uri(baseUrl), "/xml/device_description.xml")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(deviceDescription)
                });

            return mock;
        }

        /// <summary>
        /// Mock a specific request to sonos speakers.
        /// </summary>
        /// <param name="mock">Pre-existing mock, `new Mock<HttpClientHandler>()`</param>
        /// <param name="service">Service name</param>
        /// <param name="action">Action name</param>
        /// <param name="requestBody">(optionally) required body, not checked at the moment</param>
        /// <param name="responseBody">(optionally) Response that will be returned, only the part inside the `...<u:{action}Response>{this-part}</u:{action}Resonse>...` is required.</param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static Mock<HttpClientHandler> MockSonosRequest(this Mock<HttpClientHandler> mock, string service, string action, string? requestBody = null, string responseBody = "", string baseUrl = defaultUri)
        {
            string response = string.Format(SoapResponseFormat, service, action, responseBody);
            mock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.VerifySonosRequest(baseUrl, service, action, requestBody)),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(response)
                });

            return mock;
        }

        internal static bool VerifySonosRequest(this HttpRequestMessage message, string baseUrl, string service, string action, string? requestBody)
        {
            // TODO: Verify body in tests
            bool bodyChecked = false;
            if (requestBody != null)
            {
                var streamContent = message.Content as StreamContent;
                
                if (streamContent != null)
                {
                    var content = streamContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    var expectedContent = string.Format(SoapRequestFormat, service, action, requestBody);
#if DEBUG
                    Assert.Equal(expectedContent, content);
#endif
                    bodyChecked = content.Equals(expectedContent);
                }
            }
            return
                message.RequestUri == new Uri(new Uri(baseUrl), GetPathForService(service)) &&
                message.Headers.ContainsHeaderWithValue(SoapActionHeader, $"urn:schemas-upnp-org:service:{service}:1#{action}") &&
                (requestBody == null || bodyChecked);
        }

//        public static Mock<HttpClientHandler> MockMusicServiceRequest(this Mock<HttpClientHandler> mock, string action, string? requestBody = null, string responseBody = "", string baseUrl = defaultUri)
//        {
//            string response = string.Format(MusicResponseFormat, action, responseBody);
//            mock
//                .Protected()
//                .Setup<Task<HttpResponseMessage>>("SendAsync",
//                    ItExpr.Is<HttpRequestMessage>(m => m.VerifyMusicServiceRequest(baseUrl, action, requestBody)),
//                    ItExpr.IsAny<CancellationToken>()
//                ).ReturnsAsync(new HttpResponseMessage
//                {
//                    StatusCode = System.Net.HttpStatusCode.OK,
//                    Content = new StringContent(response)
//                });

//            return mock;
//        }

//        internal static bool VerifyMusicServiceRequest(this HttpRequestMessage message, string baseUrl, string action, string? requestBody)
//        {
//            // TODO: Verify body in tests
//            bool bodyChecked = false;
//            if (requestBody != null)
//            {
//                var streamContent = message.Content as StreamContent;

//                if (streamContent != null)
//                {
//                    var content = streamContent.ReadAsStringAsync().GetAwaiter().GetResult();
//                    var expectedContent = string.Format(MusicRequestFormat, action, requestBody);
//                    bodyChecked = content.Equals(expectedContent);
//#if DEBUG
//                    Assert.Equal(expectedContent, content);
//#endif
//                }
//            }
//            return
//                message.RequestUri == new Uri(baseUrl) &&
//                message.Headers.ContainsHeaderWithValue(MusicActionHeader, $"http://www.sonos.com/Services/1.1#{action}") &&
//                (requestBody == null || bodyChecked);
//        }

        internal static bool ContainsHeaderWithValue(this HttpRequestHeaders headers, string key, string value)
        {
            return headers.TryGetValues(key, out var values) && values.Contains(value);
        }

        internal static string GetPathForService(string service) => service switch
        {
            "AlarmClock" => "/AlarmClock/Control",
            "AudioIn" => "/AudioIn/Control",
            "AVTransport" => "/MediaRenderer/AVTransport/Control",
            "ConnectionManager" => "/MediaRenderer/ConnectionManager/Control",
            "ContentDirectory" => "/MediaServer/ContentDirectory/Control",
            "DeviceProperties" => "/DeviceProperties/Control",
            "GroupManagement" => "/GroupManagement/Control",
            "GroupRenderingControl" => "/MediaRenderer/GroupRenderingControl/Control",
            "HTControl" => "/HTControl/Control",
            "MusicServices" => "/MusicServices/Control",
            "Queue" => "/MediaRenderer/Queue/Control",
            "RenderingControl" => "/MediaRenderer/RenderingControl/Control",
            "SystemProperties" => "/SystemProperties/Control",
            "VirtualLineIn" => "/MediaRenderer/VirtualLineIn/Control",
            "ZoneGroupTopology" => "/ZoneGroupTopology/Control",
            _ => throw new NotImplementedException()
        };

        internal static Uri DefaultUri => new Uri(defaultUri);

        internal static IServiceCollection CreateProviderWithClientHandler(HttpClientHandler clientHandler)
        {
            var result = new ServiceCollection();
            result.AddMockedHttpClient(clientHandler);
            return result;
        }

        internal static IServiceCollection AddMockedHttpClient(this IServiceCollection services, HttpClientHandler clientHandler)
        {
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient(clientHandler);
            mockHttpClientFactory.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(client);
            services.AddTransient<IHttpClientFactory>(_ => mockHttpClientFactory.Object);
            return services;
        }

    }
}