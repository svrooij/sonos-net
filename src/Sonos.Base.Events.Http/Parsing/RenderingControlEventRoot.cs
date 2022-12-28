using Sonos.Base.Events.Http.Models;
using System.Xml;
using System.Xml.Serialization;

namespace Sonos.Base.Events.Http.Parsing;

[Serializable]
[XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:metadata-1-0/RCS/")]
[XmlRoot("Event", Namespace = "urn:schemas-upnp-org:metadata-1-0/RCS/", IsNullable = false)]

public partial class RenderingControlEventRoot : IParsedEvent<Models.RenderingControlEvent>
{
    [XmlElement("InstanceID")]
    public RenderingControlEventInstance? Instance { get; set; }

    //public Dictionary<string, string>? EventProperties() => Instance?.GetEventProperties();

    public RenderingControlEvent? GetEvent() => RenderingControlEvent.FromDictionary(Instance?.GetEventProperties());
}

[XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:metadata-1-0/RCS/")]

public partial class RenderingControlEventInstance
{
    [XmlAttribute("val")]
    public int InstanceID { get; set; }

    [XmlAnyElement]
    public XmlElement[]? OtherElements { get; set; }

    public Dictionary<string, string>? GetEventProperties()
    {
        return OtherElements?.Select(e => new KeyValuePair<string, string>($"{e.LocalName}{e.GetAttribute("channel")}", e.GetAttribute("val"))).ToDictionary(e => e.Key, e => e.Value);
    }
}
