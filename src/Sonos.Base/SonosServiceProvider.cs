using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base
{
    /// <summary>
    /// SonosServiceProvider can help the library get the required services from dependency injection
    /// </summary>
    public class SonosServiceProvider : ISonosServiceProvider
    {
        private readonly IHttpClientFactory? httpClientFactory;
        private readonly ILoggerFactory? loggerFactory;
        private readonly ISonosEventBus? bus;

        public SonosServiceProvider(IHttpClientFactory? httpClientFactory = null, ILoggerFactory? loggerFactory = null, ISonosEventBus? bus = null)
        {
            this.httpClientFactory = httpClientFactory;
            this.loggerFactory = loggerFactory;
            this.bus = bus;
        }

        public HttpClient GetHttpClient() => httpClientFactory?.CreateClient() ?? new HttpClient();

        public IHttpClientFactory? GetHttpClientFactory() => httpClientFactory;

        public ILogger<TCategoryName>? CreateLogger<TCategoryName>() => loggerFactory?.CreateLogger<TCategoryName>();

        public ILogger? CreateLogger(string categoryName) => loggerFactory?.CreateLogger(categoryName);

        public ILoggerFactory? GetLoggerFactory() => loggerFactory;

        public ISonosEventBus? GetSonosEventBus() => bus;
    }
}
