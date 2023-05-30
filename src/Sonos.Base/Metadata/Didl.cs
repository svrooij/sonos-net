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

    public Didl()
    { }

    public Didl(Item item)
    {
        Items = new[] { item };
    }

    // [XmlAnyElement]
    // public object[] Rest {get;set;}
    [XmlElement("item", Namespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/")]
    public Item[] Items { get; set; }

    public static Didl GetMetadataForBroadcast(string? itemId, string didlDescription = "RINCON_AssociatedZPUDN")
    {
        return new Didl(new Item
        {
            Class = "object.item.audioItem.audioBroadcast",
            Id = $"10092020_{itemId}",
            Restricted = true,
            Title = "Some stream",
            Desc = new DidlDesc(didlDescription)
        });
    }

    public static Didl GetMetadataForPlaylist(string? itemId, string didlDescription = "RINCON_AssociatedZPUDN")
    {
        return new Didl(new Item
        {
            Class = "object.container.playlistContainer",
            Id = $"1006206c{itemId}".Replace(":", "%3a"),
            ParentId = "10fe2664playlists", // From Sonos app
            Restricted = true,
            Desc = new DidlDesc(didlDescription)
        });
    }
}