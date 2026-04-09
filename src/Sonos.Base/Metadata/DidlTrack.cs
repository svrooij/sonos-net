using System.Xml.Serialization;

namespace Sonos.Base.Metadata;

[Serializable]
[XmlRoot("item", Namespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/")]
public class DidlTrack
{
    [XmlAttribute("id")]
    public string? Id { get; set; }

    [XmlAttribute("parentID")]
    public string? ParentId { get; set; }

    [XmlAttribute("restricted")]
    public bool Restricted { get; set; }

    [XmlElement("title", Namespace = "http://purl.org/dc/elements/1.1/")]
    public string Title { get; set; }

    [XmlElement("class", Namespace = "urn:schemas-upnp-org:metadata-1-0/upnp/")]
    public string Class { get; set; }

    [XmlElement("album", Namespace = "urn:schemas-upnp-org:metadata-1-0/upnp/")]
    public string? Album { get; set; }

    [XmlElement("creator", Namespace = "http://purl.org/dc/elements/1.1/")]
    public string? Creator { get; set; }





    [XmlElement("albumArtURI", Namespace = "urn:schemas-upnp-org:metadata-1-0/upnp/")]
    public string? AlbumArtUri { get; set; }

    [XmlElement("albumArtist", Namespace = "urn:schemas-rinconnetworks-com:metadata-1-0/")]
    public string? AlbumArtist { get; set; }

    [XmlElement("description", Namespace = "urn:schemas-rinconnetworks-com:metadata-1-0/")]
    public string? Description { get; set; }

    [XmlElement("podcast", Namespace = "urn:schemas-rinconnetworks-com:metadata-1-0/")]
    public string? Podcast { get; set; }

    [XmlElement("tags", Namespace = "urn:schemas-rinconnetworks-com:metadata-1-0/")]
    public string? Tags { get; set; }

    [XmlElement("res")]
    public Resource? Resource { get; set; }

    [XmlElement("vli")]
    public VirtualLineIn? VitualLineIn { get; set; }

    [XmlElement("streamContent", Namespace = "urn:schemas-rinconnetworks-com:metadata-1-0/")]
    public StreamContent? StreamContent { get; set; }

    [XmlElement("desc", Form = System.Xml.Schema.XmlSchemaForm.None)] //Namespace = "urn:schemas-rinconnetworks-com:metadata-1-0/"
    public DidlDesc? Desc { get; set; }
}
