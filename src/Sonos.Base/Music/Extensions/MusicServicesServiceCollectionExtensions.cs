using Microsoft.Extensions.DependencyInjection;

namespace Sonos.Base;
public static class MusicServicesServiceCollectionExtensions
{
    public static IServiceCollection AddMusicClientSupport(this IServiceCollection services)
    {
        // Register HttpClient with specific configuration
        // these headers are copied from what the Sonos app sends
        services.AddHttpClient(Music.SonosMusicManager.MUSIC_SERVICE_CLIENT_NAME, client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                ["Linux UPnP/1.0 Sonos/29.3-87071 (ICRU_iPhone7,1)"," iOS/Version 8.2 (Build 12D508)"]);
        }).ConfigurePrimaryHttpMessageHandler((_) =>
        {
            return new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };
        });

        services.AddTransient<Music.SonosMusicManager>();
        return services;
    }
}
