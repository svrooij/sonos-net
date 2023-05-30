namespace Sonos.Base.Music
{
    public interface IMusicClientCredentialStore
    {
        Task<MusicClientAccount?> GetAccountAsync(int serviceId, CancellationToken cancellationToken = default);

        Task<bool> SaveAccountAsync(int serviceId, string key, string token, CancellationToken cancellationToken = default);
    }

    public record MusicClientAccount(string Key, string Token);
}