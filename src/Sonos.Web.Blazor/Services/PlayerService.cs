using Sonos.Web.Blazor.Client;

namespace Sonos.Web.Blazor.Services;

public class PlayerService
{
    private readonly SonosWebClient _sonosWebClient;
    public PlayerService(SonosWebClient sonosWebClient)
    {
        _sonosWebClient = sonosWebClient;
    }

    public async Task Initialize()
    {
        if (Players.Count > 0)
        {
            return;
        }
        var players = await _sonosWebClient.Zones.GetAsync();
        foreach(var p in players!)
        {
            Players.Add(new PlayerInfo(p.Coordinator!.Uuid!, p.GroupName));
        }
    }

    public List<PlayerInfo> Players { get; private set; } = new List<PlayerInfo>();
    private string? _selectedPlayerId;
    public void SelectPlayer(string? id)
    {
        if(string.IsNullOrEmpty(id))
        {
            _selectedPlayerId = null;
        } else
        {
            _selectedPlayerId = Players.FirstOrDefault(p => p.Id == id)?.Id;
        }

    }

    public PlayerInfo? SelectedPlayer => string.IsNullOrEmpty(_selectedPlayerId) ? null : Players.First(p => p.Id == _selectedPlayerId);
}

public record PlayerInfo(string Id, string? Name);
