namespace Sonos.Base.Tests;
internal class FakeData
{
    public const string S1DeviceDescription = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<root xmlns=""urn:schemas-upnp-org:device-1-0"">
  <specVersion>
    <major>1</major>
    <minor>0</minor>
  </specVersion>
  <device>
    <deviceType>urn:schemas-upnp-org:device:ZonePlayer:1</deviceType>
    <friendlyName>192.168.x.x - Sonos Play:5</friendlyName>
    <manufacturer>Sonos, Inc.</manufacturer>
    <manufacturerURL>http://www.sonos.com</manufacturerURL>
    <modelNumber>S5</modelNumber>
    <modelDescription>Sonos Play:5</modelDescription>
    <modelName>Sonos Play:5</modelName>
    <modelURL>http://www.sonos.com/products/zoneplayers/S5</modelURL>
    <softwareVersion>57.19-46310</softwareVersion>
    <swGen>1</swGen>
    <hardwareVersion>1.16.4.1-2.0</hardwareVersion>
    <serialNum>00-0E-58-00-00-00:A</serialNum>
    <MACAddress>00:0E:58:00:00:00</MACAddress>
    <UDN>uuid:RINCON_000E5800000001400</UDN>
    <iconList>
      <icon>
        <id>0</id>
        <mimetype>image/png</mimetype>
        <width>48</width>
        <height>48</height>
        <depth>24</depth>
        <url>/img/icon-S5.png</url>
      </icon>
    </iconList>
    <minCompatibleVersion>56.0-00000</minCompatibleVersion>
    <legacyCompatibleVersion>36.0-00000</legacyCompatibleVersion>
    <apiVersion>1.18.10</apiVersion>
    <minApiVersion>1.1.0</minApiVersion>
    <displayVersion>11.12</displayVersion>
    <extraVersion>OTP: 1.1.1(1-16-4-zp5s-0.5)</extraVersion>
    <roomName>Werkkamer Stephan</roomName>
    <displayName>Play:5</displayName>
    <zoneType>5</zoneType>
    <feature1>0x02000002</feature1>
    <feature2>0x00006172</feature2>
    <feature3>0x0003102a</feature3>
    <seriesid>P100</seriesid>
    <variant>0</variant>
    <internalSpeakerSize>3</internalSpeakerSize>
    <bassExtension>0.000</bassExtension>
    <satGainOffset>0.000</satGainOffset>
    <memory>32</memory>
    <flash>32</flash>
    #DEACTIVATION_STATE_TAG_AND_VALUE#
    #DEACTIVATION_TTL_TAG_AND_VALUE#
    #DEACTIVATION_DATE_TIME_TAG_AND_VALUE#
    <flashRepartitioned>1</flashRepartitioned>
    <ampOnTime>425</ampOnTime>
    <retailMode>0</retailMode>
    <serviceList>
      <service>
        <serviceType>urn:schemas-upnp-org:service:AlarmClock:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:AlarmClock</serviceId>
        <controlURL>/AlarmClock/Control</controlURL>
        <eventSubURL>/AlarmClock/Event</eventSubURL>
        <SCPDURL>/xml/AlarmClock1.xml</SCPDURL>
      </service>    
      <service>
        <serviceType>urn:schemas-upnp-org:service:MusicServices:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:MusicServices</serviceId>
        <controlURL>/MusicServices/Control</controlURL>
        <eventSubURL>/MusicServices/Event</eventSubURL>
        <SCPDURL>/xml/MusicServices1.xml</SCPDURL>
      </service>    
      <service>
        <serviceType>urn:schemas-upnp-org:service:AudioIn:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:AudioIn</serviceId>
        <controlURL>/AudioIn/Control</controlURL>
        <eventSubURL>/AudioIn/Event</eventSubURL>
        <SCPDURL>/xml/AudioIn1.xml</SCPDURL>
      </service>
      <service>
        <serviceType>urn:schemas-upnp-org:service:DeviceProperties:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:DeviceProperties</serviceId>
        <controlURL>/DeviceProperties/Control</controlURL>
        <eventSubURL>/DeviceProperties/Event</eventSubURL>
        <SCPDURL>/xml/DeviceProperties1.xml</SCPDURL>
      </service>    
      <service>
        <serviceType>urn:schemas-upnp-org:service:SystemProperties:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:SystemProperties</serviceId>
        <controlURL>/SystemProperties/Control</controlURL>
        <eventSubURL>/SystemProperties/Event</eventSubURL>
        <SCPDURL>/xml/SystemProperties1.xml</SCPDURL>
      </service>    
      <service>
        <serviceType>urn:schemas-upnp-org:service:ZoneGroupTopology:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:ZoneGroupTopology</serviceId>
        <controlURL>/ZoneGroupTopology/Control</controlURL>
        <eventSubURL>/ZoneGroupTopology/Event</eventSubURL>
        <SCPDURL>/xml/ZoneGroupTopology1.xml</SCPDURL>
      </service>    
      <service>
        <serviceType>urn:schemas-upnp-org:service:GroupManagement:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:GroupManagement</serviceId>
        <controlURL>/GroupManagement/Control</controlURL>
        <eventSubURL>/GroupManagement/Event</eventSubURL>
        <SCPDURL>/xml/GroupManagement1.xml</SCPDURL>
      </service>
      <service>
        <serviceType>urn:schemas-tencent-com:service:QPlay:1</serviceType>
        <serviceId>urn:tencent-com:serviceId:QPlay</serviceId>
        <controlURL>/QPlay/Control</controlURL>
        <eventSubURL>/QPlay/Event</eventSubURL>
        <SCPDURL>/xml/QPlay1.xml</SCPDURL>
      </service>
    </serviceList>
    <deviceList>
      <device>
  <deviceType>urn:schemas-upnp-org:device:MediaServer:1</deviceType>
  <friendlyName>192.168.x.x - Sonos Play:5 Media Server</friendlyName>
  <manufacturer>Sonos, Inc.</manufacturer>
  <manufacturerURL>http://www.sonos.com</manufacturerURL>
  <modelNumber>S5</modelNumber>
  <modelDescription>Sonos Play:5 Media Server</modelDescription>
  <modelName>Sonos Play:5</modelName>
  <modelURL>http://www.sonos.com/products/zoneplayers/S5</modelURL>
  <UDN>uuid:RINCON_000E5800000001400_MS</UDN>
  <serviceList>
    <service>
      <serviceType>urn:schemas-upnp-org:service:ContentDirectory:1</serviceType>
      <serviceId>urn:upnp-org:serviceId:ContentDirectory</serviceId>
      <controlURL>/MediaServer/ContentDirectory/Control</controlURL>
      <eventSubURL>/MediaServer/ContentDirectory/Event</eventSubURL>
      <SCPDURL>/xml/ContentDirectory1.xml</SCPDURL>
    </service>
    <service>
      <serviceType>urn:schemas-upnp-org:service:ConnectionManager:1</serviceType>
	    <serviceId>urn:upnp-org:serviceId:ConnectionManager</serviceId>
	    <controlURL>/MediaServer/ConnectionManager/Control</controlURL>
	    <eventSubURL>/MediaServer/ConnectionManager/Event</eventSubURL>
	    <SCPDURL>/xml/ConnectionManager1.xml</SCPDURL>
	  </service>
	</serviceList>
      </device>
      <device>
	<deviceType>urn:schemas-upnp-org:device:MediaRenderer:1</deviceType>
  <friendlyName>Werkkamer Stephan - Sonos Play:5 Media Renderer</friendlyName>
  <manufacturer>Sonos, Inc.</manufacturer>
  <manufacturerURL>http://www.sonos.com</manufacturerURL>
  <modelNumber>S5</modelNumber>
  <modelDescription>Sonos Play:5 Media Renderer</modelDescription>
  <modelName>Sonos Play:5</modelName>
  <modelURL>http://www.sonos.com/products/zoneplayers/S5</modelURL>
	<UDN>uuid:RINCON_000E5800000001400_MR</UDN>
	<serviceList>
	  <service>
	    <serviceType>urn:schemas-upnp-org:service:RenderingControl:1</serviceType>
	    <serviceId>urn:upnp-org:serviceId:RenderingControl</serviceId>
	    <controlURL>/MediaRenderer/RenderingControl/Control</controlURL>
	    <eventSubURL>/MediaRenderer/RenderingControl/Event</eventSubURL>
	    <SCPDURL>/xml/RenderingControl1.xml</SCPDURL>
	  </service>
	  <service>
	    <serviceType>urn:schemas-upnp-org:service:ConnectionManager:1</serviceType>
	    <serviceId>urn:upnp-org:serviceId:ConnectionManager</serviceId>
	    <controlURL>/MediaRenderer/ConnectionManager/Control</controlURL>
	    <eventSubURL>/MediaRenderer/ConnectionManager/Event</eventSubURL>
	    <SCPDURL>/xml/ConnectionManager1.xml</SCPDURL>
	  </service>
	  <service>
	    <serviceType>urn:schemas-upnp-org:service:AVTransport:1</serviceType>
	    <serviceId>urn:upnp-org:serviceId:AVTransport</serviceId>
	    <controlURL>/MediaRenderer/AVTransport/Control</controlURL>
	    <eventSubURL>/MediaRenderer/AVTransport/Event</eventSubURL>
	    <SCPDURL>/xml/AVTransport1.xml</SCPDURL>
	  </service>
	  <service>
	    <serviceType>urn:schemas-sonos-com:service:Queue:1</serviceType>
	    <serviceId>urn:sonos-com:serviceId:Queue</serviceId>
	    <controlURL>/MediaRenderer/Queue/Control</controlURL>
	    <eventSubURL>/MediaRenderer/Queue/Event</eventSubURL>
	    <SCPDURL>/xml/Queue1.xml</SCPDURL>
	  </service>
      <service>
        <serviceType>urn:schemas-upnp-org:service:GroupRenderingControl:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:GroupRenderingControl</serviceId>
        <controlURL>/MediaRenderer/GroupRenderingControl/Control</controlURL>
        <eventSubURL>/MediaRenderer/GroupRenderingControl/Event</eventSubURL>
        <SCPDURL>/xml/GroupRenderingControl1.xml</SCPDURL>
      </service>
      <service>
        <serviceType>urn:schemas-upnp-org:service:VirtualLineIn:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:VirtualLineIn</serviceId>
        <controlURL>/MediaRenderer/VirtualLineIn/Control</controlURL>
        <eventSubURL>/MediaRenderer/VirtualLineIn/Event</eventSubURL>
        <SCPDURL>/xml/VirtualLineIn1.xml</SCPDURL>
    </service>
	</serviceList>
        <X_Rhapsody-Extension xmlns=""http://www.real.com/rhapsody/xmlns/upnp-1-0"">
          <deviceID>urn:rhapsody-real-com:device-id-1-0:sonos_1:RINCON_000E5800000001400</deviceID>
            <deviceCapabilities>
              <interactionPattern type=""real-rhapsody-upnp-1-0""/>
            </deviceCapabilities>
        </X_Rhapsody-Extension>
        <qq:X_QPlay_SoftwareCapability xmlns:qq=""http://www.tencent.com"">QPlay:2</qq:X_QPlay_SoftwareCapability>
        <iconList>
          <icon>
            <mimetype>image/png</mimetype>
            <width>48</width>
            <height>48</height>
            <depth>24</depth>
            <url>/img/icon-S5.png</url>
          </icon>
        </iconList>
      </device>
    </deviceList>
  </device>
</root>
";

