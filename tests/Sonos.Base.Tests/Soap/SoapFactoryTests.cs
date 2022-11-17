using System.IO;
using System.Text;
using Xunit;


namespace Sonos.Base.Soap.Tests;

public class SoapFactoryTests {
    const string xmlInputNextRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<s:Envelope s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
  <s:Body>
    <u:Next xmlns:u=""urn:schemas-upnp-org:service:AVTransport:1"">
      <InstanceID>0</InstanceID>
    </u:Next>
  </s:Body>
</s:Envelope>";

    [Fact]
    public void GeneratesXmlStream_AvTransportNextRequest_GeneratesExpectedXml() {
        using var stream = SoapFactory.GenerateXmlStream("AVTransport", "Next", new Services.AVTransportService.NextRequest());
        using var reader = new StreamReader(stream);
        var xml = reader.ReadToEnd();

        Assert.Equal(xmlInputNextRequest, xml);
    }

    [Fact]
    public void GeneratesXml_AvTransportNextRequest_GeneratesExpectedXml()
    {
        var xml = SoapFactory.GenerateXml("AVTransport", "Next", new Services.AVTransportService.NextRequest());
        Assert.Equal(xmlInputNextRequest, xml);
    }

    const string xmlGetZoneInfoResponse = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetZoneInfoResponse xmlns:u=""urn:schemas-upnp-org:service:DeviceProperties:1""><SerialNumber>AA-2A-1B-AA-AA-AA:C</SerialNumber><SoftwareVersion>70.1-34112</SoftwareVersion><DisplaySoftwareVersion>14.18</DisplaySoftwareVersion><HardwareVersion>1.26.1.7-2.1</HardwareVersion><IPAddress>192.168.10.20</IPAddress><MACAddress>AA:2A:1B:AA:AA:AA</MACAddress><CopyrightInfo>� 2003-2021, Sonos, Inc. All rights reserved.</CopyrightInfo><ExtraInfo></ExtraInfo><HTAudioIn>0</HTAudioIn><Flags>0</Flags></u:GetZoneInfoResponse></s:Body></s:Envelope>";


    [Fact]
    public void ParseXml_GetZoneInfoXmlStream_CreatesCorrectObject()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlGetZoneInfoResponse));
        var parsedObject = SoapFactory.ParseXml<Sonos.Base.Services.DevicePropertiesService.GetZoneInfoResponse>("DeviceProperties", stream);

        Assert.NotNull(parsedObject);
        Assert.Equal("AA:2A:1B:AA:AA:AA", parsedObject.MACAddress);

    }

    [Fact]
    public void ParseXml_GetZoneInfoXml_CreatesCorrectObject()
    {
        var parsedObject = SoapFactory.ParseXml<Sonos.Base.Services.DevicePropertiesService.GetZoneInfoResponse>("DeviceProperties", xmlGetZoneInfoResponse);

        Assert.NotNull(parsedObject);
        Assert.Equal("AA:2A:1B:AA:AA:AA", parsedObject.MACAddress);

    }
}