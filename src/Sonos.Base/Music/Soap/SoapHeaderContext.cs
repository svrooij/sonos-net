using System.Xml.Serialization;

namespace Sonos.Base.Music.Soap
{
    public class SoapHeaderContext
    {
        [XmlElement(ElementName = "timeZone", Namespace = "http://www.sonos.com/Services/1.1")]
        public string? TimeZone { get; set; }
    }
}