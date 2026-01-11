using Sonos.Base.Services;

namespace Sonos.Base.Events.Http.Models
{
    public partial class ZoneGroupTopologyEvent
    {
        public ZoneGroupTopologyService.ZoneGroupState? ParsedState => ZoneGroupTopologyService.ParseState(ZoneGroupState);
    }
}
