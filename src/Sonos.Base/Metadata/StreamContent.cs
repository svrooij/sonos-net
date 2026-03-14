using System.Xml.Serialization;

namespace Sonos.Base.Metadata;

public class StreamContent
{
    [XmlText]
    public string Value { get; set; }

    public override string ToString()
    {
        return Value;
    }
}