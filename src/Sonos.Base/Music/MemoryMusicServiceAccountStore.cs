using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Music
{
    internal class MemoryMusicServiceAccountStore : IMusicClientCredentialStore
    {
        private readonly ILogger<MemoryMusicServiceAccountStore> logger;

        public MemoryMusicServiceAccountStore(ILogger<MemoryMusicServiceAccountStore>? logger = null)
        {
            this.logger = logger ?? NullLogger<MemoryMusicServiceAccountStore>.Instance;
        }

        private readonly Dictionary<int, MusicClientAccount> store = new Dictionary<int, MusicClientAccount>();
        public async Task<MusicClientAccount?> GetAccountAsync(int serviceId, CancellationToken cancellationToken = default)
        {
            logger.LogDebug("Loading account for service {serviceId}", serviceId);
            return store.ContainsKey(serviceId) ? store[serviceId] : null;
        }

        public async Task<bool> SaveAccount(int serviceId, string key, string token, CancellationToken cancellationToken = default)
        {
            logger.LogDebug("Saving account for service {serviceId}", serviceId);
            if (store.ContainsKey(serviceId))
            {
                store[serviceId] = new MusicClientAccount(key, token);
            }
            else
            {
                store.Add(serviceId, new MusicClientAccount(key, token));
            }
            return true;
        }
    }
}
