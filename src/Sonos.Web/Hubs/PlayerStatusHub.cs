using Microsoft.AspNetCore.SignalR;

namespace Sonos.Web.Hubs;
/// <summary>
/// Interface for the PlayerStatusHub client methods
/// </summary>
public interface IPlayerStatusClient
{
    /// <summary>
    /// Player status updates
    /// </summary>
    /// <param name="uuid">UUID of sonos speaker</param>
    /// <param name="data">status data</param>
    /// <returns></returns>
    Task PlayerStatusChanged(string uuid, Sonos.Web.Models.SonosEvent data);

    /// <summary>
    /// Subscribe to updates for a specific player by UUID
    /// </summary>
    /// <param name="uuid">UUID of the sonos speaker</param>
    /// <returns></returns>
    /// <remarks>You're only getting statuses for speakers you subscribed to</remarks>
    Task Subscribe(string uuid);
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

    /// <inheritdoc/>
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
