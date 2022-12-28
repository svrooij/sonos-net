using Sonos.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Events.Http.Models
{
    public partial class ZoneGroupTopologyEvent
    {
        public ZoneGroupTopologyService.ZoneGroupState? ParsedState => ZoneGroupTopologyService.ParseState(ZoneGroupState);
    }
}
