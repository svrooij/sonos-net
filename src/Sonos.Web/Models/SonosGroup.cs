namespace Sonos.Web.Models;

public record SonosGroup (string Id, string GroupName, SonosDevice Coordinator, SonosDevice[]? Members)
{
}

public record SonosDevice(string Uuid, string Name) { }
