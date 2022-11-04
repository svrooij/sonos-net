namespace Sonos.Base.Metadata;

using System.Xml.Serialization;

[Serializable]
[XmlRoot("DIDL-Lite", Namespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/")]

public class Didl
{
    [XmlNamespaceDeclarations]
    public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces(
      new[] {
          new System.Xml.XmlQualifiedName("dc", "http://purl.org/dc/elements/1.1/"),
          new System.Xml.XmlQualifiedName("upnp", "urn:schemas-upnp-org:metadata-1-0/upnp/"),
          new System.Xml.XmlQualifiedName("r", "urn:schemas-rinconnetworks-com:metadata-1-0/"),
      });

    public Didl() { }
    public Didl(Item item)
    {
        Items = new[] { item };
    }

    // [XmlAnyElement]
    // public object[] Rest {get;set;}
    [XmlElement("item", Namespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/")]
    public Item[] Items { get; set; }
}
