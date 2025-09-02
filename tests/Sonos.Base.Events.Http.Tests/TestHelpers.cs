using Microsoft.AspNetCore.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Events.Http.Tests
{
    internal static class TestHelpers
    {
        internal static Mock<HttpClientHandler> MockEventRequest(this Mock<HttpClientHandler> handler, Uri uri, string method, Dictionary<string,string>? verifyHeaders = null, Dictionary<string,string>? responseHeaders = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            

            

            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.VerifyEventRequest(uri, method, verifyHeaders)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(StatusResponseMessage(statusCode, responseHeaders));
            return handler;
        }

        internal static bool VerifyEventRequest(this HttpRequestMessage message, Uri uri, string method, Dictionary<string, string>? verifyHeaders = null)
        {
            var result = message.RequestUri == uri && message.Method == new HttpMethod(method);

            if (verifyHeaders is null || !result)
            {
                return result;
            }

            foreach(var kvp in verifyHeaders)
            {
                if (!message.Headers.TryGetValues(kvp.Key, out var values) || !values.Contains(kvp.Value))
                {
                    return false;
                }
            }
            return true;
        }

        internal static HttpResponseMessage StatusResponseMessage(HttpStatusCode statusCode, Dictionary<string, string>? headers = null)
        {
            var message = new HttpResponseMessage
            {
                StatusCode = statusCode
            };

            if (headers is not null)
            {
                foreach (var header in headers)
                {
                    message.Headers.Add(header.Key, header.Value);
                }
            }

            return message;
        }
    }
}
