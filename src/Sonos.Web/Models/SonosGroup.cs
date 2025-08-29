namespace Sonos.Web.Models;

public record SonosGroup (string Id, string GroupName, string Coordinator, string[]? Members)
{
}
