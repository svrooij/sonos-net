using System.Xml.Serialization;

namespace Sonos.Base.Metadata;

[XmlType(AnonymousType = true, Namespace = "urn:schemas-rinconnetworks-com:metadata-1-0/")]
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