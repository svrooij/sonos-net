using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sonos.Base;

namespace Sonos.Cli.Commands
{
    internal static class IHostExtensions
    {
        internal static SonosDevice CreateSonosDeviceWithOptions(this IHost host, BaseOptions options)
        {
            var httpClient = host.Services.GetRequiredService<HttpClient>();
            var logFactory = host.Services.GetService<ILoggerFactory>();

            if (string.IsNullOrEmpty(options.Host))
            {
                throw new ArgumentNullException(nameof(options.Host));
            }

            return new SonosDevice(new Uri($"http://{options.Host}:1400/"), httpClient: httpClient, loggerFactory: logFactory);
        }
    }
}
