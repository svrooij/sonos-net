using System.Xml.Serialization;

namespace Sonos.Base.Metadata;

[XmlType(AnonymousType = false, Namespace = "")] //, Namespace = "urn:schemas-rinconnetworks-com:metadata-1-0/"
public class DidlDesc
{
    public DidlDesc() { }
    public DidlDesc(string value)
    {
        Value = value;
    }

    [XmlAttribute("id")]
    public string Id { get; set; } = "cdudn";

    /// <summary>
    /// This is a trick to set the correct namespace on the desc element. Do not change the value!
    /// </summary>
    [XmlAttribute("nameSpace")]
    public string NameSpace { get; set; } = "urn:schemas-rinconnetworks-com:metadata-1-0/";

    [XmlText]
    public string Value { get; set; } = "RINCON_AssociatedZPUDN";
}