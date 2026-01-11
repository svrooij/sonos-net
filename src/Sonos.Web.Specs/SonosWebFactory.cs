using Microsoft.AspNetCore.Mvc.Testing;
using Sonos.Web.Hubs;

namespace Sonos.Web.Specs;

internal class SonosWebFactory : WebApplicationFactory<IPlayerStatusClient>
{
}
