using System.Xml.Serialization;

namespace Sonos.Base.Metadata;

public class DidlDesc
{
    public DidlDesc() { }
    public DidlDesc(string value)
    {
        Value = value;
    }
    [XmlAttribute("id")]
    public string Id { get; set; } = "cdudn";

    [XmlText]
    public string Value { get; set; } = "RINCON_AssociatedZPUDN";
}