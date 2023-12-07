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

using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sonos.Base.Tests
{
    public class SonosManagerTests
    {
        private const string ZoneTopologyResponse = @"<ZoneGroupState>&lt;ZoneGroupState&gt;&lt;ZoneGroups&gt;&lt;ZoneGroup Coordinator=&quot;RINCON_A1A1A1A1A1A101400&quot; ID=&quot;RINCON_A1A1A1A1A1A101400:2788870897&quot;&gt;&lt;ZoneGroupMember UUID=&quot;RINCON_A1A1A1A1A1A101400&quot; Location=&quot;http://192.168.200.52:1400/xml/device_description.xml&quot; ZoneName=&quot;Badkamer&quot; Icon=&quot;&quot; Configuration=&quot;1&quot; SoftwareVersion=&quot;70.3-35070&quot; SWGen=&quot;2&quot; MinCompatibleVersion=&quot;69.0-00000&quot; LegacyCompatibleVersion=&quot;58.0-00000&quot; BootSeq=&quot;43&quot; TVConfigurationError=&quot;0&quot; HdmiCecAvailable=&quot;0&quot; WirelessMode=&quot;1&quot; WirelessLeafOnly=&quot;0&quot; ChannelFreq=&quot;5240&quot; BehindWifiExtender=&quot;0&quot; WifiEnabled=&quot;1&quot; EthLink=&quot;0&quot; Orientation=&quot;0&quot; RoomCalibrationState=&quot;4&quot; SecureRegState=&quot;3&quot; VoiceConfigState=&quot;2&quot; MicEnabled=&quot;0&quot; AirPlayEnabled=&quot;1&quot; IdleState=&quot;1&quot; MoreInfo=&quot;&quot; SSLPort=&quot;1443&quot; HHSSLPort=&quot;1843&quot;/&gt;&lt;/ZoneGroup&gt;&lt;ZoneGroup Coordinator=&quot;RINCON_A1A1A2A1A1A201400&quot; ID=&quot;RINCON_A1A1A2A1A1A201400:3394029939&quot;&gt;&lt;ZoneGroupMember UUID=&quot;RINCON_A1A1A2A1A1A201400&quot; Location=&quot;http://192.168.200.59:1400/xml/device_description.xml&quot; ZoneName=&quot;Slaapkamer&quot; Icon=&quot;&quot; Configuration=&quot;1&quot; SoftwareVersion=&quot;70.3-35070&quot; SWGen=&quot;2&quot; MinCompatibleVersion=&quot;69.0-00000&quot; LegacyCompatibleVersion=&quot;58.0-00000&quot; BootSeq=&quot;5&quot; TVConfigurationError=&quot;0&quot; HdmiCecAvailable=&quot;0&quot; WirelessMode=&quot;1&quot; WirelessLeafOnly=&quot;0&quot; ChannelFreq=&quot;5240&quot; BehindWifiExtender=&quot;0&quot; WifiEnabled=&quot;1&quot; EthLink=&quot;0&quot; Orientation=&quot;0&quot; RoomCalibrationState=&quot;4&quot; SecureRegState=&quot;3&quot; VoiceConfigState=&quot;0&quot; MicEnabled=&quot;0&quot; AirPlayEnabled=&quot;0&quot; IdleState=&quot;1&quot; MoreInfo=&quot;&quot; SSLPort=&quot;1443&quot; HHSSLPort=&quot;1843&quot;/&gt;&lt;/ZoneGroup&gt;&lt;ZoneGroup Coordinator=&quot;RINCON_BBA1A2A1A1A201400&quot; ID=&quot;RINCON_BBA1A2A1A1A201400:3477415731&quot;&gt;&lt;ZoneGroupMember UUID=&quot;RINCON_BBA1A2A1A1A201400&quot; Location=&quot;http://192.168.200.57:1400/xml/device_description.xml&quot; ZoneName=&quot;Werkkamer Alrieke&quot; Icon=&quot;&quot; Configuration=&quot;1&quot; SoftwareVersion=&quot;70.3-35070&quot; SWGen=&quot;2&quot; MinCompatibleVersion=&quot;69.0-00000&quot; LegacyCompatibleVersion=&quot;58.0-00000&quot; BootSeq=&quot;5&quot; TVConfigurationError=&quot;0&quot; HdmiCecAvailable=&quot;0&quot; WirelessMode=&quot;1&quot; WirelessLeafOnly=&quot;0&quot; ChannelFreq=&quot;5240&quot; BehindWifiExtender=&quot;0&quot; WifiEnabled=&quot;1&quot; EthLink=&quot;0&quot; Orientation=&quot;0&quot; RoomCalibrationState=&quot;4&quot; SecureRegState=&quot;3&quot; VoiceConfigState=&quot;0&quot; MicEnabled=&quot;0&quot; AirPlayEnabled=&quot;0&quot; IdleState=&quot;0&quot; MoreInfo=&quot;&quot; SSLPort=&quot;1443&quot; HHSSLPort=&quot;1843&quot;/&gt;&lt;/ZoneGroup&gt;&lt;ZoneGroup Coordinator=&quot;RINCON_CCA1A2A1A1A201400&quot; ID=&quot;RINCON_CCA1A2A1A1A201400:2322042732&quot;&gt;&lt;ZoneGroupMember UUID=&quot;RINCON_DDA1A2A1A1A201400&quot; Location=&quot;http://192.168.200.53:1400/xml/device_description.xml&quot; ZoneName=&quot;Woonkamer&quot; Icon=&quot;&quot; Configuration=&quot;1&quot; SoftwareVersion=&quot;70.3-35070&quot; SWGen=&quot;2&quot; MinCompatibleVersion=&quot;69.0-00000&quot; LegacyCompatibleVersion=&quot;58.0-00000&quot; BootSeq=&quot;7&quot; TVConfigurationError=&quot;0&quot; HdmiCecAvailable=&quot;0&quot; WirelessMode=&quot;1&quot; WirelessLeafOnly=&quot;0&quot; ChannelFreq=&quot;5180&quot; BehindWifiExtender=&quot;0&quot; WifiEnabled=&quot;1&quot; EthLink=&quot;0&quot; Orientation=&quot;0&quot; RoomCalibrationState=&quot;4&quot; SecureRegState=&quot;3&quot; VoiceConfigState=&quot;0&quot; MicEnabled=&quot;0&quot; AirPlayEnabled=&quot;0&quot; IdleState=&quot;1&quot; MoreInfo=&quot;&quot; SSLPort=&quot;1443&quot; HHSSLPort=&quot;1843&quot;/&gt;&lt;ZoneGroupMember UUID=&quot;RINCON_B8E9375A170401400&quot; Location=&quot;http://192.168.200.58:1400/xml/device_description.xml&quot; ZoneName=&quot;Eetkamer&quot; Icon=&quot;&quot; Configuration=&quot;1&quot; SoftwareVersion=&quot;70.3-35070&quot; SWGen=&quot;2&quot; MinCompatibleVersion=&quot;69.0-00000&quot; LegacyCompatibleVersion=&quot;58.0-00000&quot; BootSeq=&quot;8&quot; TVConfigurationError=&quot;0&quot; HdmiCecAvailable=&quot;0&quot; WirelessMode=&quot;1&quot; WirelessLeafOnly=&quot;0&quot; ChannelFreq=&quot;5240&quot; BehindWifiExtender=&quot;0&quot; WifiEnabled=&quot;1&quot; EthLink=&quot;0&quot; Orientation=&quot;0&quot; RoomCalibrationState=&quot;4&quot; SecureRegState=&quot;3&quot; VoiceConfigState=&quot;0&quot; MicEnabled=&quot;0&quot; AirPlayEnabled=&quot;0&quot; IdleState=&quot;1&quot; MoreInfo=&quot;&quot; SSLPort=&quot;1443&quot; HHSSLPort=&quot;1843&quot;/&gt;&lt;ZoneGroupMember UUID=&quot;RINCON_CCA1A2A1A1A201400&quot; Location=&quot;http://192.168.200.51:1400/xml/device_description.xml&quot; ZoneName=&quot;Keuken&quot; Icon=&quot;&quot; Configuration=&quot;1&quot; SoftwareVersion=&quot;70.3-35070&quot; SWGen=&quot;2&quot; MinCompatibleVersion=&quot;69.0-00000&quot; LegacyCompatibleVersion=&quot;58.0-00000&quot; BootSeq=&quot;48&quot; TVConfigurationError=&quot;0&quot; HdmiCecAvailable=&quot;0&quot; WirelessMode=&quot;1&quot; WirelessLeafOnly=&quot;0&quot; ChannelFreq=&quot;5240&quot; BehindWifiExtender=&quot;0&quot; WifiEnabled=&quot;1&quot; EthLink=&quot;0&quot; Orientation=&quot;0&quot; RoomCalibrationState=&quot;4&quot; SecureRegState=&quot;3&quot; VoiceConfigState=&quot;2&quot; MicEnabled=&quot;1&quot; AirPlayEnabled=&quot;1&quot; IdleState=&quot;1&quot; MoreInfo=&quot;&quot; SSLPort=&quot;1443&quot; HHSSLPort=&quot;1843&quot;/&gt;&lt;/ZoneGroup&gt;&lt;/ZoneGroups&gt;&lt;VanishedDevices&gt;&lt;/VanishedDevices&gt;&lt;/ZoneGroupState&gt;</ZoneGroupState>";

        [Fact]
        public async Task InitializeFromDevice_WorksAsExpected()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest("ZoneGroupTopology", nameof(Services.ZoneGroupTopologyService.GetZoneGroupState), responseBody: ZoneTopologyResponse);

            var sonosManager = new SonosManager(new StaticSonosServiceProvider(mockedHandler.Object));
            await sonosManager.InitializeFromDeviceAsync(TestHelpers.DefaultUri);

            var groups = sonosManager.GetGroups();

            Assert.Equal(4, groups.Count);
        }
    }
}