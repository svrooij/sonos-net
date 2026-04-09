using System.Xml.Serialization;

namespace Sonos.Base.Metadata;
public class VirtualLineIn
{
    [XmlAttribute("cookie")]
    public string? Cookie { get; set; }

    [XmlAttribute("group")]
    public string? Group { get; set; }
}