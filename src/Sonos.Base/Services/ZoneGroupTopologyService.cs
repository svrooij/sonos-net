using Sonos.Base.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sonos.Base.Services
{
    public partial class ZoneGroupTopologyService
    {
        public partial class GetZoneGroupStateResponse
        {
            private ZoneGroupState _zoneGroupState;
            [XmlIgnore]
            public ZoneGroupState ParsedState
            {
                get
                {
                    if (_zoneGroupState == null)
                    {
                        _zoneGroupState = SoapFactory.ParseEmbeddedXml<ZoneGroupState>(ZoneGroupState);
                    }
                    return _zoneGroupState;
                }
            }
        }
    }
}
