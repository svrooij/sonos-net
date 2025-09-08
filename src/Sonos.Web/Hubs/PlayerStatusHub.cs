using Microsoft.AspNetCore.SignalR;

namespace Sonos.Web.Hubs;

public interface IPlayerStatusClient
{
    Task PlayerStatusChanged(string uuid, Sonos.Base.Models.SonosEvent data);
}

/// <summary>
/// Status hub for real-time player updates
/// </summary>
public class PlayerStatusHub : Hub<IPlayerStatusClient>
{
    /// <summary>
    /// Subscribe to updates for a specific player by UUID
    /// </summary>
    /// <param name="uuid">Speaker UUID</param>
    public Task Subscribe(string uuid)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, uuid);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