    public const string S2DeviceDescription = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<root xmlns=""urn:schemas-upnp-org:device-1-0"">
  <specVersion>
    <major>1</major>
    <minor>0</minor>
  </specVersion>
  <device>
    <deviceType>urn:schemas-upnp-org:device:ZonePlayer:1</deviceType>
    <friendlyName>192.168.1.10 - Sonos One</friendlyName>
    <manufacturer>Sonos, Inc.</manufacturer>
    <manufacturerURL>http://www.sonos.com</manufacturerURL>
    <modelNumber>S18</modelNumber>
    <modelDescription>Sonos One</modelDescription>
    <modelName>Sonos One</modelName>
    <modelURL>http://www.sonos.com/products/zoneplayers/S18</modelURL>
    <softwareVersion>76.2-47142</softwareVersion>
    <swGen>2</swGen>
    <hardwareVersion>1.26.1.7-2.1</hardwareVersion>
    <serialNum>54-2A-1B-AA-AA-AA:C</serialNum>
    <MACAddress>54:2A:1B:AA:AA:AA</MACAddress>
    <UDN>uuid:RINCON_000E58000BB001400</UDN>
    <iconList>
      <icon>
        <id>0</id>
        <mimetype>image/png</mimetype>
        <width>48</width>
        <height>48</height>
        <depth>24</depth>
        <url>/img/icon-S18.png</url>
      </icon>
    </iconList>
    <minCompatibleVersion>75.0-00000</minCompatibleVersion>
    <legacyCompatibleVersion>58.0-00000</legacyCompatibleVersion>
    <apiVersion>1.37.0</apiVersion>
    <minApiVersion>1.1.0</minApiVersion>
    <displayVersion>15.10</displayVersion>
    <extraVersion></extraVersion>
    <nsVersion>35</nsVersion>
    <roomName>Badkamer</roomName>
    <displayName>One</displayName>
    <zoneType>20</zoneType>
    <feature1>0x00000000</feature1>
    <feature2>0x05c18332</feature2>
    <feature3>0x0541580e</feature3>
    <seriesid>A201</seriesid>
    <variant>1</variant>
    <internalSpeakerSize>5</internalSpeakerSize>
    <memory>1024</memory>
    <flash>1024</flash>
    <ampOnTime>20</ampOnTime>
    <retailMode>0</retailMode>
    <SSLPort>1443</SSLPort>
    <securehhSSLPort>1843</securehhSSLPort>
    <serviceList>
      <service>
        <serviceType>urn:schemas-upnp-org:service:AlarmClock:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:AlarmClock</serviceId>
        <controlURL>/AlarmClock/Control</controlURL>
        <eventSubURL>/AlarmClock/Event</eventSubURL>
        <SCPDURL>/xml/AlarmClock1.xml</SCPDURL>
      </service>
      <service>
        <serviceType>urn:schemas-upnp-org:service:MusicServices:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:MusicServices</serviceId>
        <controlURL>/MusicServices/Control</controlURL>
        <eventSubURL>/MusicServices/Event</eventSubURL>
        <SCPDURL>/xml/MusicServices1.xml</SCPDURL>
      </service>
      <service>
        <serviceType>urn:schemas-upnp-org:service:DeviceProperties:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:DeviceProperties</serviceId>
        <controlURL>/DeviceProperties/Control</controlURL>
        <eventSubURL>/DeviceProperties/Event</eventSubURL>
        <SCPDURL>/xml/DeviceProperties1.xml</SCPDURL>
      </service>
      <service>
        <serviceType>urn:schemas-upnp-org:service:SystemProperties:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:SystemProperties</serviceId>
        <controlURL>/SystemProperties/Control</controlURL>
        <eventSubURL>/SystemProperties/Event</eventSubURL>
        <SCPDURL>/xml/SystemProperties1.xml</SCPDURL>
      </service>
      <service>
        <serviceType>urn:schemas-upnp-org:service:ZoneGroupTopology:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:ZoneGroupTopology</serviceId>
        <controlURL>/ZoneGroupTopology/Control</controlURL>
        <eventSubURL>/ZoneGroupTopology/Event</eventSubURL>
        <SCPDURL>/xml/ZoneGroupTopology1.xml</SCPDURL>
      </service>
      <service>
        <serviceType>urn:schemas-upnp-org:service:GroupManagement:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:GroupManagement</serviceId>
        <controlURL>/GroupManagement/Control</controlURL>
        <eventSubURL>/GroupManagement/Event</eventSubURL>
        <SCPDURL>/xml/GroupManagement1.xml</SCPDURL>
      </service>
      <service>
        <serviceType>urn:schemas-tencent-com:service:QPlay:1</serviceType>
        <serviceId>urn:tencent-com:serviceId:QPlay</serviceId>
        <controlURL>/QPlay/Control</controlURL>
        <eventSubURL>/QPlay/Event</eventSubURL>
        <SCPDURL>/xml/QPlay1.xml</SCPDURL>
      </service>
    </serviceList>
    <deviceList>
      <device>
  <deviceType>urn:schemas-upnp-org:device:MediaServer:1</deviceType>
  <friendlyName>192.168.1.10 - Sonos One Media Server</friendlyName>
  <manufacturer>Sonos, Inc.</manufacturer>
  <manufacturerURL>http://www.sonos.com</manufacturerURL>
  <modelNumber>S18</modelNumber>
  <modelDescription>Sonos One Media Server</modelDescription>
  <modelName>Sonos One</modelName>
  <modelURL>http://www.sonos.com/products/zoneplayers/S18</modelURL>
  <UDN>uuid:RINCON_000E58000BB001400_MS</UDN>
  <serviceList>
    <service>
      <serviceType>urn:schemas-upnp-org:service:ContentDirectory:1</serviceType>
      <serviceId>urn:upnp-org:serviceId:ContentDirectory</serviceId>
      <controlURL>/MediaServer/ContentDirectory/Control</controlURL>
      <eventSubURL>/MediaServer/ContentDirectory/Event</eventSubURL>
      <SCPDURL>/xml/ContentDirectory1.xml</SCPDURL>
    </service>
    <service>
      <serviceType>urn:schemas-upnp-org:service:ConnectionManager:1</serviceType>
	    <serviceId>urn:upnp-org:serviceId:ConnectionManager</serviceId>
	    <controlURL>/MediaServer/ConnectionManager/Control</controlURL>
	    <eventSubURL>/MediaServer/ConnectionManager/Event</eventSubURL>
	    <SCPDURL>/xml/ConnectionManager1.xml</SCPDURL>
	  </service>
	</serviceList>
      </device>
      <device>
	<deviceType>urn:schemas-upnp-org:device:MediaRenderer:1</deviceType>
  <friendlyName>Badkamer - Sonos One Media Renderer</friendlyName>
  <manufacturer>Sonos, Inc.</manufacturer>
  <manufacturerURL>http://www.sonos.com</manufacturerURL>
  <modelNumber>S18</modelNumber>
  <modelDescription>Sonos One Media Renderer</modelDescription>
  <modelName>Sonos One</modelName>
  <modelURL>http://www.sonos.com/products/zoneplayers/S18</modelURL>
	<UDN>uuid:RINCON_000E58000BB001400_MR</UDN>
	<serviceList>
	  <service>
	    <serviceType>urn:schemas-upnp-org:service:RenderingControl:1</serviceType>
	    <serviceId>urn:upnp-org:serviceId:RenderingControl</serviceId>
	    <controlURL>/MediaRenderer/RenderingControl/Control</controlURL>
	    <eventSubURL>/MediaRenderer/RenderingControl/Event</eventSubURL>
	    <SCPDURL>/xml/RenderingControl1.xml</SCPDURL>
	  </service>
	  <service>
	    <serviceType>urn:schemas-upnp-org:service:ConnectionManager:1</serviceType>
	    <serviceId>urn:upnp-org:serviceId:ConnectionManager</serviceId>
	    <controlURL>/MediaRenderer/ConnectionManager/Control</controlURL>
	    <eventSubURL>/MediaRenderer/ConnectionManager/Event</eventSubURL>
	    <SCPDURL>/xml/ConnectionManager1.xml</SCPDURL>
	  </service>
	  <service>
	    <serviceType>urn:schemas-upnp-org:service:AVTransport:1</serviceType>
	    <serviceId>urn:upnp-org:serviceId:AVTransport</serviceId>
	    <controlURL>/MediaRenderer/AVTransport/Control</controlURL>
	    <eventSubURL>/MediaRenderer/AVTransport/Event</eventSubURL>
	    <SCPDURL>/xml/AVTransport1.xml</SCPDURL>
	  </service>
	  <service>
	    <serviceType>urn:schemas-sonos-com:service:Queue:1</serviceType>
	    <serviceId>urn:sonos-com:serviceId:Queue</serviceId>
	    <controlURL>/MediaRenderer/Queue/Control</controlURL>
	    <eventSubURL>/MediaRenderer/Queue/Event</eventSubURL>
	    <SCPDURL>/xml/Queue1.xml</SCPDURL>
	  </service>
      <service>
        <serviceType>urn:schemas-upnp-org:service:GroupRenderingControl:1</serviceType>
        <serviceId>urn:upnp-org:serviceId:GroupRenderingControl</serviceId>
        <controlURL>/MediaRenderer/GroupRenderingControl/Control</controlURL>
        <eventSubURL>/MediaRenderer/GroupRenderingControl/Event</eventSubURL>
        <SCPDURL>/xml/GroupRenderingControl1.xml</SCPDURL>
      </service>
      <service>
          <serviceType>urn:schemas-upnp-org:service:VirtualLineIn:1</serviceType>
          <serviceId>urn:upnp-org:serviceId:VirtualLineIn</serviceId>
          <controlURL>/MediaRenderer/VirtualLineIn/Control</controlURL>
          <eventSubURL>/MediaRenderer/VirtualLineIn/Event</eventSubURL>
          <SCPDURL>/xml/VirtualLineIn1.xml</SCPDURL>
      </service>
	</serviceList>
        <X_Rhapsody-Extension xmlns=""http://www.real.com/rhapsody/xmlns/upnp-1-0"">
          <deviceID>urn:rhapsody-real-com:device-id-1-0:sonos_1:RINCON_000E58000BB001400</deviceID>
            <deviceCapabilities>
              <interactionPattern type=""real-rhapsody-upnp-1-0""/>
            </deviceCapabilities>
        </X_Rhapsody-Extension>
        <qq:X_QPlay_SoftwareCapability xmlns:qq=""http://www.tencent.com"">QPlay:2</qq:X_QPlay_SoftwareCapability>
        <iconList>
          <icon>
            <mimetype>image/png</mimetype>
            <width>48</width>
            <height>48</height>
            <depth>24</depth>
            <url>/img/icon-S18.png</url>
          </icon>
        </iconList>
      </device>
    </deviceList>
  </device>
</root>
";

    public const string DevicePropertiesGetZoneInfo = "<SerialNumber>00-0E-58-64-AA-AA:A</SerialNumber><SoftwareVersion>57.19-46310</SoftwareVersion><DisplaySoftwareVersion>11.12</DisplaySoftwareVersion><HardwareVersion>1.16.4.1-2.0</HardwareVersion><IPAddress>192.168.1.10</IPAddress><MACAddress>00:0E:58:64:AA:AA</MACAddress><CopyrightInfo>© 2003-2023, Sonos, Inc. All rights reserved.</CopyrightInfo><ExtraInfo>OTP: 1.1.1(1-16-4-zp5s-0.5)</ExtraInfo><HTAudioIn>0</HTAudioIn><Flags>1</Flags>";

    public const string DevicePropertiesGetZoneInfoMac = "00:0E:58:64:AA:AA";
    public static string DevicePropertiesGetZoneInfoUuid() => $"RINCON_{DevicePropertiesGetZoneInfoMac.Replace(":","")}01400";
}