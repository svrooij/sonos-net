using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sonos.Base;

namespace Sonos.Cli.Commands;

internal static class IHostExtensions
{
    internal static SonosDevice CreateSonosDeviceWithOptions(this IHost host, BaseOptions options)
    {
        if (string.IsNullOrEmpty(options.Host))
        {
            throw new ArgumentNullException(nameof(options.Host));
        }

        var sonosServiceProvider = host.Services.GetRequiredService<ISonosServiceProvider>();

        return new SonosDevice(new SonosDeviceOptions(new Uri($"http://{options.Host}:1400/"), sonosServiceProvider));
    }

    internal static SonosManager CreateSonosManager(this IHost host)
    {
        return new SonosManager(host.Services.GetRequiredService<ISonosServiceProvider>());
    }
}