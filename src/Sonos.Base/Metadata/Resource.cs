using System.Xml.Serialization;

namespace Sonos.Base.Metadata;
public class Resource
{
    [XmlAttribute("protocolInfo")]
    public string? ProtocolInfo { get; set; }

    [XmlAttribute("duration")]
    public string? Duration { get; set; }

    [XmlText]
    public string Uri { get; set; }
}