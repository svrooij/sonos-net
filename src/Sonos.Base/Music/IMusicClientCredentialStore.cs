namespace Sonos.Base.Music
{
    public interface IMusicClientCredentialStore
    {
        Task<MusicClientAccount?> GetAccountAsync(int serviceId, CancellationToken cancellationToken);

        Task<bool> SaveAccount(int serviceId, string key, string token, CancellationToken cancellationToken);
    }

    public record MusicClientAccount(string Key, string Token);
}