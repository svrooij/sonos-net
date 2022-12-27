using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base;

public interface ISonosServiceProvider
{
    public HttpClient GetHttpClient();
    public IHttpClientFactory? GetHttpClientFactory();
    public ILogger<TCategoryName>? CreateLogger<TCategoryName>();
    public ILogger? CreateLogger(string categoryName);
    public ILoggerFactory? GetLoggerFactory();
    public ISonosEventBus? GetSonosEventBus();
}
