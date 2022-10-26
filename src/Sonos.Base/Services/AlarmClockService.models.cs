using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Services
{
    public partial class AlarmClockService
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        [System.Xml.Serialization.XmlRoot("Alarms", Namespace = "", IsNullable = false)]
        public partial class AlarmCollection
        {

            private Alarm[] alarmField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElement("Alarm")]
            public Alarm[] Alarms
            {
                get
                {
                    return this.alarmField;
                }
                set
                {
                    this.alarmField = value;
                }
            }
        }

        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public partial class Alarm
        {

            private byte idField;

            private System.DateTime startTimeField;

            private System.DateTime durationField;

            private string recurrenceField;

            private byte enabledField;

            private string roomUUIDField;

            private string programURIField;

            private string programMetaDataField;

            private string playModeField;

            private byte volumeField;

            private byte includeLinkedZonesField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public byte ID
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute(DataType = "time")]
            public System.DateTime StartTime
            {
                get
                {
                    return this.startTimeField;
                }
                set
                {
                    this.startTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute(DataType = "time")]
            public System.DateTime Duration
            {
                get
                {
                    return this.durationField;
                }
                set
                {
                    this.durationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string Recurrence
            {
                get
                {
                    return this.recurrenceField;
                }
                set
                {
                    this.recurrenceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public byte Enabled
            {
                get
                {
                    return this.enabledField;
                }
                set
                {
                    this.enabledField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string RoomUUID
            {
                get
                {
                    return this.roomUUIDField;
                }
                set
                {
                    this.roomUUIDField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string ProgramURI
            {
                get
                {
                    return this.programURIField;
                }
                set
                {
                    this.programURIField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string ProgramMetaData
            {
                get
                {
                    return this.programMetaDataField;
                }
                set
                {
                    this.programMetaDataField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string PlayMode
            {
                get
                {
                    return this.playModeField;
                }
                set
                {
                    this.playModeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public byte Volume
            {
                get
                {
                    return this.volumeField;
                }
                set
                {
                    this.volumeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public byte IncludeLinkedZones
            {
                get
                {
                    return this.includeLinkedZonesField;
                }
                set
                {
                    this.includeLinkedZonesField = value;
                }
            }
        }


    }
}
