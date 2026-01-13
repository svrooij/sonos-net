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

        [System.Xml.Serialization.XmlIgnore]

        public ZoneGroupStateZoneGroupZoneGroupMember CoordinatorMember => ZoneGroupMember.Single(m => m.UUID == Coordinator);
        public ZoneGroupStateZoneGroupZoneGroupMember[] Members => ZoneGroupMember.Where(m => m.UUID != Coordinator).ToArray();

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

        [System.Xml.Serialization.XmlIgnore]
        public string GroupName
        {
            get
            {
                if (ZoneGroupMember.Length == 1)
                {
                    return CoordinatorMember.ZoneName;
                }
                if (ZoneGroupMember.Length == 2)
                {
                    return $"{CoordinatorMember.ZoneName} + {ZoneGroupMember.Single(m => m.UUID != Coordinator).ZoneName}";
                }
                return $"{CoordinatorMember.ZoneName} + {ZoneGroupMember.Length - 1} speakers";
            }
        }

        public override string ToString()
        {
            return $"ZoneGroup {GroupName} ({Coordinator})";
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

        private int configurationField;

        private string softwareVersionField;

        private int sWGenField;

        private string minCompatibleVersionField;

        private string legacyCompatibleVersionField;

        private int bootSeqField;

        private int tVConfigurationErrorField;

        private int hdmiCecAvailableField;

        private int wirelessModeField;

        private int wirelessLeafOnlyField;

        private ushort channelFreqField;

        private int behindWifiExtenderField;

        private int wifiEnabledField;

        private int ethLinkField;

        private int orientationField;

        private int roomCalibrationStateField;

        private int secureRegStateField;

        private int voiceConfigStateField;

        private int micEnabledField;

        private int airPlayEnabledField;

        private int idleStateField;

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

        public Uri BaseUri
        {
            get
            {
                var uri = new Uri(Location);
                return new Uri($"{uri.Scheme}://{uri.Authority}/");
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
        public int Configuration
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
        public int SWGen
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
        public int BootSeq
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
        public int TVConfigurationError
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
        public int HdmiCecAvailable
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
        public int WirelessMode
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
        public int WirelessLeafOnly
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
        public int BehindWifiExtender
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
        public int WifiEnabled
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
        public int EthLink
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
        public int Orientation
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
        public int RoomCalibrationState
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
        public int SecureRegState
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
        public int VoiceConfigState
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
        public int MicEnabled
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
        public int AirPlayEnabled
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
        public int IdleState
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

        public override string ToString()
        {
            return $"ZoneGroupMember {ZoneName} ({UUID})";
        }
    }

    /// <summary>
    /// Represents the xml document containing a collection of third-party media servers.
    /// </summary>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot("MediaServers", Namespace = "", IsNullable = false)]
    public partial class ThirdPartyMediaServersResponse
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Service")]
        public ThirdPartyMediaServer[] Servers { get; set; }
    }

    /// <summary>
    /// Represents a third-party media server and its associated account information.
    /// </summary>
    /// <remarks>This class is typically used for serialization and deserialization of media server data, including
    /// account credentials and server metadata. Property values correspond to XML attributes and may be required for
    /// integration with external systems or services.</remarks>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ThirdPartyMediaServer
    {
        /// <summary>
        /// Gets the unique music service device name (UDN)
        /// </summary>
        /// <remarks>Looks like `SA_RINCON2311_X_#Svc2311-0-Token` for spotify</remarks>
        [System.Xml.Serialization.XmlAttribute()]
        public string UDN { get; set; }

        /// <summary>
        /// Number of accounts for this media service.
        /// </summary>
        [System.Xml.Serialization.XmlAttribute()]
        public int NumAccounts { get; set; }

        /// <summary>
        /// Md0 value, unknown purpose.
        /// </summary>
        [System.Xml.Serialization.XmlAttribute("Md0")]
        public string Md { get; set; }

        /// <summary>
        /// Internal username for the media service account.
        /// </summary>
        [System.Xml.Serialization.XmlAttribute("Username0")]
        public string Username { get; set; }

        /// <summary>
        /// Nickname associated with the media service account, not provided by all services.
        /// </summary>
        [System.Xml.Serialization.XmlAttribute("Nickname0")]
        public string Nickname { get; set; }

        /// <summary>
        /// Value of SerialNum0, purpose unknown.
        /// </summary>
        [System.Xml.Serialization.XmlAttribute("SerialNum0")]
        public int SerialNum { get; set; }

        /// <summary>
        /// Value of Flags0, purpose unknown.
        /// </summary>
        [System.Xml.Serialization.XmlAttribute("Flags0")]
        public int Flags { get; set; }

        /// <summary>
        /// Tier of the media service, 1 = free, 3 = premium
        /// </summary>
        [System.Xml.Serialization.XmlAttribute("Tier0")]
        public int Tier { get; set; }

        /// <summary>
        /// Access token for the media service account.
        /// </summary>
        [System.Xml.Serialization.XmlAttribute("Token0")]
        public string Token { get; set; }

        /// <summary>
        /// Refresh key for the media service account.
        /// </summary>
        [System.Xml.Serialization.XmlAttribute("Key0")]
        public string Key { get; set; }
    }

    public partial class ThirdPartyMediaServer
    {
        private int? serviceId;
        /// <summary>
        /// Gets the unique identifier for the service associated with this instance.
        /// </summary>
        /// <remarks>The service identifier is derived from the UDN (Unique Device Name) property.</remarks>
        /// <exception cref="FormatException">Thrown when the UDN is not in the expected format.</exception>"
        public int ServiceId
        {
            get
            {
                if (!serviceId.HasValue)
                {
                    // UDN looks like: SA_RINCON2311_X_#Svc2311-0-Token, extract the number after RINCON using a generated regular expression
                    if (string.IsNullOrEmpty(UDN) || !RegularExpressions.UdnRegex().IsMatch(UDN))
                    {
                        throw new FormatException($"UDN '{UDN}' is not in the expected format.");
                    }

                    var result = RegularExpressions.UdnRegex().Match(UDN);
                    // Don't ask me how I found out about this encoding, but Sonos encodes the service id like this: ServiceId = (encodedServiceNumber - 7) / 256
                    var encodedServiceNumber = int.Parse(result.Groups["ServiceId"].Value);
                    serviceId = (encodedServiceNumber - 7)/256;
                }
                return serviceId.Value;
            }
        }
        public override string ToString()
        {
            return $"ThirdPartyMediaServer UDN={UDN}, Username={Username}, Nickname={Nickname}, Tier={Tier}";
        }
    }



}

public static partial class RegularExpressions
{
    [System.Text.RegularExpressions.GeneratedRegex(@"^SA_RINCON(?<ServiceId>\d+)_X_#.*$")]
    internal static partial System.Text.RegularExpressions.Regex UdnRegex();
}