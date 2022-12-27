using System;
using System.Xml;
using System.Xml.Serialization;

namespace Sonos.Base.Events.Parsing;


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:event-1-0")]
[System.Xml.Serialization.XmlRoot("propertyset", Namespace = "urn:schemas-upnp-org:event-1-0", IsNullable = false)]
public partial class PropertyCollection
{

    private EventProperty[]? propertyField;

    /// <remarks/>
    [XmlElement("property", Namespace = "urn:schemas-upnp-org:event-1-0")]
    public EventProperty[]? property
    {
        get
        {
            return this.propertyField;
        }
        set
        {
            this.propertyField = value;
        }
    }

    internal Dictionary<string, string>? GetEventProperties()
    {
        if (property is null || property.Length == 0)
        {
            return null;
        }
        if (property.Length == 1 && property[0].LastChange is not null)
        {
            return null;
        }
#pragma warning disable CS8604 // Possible null reference argument.
        return property
            .Where(p => p.OtherElements is not null)
            .Select(p => p.OtherElements.First())
            .Select(p => new KeyValuePair<string, string>(p.LocalName, p.InnerText))
            .ToDictionary(e => e.Key, e => e.Value);
#pragma warning restore CS8604 // Possible null reference argument.
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType("property", AnonymousType = true, Namespace = "urn:schemas-upnp-org:event-1-0")]
public partial class EventProperty
{

    private string? lastChangeField;

    /// <remarks/>
    [XmlElement(Namespace = "")]
    public string? LastChange
    {
        get
        {
            return this.lastChangeField;
        }
        set
        {
            this.lastChangeField = value;
        }
    }

    [XmlAnyElement]
    public XmlElement[]? OtherElements { get; set; }
}


