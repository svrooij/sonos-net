using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Music;
public class SonosMusicServiceClientOptions
{
    public string Name { get; init; }
    public ushort Id { get; init; }
    public Services.MusicServicesService.MusicServiceAuthentication Authentication { get; init; }
    public Uri BaseUri { get; init; }
    public string? HouseholdId { get; set; }
    public string? Timezone { get; set; }
    public string? Key { get; set; }
    public string? AuthToken { get; set; }
    public Func<ushort, string, string, Task>? SaveNewToken { get; set; }
}


