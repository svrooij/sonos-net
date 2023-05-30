using System.Xml.Serialization;

namespace Sonos.Base.Metadata;

[XmlType(AnonymousType = true, Namespace = "urn:schemas-rinconnetworks-com:metadata-1-0/")]
public class DidlDesc
{
    public const string Default = "RINCON_AssociatedZPUDN";
    public const string SpotifyEurope = "SA_RINCON2311_X_#Svc2311-0-Token";

    public DidlDesc()
    { }

    public DidlDesc(string value)
    {
        Value = value;
    }

    [XmlAttribute("id")]
    public string Id { get; set; } = "cdudn";

    [XmlText]
    public string Value { get; set; } = Default;
}