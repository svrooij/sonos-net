using Microsoft.Extensions.Logging;
using System.Net.Http;
namespace Sonos.Base.Tests
{
    internal class StaticSonosServiceProvider : ISonosServiceProvider
    {
        private readonly HttpClient httpClient;
        private readonly ISonosEventBus? sonosEventBus;

        public StaticSonosServiceProvider(HttpClientHandler? httpClientHandler = null, ISonosEventBus? sonosEventBus = null)
        {
            this.httpClient = httpClientHandler is null ? new HttpClient() : new HttpClient(httpClientHandler);
            this.sonosEventBus = sonosEventBus;
        }

        public ILogger<TCategoryName>? CreateLogger<TCategoryName>() => null;

        public ILogger? CreateLogger(string categoryName) => null;

        public HttpClient GetHttpClient() => httpClient;

        public IHttpClientFactory? GetHttpClientFactory() => null;

        public ILoggerFactory? GetLoggerFactory() => null;

        public ISonosEventBus? GetSonosEventBus() => sonosEventBus;
    }
}
