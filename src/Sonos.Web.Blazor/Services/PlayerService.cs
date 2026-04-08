using Microsoft.AspNetCore.SignalR.Client;
using Sonos.Web.Blazor.Client;
using Sonos.Web.Blazor.Models;

namespace Sonos.Web.Blazor.Services;

public class PlayerService : IAsyncDisposable
{
    private readonly SonosWebClient _sonosWebClient;
    private HubConnection? _hubConnection;

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

    public async Task InitializeSignalR(Uri hubUri)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUri)
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<string, SonosEvent>("PlayerStatusChanged", (speakerId, status) =>
        {
            if (speakerId == _selectedPlayerId)
            {
                CurrentStatus = status;
                OnStatusChanged?.Invoke(speakerId, status);
            }
        });

        await _hubConnection.StartAsync();
    }

    public List<PlayerInfo> Players { get; private set; } = new List<PlayerInfo>();
    private string? _selectedPlayerId;

    public SonosEvent? CurrentStatus { get; private set; }

    public event Action<string?>? OnPlayerChanged;
    public event Action<string, SonosEvent>? OnStatusChanged;

    public async Task SelectPlayerAsync(string? id)
    {
        var previousId = _selectedPlayerId;
        if(string.IsNullOrEmpty(id))
        {
            _selectedPlayerId = null;
        } else
        {
            _selectedPlayerId = Players.FirstOrDefault(p => p.Id == id)?.Id;
        }

        if (_selectedPlayerId != previousId)
        {
            CurrentStatus = null;
            OnPlayerChanged?.Invoke(_selectedPlayerId);

            if (_selectedPlayerId is not null)
            {
                await RefreshStatusAsync();
                await SubscribeAsync();
            }
        }
    }

    public async Task RefreshStatusAsync()
    {
        if (_selectedPlayerId is null) return;

        try
        {
            var status = await _sonosWebClient.Speakers[_selectedPlayerId].Status.GetAsync();
            if (status is not null)
            {
                CurrentStatus = status.ToSonosEvent();
                OnStatusChanged?.Invoke(_selectedPlayerId, CurrentStatus);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching status: {ex.Message}");
        }
    }

    private async Task SubscribeAsync()
    {
        if (_selectedPlayerId is null) return;
        if (_hubConnection?.State == HubConnectionState.Connected)
        {
            await _hubConnection.InvokeAsync<string>("Subscribe", _selectedPlayerId);
        }
    }

    public PlayerInfo? SelectedPlayer => string.IsNullOrEmpty(_selectedPlayerId) ? null : Players.First(p => p.Id == _selectedPlayerId);

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}

public record PlayerInfo(string Id, string? Name);
