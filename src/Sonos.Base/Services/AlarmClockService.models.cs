/*
 * Sonos-net
 *
 * Repository https://github.com/svrooij/sonos-net
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Sonos.Base.Services;

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
        private int idField;

        private System.DateTime startTimeField;

        private System.DateTime durationField;

        private string recurrenceField;

        private int enabledField;

        private string roomUUIDField;

        private string programURIField;

        private string programMetaDataField;

        private string playModeField;

        private int volumeField;

        private int includeLinkedZonesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public int ID
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
        public int Enabled
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
        public int Volume
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
        public int IncludeLinkedZones
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