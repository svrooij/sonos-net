using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sonos.Base.Services;
using System.Text.Json;

namespace Sonos.Base.Music
{
    public class KvMusicServiceAccountStore : IMusicClientCredentialStore
    {
        private readonly SystemPropertiesService _propertiesService;
        private readonly ILogger<KvMusicServiceAccountStore> _logger;
        private readonly Dictionary<int, MusicClientAccount> store = new Dictionary<int, MusicClientAccount>();

        public KvMusicServiceAccountStore(SystemPropertiesService propertiesService, ILogger<KvMusicServiceAccountStore>? logger)
        {
            _propertiesService = propertiesService;
            _logger = logger ?? NullLogger<KvMusicServiceAccountStore>.Instance;
        }

        public async Task<MusicClientAccount?> GetAccountAsync(int serviceId, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Loading account for service {serviceId}", serviceId);
            if (!store.ContainsKey(serviceId))
            {
                try
                {
                    var key = GetKeyForService(serviceId);
                    var data = await _propertiesService.GetStringAsync(key, cancellationToken);
                    var account = JsonSerializer.Deserialize<MusicClientAccount>(data);
                    if (account == null)
                    {
                        return null;
                    }
                    store[serviceId] = account;
                    return account;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error getting account for {serviceId}", serviceId);
                    return null;
                }
            }
            return store[serviceId];
        }

        public async Task<bool> SaveAccountAsync(int serviceId, string key, string token, CancellationToken cancellationToken = default)
        {
            var account = new MusicClientAccount(key, token);
            store[serviceId] = account;

            try
            {
                var result = await _propertiesService.SetStringAsync(GetKeyForService(serviceId), JsonSerializer.Serialize(account), cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error saving account for {serviceId}", serviceId);
                return false;
            }
        }

        private string GetKeyForService(int serviceId) => $"SonosNet-Music-{serviceId}";
    }
}