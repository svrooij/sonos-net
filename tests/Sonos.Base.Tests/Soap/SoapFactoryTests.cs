using System.IO;
using System.Text;
using Xunit;

namespace Sonos.Base.Soap.Tests;

public class SoapFactoryTests
{
    private const string xmlInputNextRequest = @"<s:Envelope s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body><u:Next xmlns:u=""urn:schemas-upnp-org:service:AVTransport:1""><InstanceID>0</InstanceID></u:Next></s:Body></s:Envelope>";

    [Fact]
    public void GeneratesXml_AvTransportNextRequest_GeneratesExpectedXml()
    {
        var xml = SoapFactory.GenerateXml(nameof(Services.SonosService.AVTransport), "Next", new Services.AVTransportService.NextRequest());
        Assert.Equal(xmlInputNextRequest, xml);
    }

    private const string xmlGetZoneInfoResponse = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetZoneInfoResponse xmlns:u=""urn:schemas-upnp-org:service:DeviceProperties:1""><SerialNumber>AA-2A-1B-AA-AA-AA:C</SerialNumber><SoftwareVersion>70.1-34112</SoftwareVersion><DisplaySoftwareVersion>14.18</DisplaySoftwareVersion><HardwareVersion>1.26.1.7-2.1</HardwareVersion><IPAddress>192.168.10.20</IPAddress><MACAddress>AA:2A:1B:AA:AA:AA</MACAddress><CopyrightInfo>� 2003-2021, Sonos, Inc. All rights reserved.</CopyrightInfo><ExtraInfo></ExtraInfo><HTAudioIn>0</HTAudioIn><Flags>0</Flags></u:GetZoneInfoResponse></s:Body></s:Envelope>";

    [Fact]
    public void ParseXml_GetZoneInfoXmlStream_CreatesCorrectObject()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlGetZoneInfoResponse));
        var parsedObject = SoapFactory.ParseXml<Sonos.Base.Services.DevicePropertiesService.GetZoneInfoResponse>(nameof(Services.SonosService.DeviceProperties), stream);

        Assert.NotNull(parsedObject);
        Assert.Equal("AA:2A:1B:AA:AA:AA", parsedObject.MACAddress);
    }

    [Fact]
    public void ParseXml_GetZoneInfoXml_CreatesCorrectObject()
    {
        var parsedObject = SoapFactory.ParseXml<Sonos.Base.Services.DevicePropertiesService.GetZoneInfoResponse>(nameof(Services.SonosService.DeviceProperties), xmlGetZoneInfoResponse);

        Assert.NotNull(parsedObject);
        Assert.Equal("AA:2A:1B:AA:AA:AA", parsedObject.MACAddress);
    }

    const string SonosUpnpErrorResponse = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><s:Fault><faultcode>s:Client</faultcode><faultstring>UPnPError</faultstring><detail><UPnPError xmlns=""urn:schemas-upnp-org:control-1-0""><errorCode>402</errorCode></UPnPError></detail></s:Fault></s:Body></s:Envelope>";
    [Fact]
    public void CanParseErrorResponse()
    {
        var parsedError = SoapFactory.ParseFaultXml(SonosUpnpErrorResponse);
        Assert.NotNull(parsedError);
    }

    [Fact]
    public void GetRequestSerializer_SameServiceAndAction_ReturnsSameInstance()
    {
        var first = SoapFactory.GetRequestSerializer<Services.AVTransportService.NextRequest>(nameof(Services.SonosService.AVTransport), "Next");
        var second = SoapFactory.GetRequestSerializer<Services.AVTransportService.NextRequest>(nameof(Services.SonosService.AVTransport), "Next");

        Assert.Same(first, second);
    }

    [Fact]
    public void GetResponseSerializer_SameService_ReturnsSameInstance()
    {
        var first = SoapFactory.GetResponseSerializer<Services.DevicePropertiesService.GetZoneInfoResponse>(nameof(Services.SonosService.DeviceProperties));
        var second = SoapFactory.GetResponseSerializer<Services.DevicePropertiesService.GetZoneInfoResponse>(nameof(Services.SonosService.DeviceProperties));

        Assert.Same(first, second);
    }

    [Fact]
    public void ParseXml_TwoResponseTypesOfOneService_ParsesEachType()
    {
        const string xmlGetZoneAttributesResponse = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetZoneAttributesResponse xmlns:u=""urn:schemas-upnp-org:service:DeviceProperties:1""><CurrentZoneName>Kitchen</CurrentZoneName></u:GetZoneAttributesResponse></s:Body></s:Envelope>";

        var zoneInfo = SoapFactory.ParseXml<Services.DevicePropertiesService.GetZoneInfoResponse>(nameof(Services.SonosService.DeviceProperties), xmlGetZoneInfoResponse);
        var zoneAttributes = SoapFactory.ParseXml<Services.DevicePropertiesService.GetZoneAttributesResponse>(nameof(Services.SonosService.DeviceProperties), xmlGetZoneAttributesResponse);

        Assert.Equal("AA:2A:1B:AA:AA:AA", zoneInfo.MACAddress);
        Assert.Equal("Kitchen", zoneAttributes.CurrentZoneName);
    }

    [Fact]
    public void GeneratesXml_SharedRequestTypeForTwoActions_UsesTheActionOfEachCall()
    {
        // Actions without arguments share one request type per service, so the serializer must not be reused across actions.
        var timeNow = SoapFactory.GenerateXml(nameof(Services.SonosService.AlarmClock), "GetTimeNow", new Services.AlarmClockService.BaseRequest());
        var format = SoapFactory.GenerateXml(nameof(Services.SonosService.AlarmClock), "GetFormat", new Services.AlarmClockService.BaseRequest());
        var timeNowAgain = SoapFactory.GenerateXml(nameof(Services.SonosService.AlarmClock), "GetTimeNow", new Services.AlarmClockService.BaseRequest());

        Assert.Contains("<u:GetTimeNow xmlns:u=\"urn:schemas-upnp-org:service:AlarmClock:1\"", timeNow);
        Assert.Contains("<u:GetFormat xmlns:u=\"urn:schemas-upnp-org:service:AlarmClock:1\"", format);
        Assert.Equal(timeNow, timeNowAgain);
    }
}