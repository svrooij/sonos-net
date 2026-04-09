namespace Sonos.Web.Models;
/// <summary>
/// Sonos group, consisting of a coordinator and zero or more members
/// </summary>
/// <param name="Id">Sonos group id, will change if you change anything to this group</param>
/// <param name="GroupName">Name of the group</param>
/// <param name="Coordinator">The player responsible for the queue</param>
/// <param name="Members">All the members in this group</param>
public record SonosGroup (string Id, string GroupName, SonosDevice Coordinator, SonosDevice[]? Members)
{
}

/// <summary>
/// Represents a Sonos device with a unique identifier and a name.
/// </summary>
/// <param name="Uuid">The unique identifier of the Sonos device.</param>
/// <param name="Name">The name of the Sonos device.</param>
public record SonosDevice(string Uuid, string Name) { }
