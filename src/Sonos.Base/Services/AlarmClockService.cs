using Sonos.Base.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sonos.Base.Services.ZoneGroupTopologyService;
using System.Xml.Serialization;

namespace Sonos.Base.Services
{
    public partial class AlarmClockService
    {
        public partial class ListAlarmsResponse
        {
            private AlarmCollection _alarms;
            [XmlIgnore]
            public Alarm[] Alarms
            {
                get
                {
                    if (_alarms == null)
                    {
                        _alarms = SoapFactory.ParseEmbeddedXml<AlarmCollection>(this.CurrentAlarmList);
                    }
                    return _alarms.Alarms;
                }
            }
        }
    }
}
