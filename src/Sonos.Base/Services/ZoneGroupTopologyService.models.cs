namespace Sonos.Base.Services;

public partial class ZoneGroupTopologyService
{
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class ZoneGroupState
    {
        private ZoneGroupStateZoneGroup[] zoneGroupsField;

        private object vanishedDevicesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("ZoneGroup", IsNullable = false)]
        public ZoneGroupStateZoneGroup[] ZoneGroups
        {
            get
            {
                return this.zoneGroupsField;
            }
            set
            {
                this.zoneGroupsField = value;
            }
        }

        /// <remarks/>
        public object VanishedDevices
        {
            get
            {
                return this.vanishedDevicesField;
            }
            set
            {
                this.vanishedDevicesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ZoneGroupStateZoneGroup
    {
        private ZoneGroupStateZoneGroupZoneGroupMember[] zoneGroupMemberField;

        private string coordinatorField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ZoneGroupMember")]
        public ZoneGroupStateZoneGroupZoneGroupMember[] ZoneGroupMember
        {
            get
            {
                return this.zoneGroupMemberField;
            }
            set
            {
                this.zoneGroupMemberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Coordinator
        {
            get
            {
                return this.coordinatorField;
            }
            set
            {
                this.coordinatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string ID
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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ZoneGroupStateZoneGroupZoneGroupMember
    {
        private string uUIDField;

        private string locationField;

        private string zoneNameField;

        private string iconField;

        private byte configurationField;

        private string softwareVersionField;

        private byte sWGenField;

        private string minCompatibleVersionField;

        private string legacyCompatibleVersionField;

        private byte bootSeqField;

        private byte tVConfigurationErrorField;

        private byte hdmiCecAvailableField;

        private byte wirelessModeField;

        private byte wirelessLeafOnlyField;

        private ushort channelFreqField;

        private byte behindWifiExtenderField;

        private byte wifiEnabledField;

        private byte ethLinkField;

        private byte orientationField;

        private byte roomCalibrationStateField;

        private byte secureRegStateField;

        private byte voiceConfigStateField;

        private byte micEnabledField;

        private byte airPlayEnabledField;

        private byte idleStateField;

        private string moreInfoField;

        private ushort sSLPortField;

        private ushort hHSSLPortField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string UUID
        {
            get
            {
                return this.uUIDField;
            }
            set
            {
                this.uUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string ZoneName
        {
            get
            {
                return this.zoneNameField;
            }
            set
            {
                this.zoneNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Icon
        {
            get
            {
                return this.iconField;
            }
            set
            {
                this.iconField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte Configuration
        {
            get
            {
                return this.configurationField;
            }
            set
            {
                this.configurationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string SoftwareVersion
        {
            get
            {
                return this.softwareVersionField;
            }
            set
            {
                this.softwareVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte SWGen
        {
            get
            {
                return this.sWGenField;
            }
            set
            {
                this.sWGenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string MinCompatibleVersion
        {
            get
            {
                return this.minCompatibleVersionField;
            }
            set
            {
                this.minCompatibleVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string LegacyCompatibleVersion
        {
            get
            {
                return this.legacyCompatibleVersionField;
            }
            set
            {
                this.legacyCompatibleVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte BootSeq
        {
            get
            {
                return this.bootSeqField;
            }
            set
            {
                this.bootSeqField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte TVConfigurationError
        {
            get
            {
                return this.tVConfigurationErrorField;
            }
            set
            {
                this.tVConfigurationErrorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte HdmiCecAvailable
        {
            get
            {
                return this.hdmiCecAvailableField;
            }
            set
            {
                this.hdmiCecAvailableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte WirelessMode
        {
            get
            {
                return this.wirelessModeField;
            }
            set
            {
                this.wirelessModeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte WirelessLeafOnly
        {
            get
            {
                return this.wirelessLeafOnlyField;
            }
            set
            {
                this.wirelessLeafOnlyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public ushort ChannelFreq
        {
            get
            {
                return this.channelFreqField;
            }
            set
            {
                this.channelFreqField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte BehindWifiExtender
        {
            get
            {
                return this.behindWifiExtenderField;
            }
            set
            {
                this.behindWifiExtenderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte WifiEnabled
        {
            get
            {
                return this.wifiEnabledField;
            }
            set
            {
                this.wifiEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte EthLink
        {
            get
            {
                return this.ethLinkField;
            }
            set
            {
                this.ethLinkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte Orientation
        {
            get
            {
                return this.orientationField;
            }
            set
            {
                this.orientationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte RoomCalibrationState
        {
            get
            {
                return this.roomCalibrationStateField;
            }
            set
            {
                this.roomCalibrationStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte SecureRegState
        {
            get
            {
                return this.secureRegStateField;
            }
            set
            {
                this.secureRegStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte VoiceConfigState
        {
            get
            {
                return this.voiceConfigStateField;
            }
            set
            {
                this.voiceConfigStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte MicEnabled
        {
            get
            {
                return this.micEnabledField;
            }
            set
            {
                this.micEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte AirPlayEnabled
        {
            get
            {
                return this.airPlayEnabledField;
            }
            set
            {
                this.airPlayEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte IdleState
        {
            get
            {
                return this.idleStateField;
            }
            set
            {
                this.idleStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string MoreInfo
        {
            get
            {
                return this.moreInfoField;
            }
            set
            {
                this.moreInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public ushort SSLPort
        {
            get
            {
                return this.sSLPortField;
            }
            set
            {
                this.sSLPortField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public ushort HHSSLPort
        {
            get
            {
                return this.hHSSLPortField;
            }
            set
            {
                this.hHSSLPortField = value;
            }
        }
    }
}