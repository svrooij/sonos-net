using Sonos.Base.Events.Models;
using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace Sonos.Base.Events.Parsing;

[Serializable]
[XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:metadata-1-0/AVT/")]
[XmlRoot("Event", Namespace = "urn:schemas-upnp-org:metadata-1-0/AVT/", IsNullable = false)]

public partial class AVTransportEventRoot : IParsedEvent<Models.AVTransportEvent>
{
    [XmlElement("InstanceID")]
    public AVTransportEventInstance? Instance { get; set; }

    //public Dictionary<string, string>? EventProperties() => Instance?.GetEventProperties();

    public AVTransportEvent? GetEvent() => AVTransportEvent.FromDictionary(Instance?.GetEventProperties());
}

[XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:metadata-1-0/AVT/")]

public partial class AVTransportEventInstance
{
    [XmlAttribute("val")]
    public int InstanceID { get; set; }

    [XmlAnyElement]
    public XmlElement[]? OtherElements { get; set; }

    public Dictionary<string, string>? GetEventProperties()
    {
        return OtherElements?.Select(e => new KeyValuePair<string, string>(e.LocalName, e.GetAttribute("val"))).ToDictionary(e => e.Key, e => e.Value);
    }
}
