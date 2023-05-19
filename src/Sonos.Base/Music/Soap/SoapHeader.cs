using System.Xml.Serialization;

namespace Sonos.Base.Music.Soap
{
    public class SoapHeader
    {
        [XmlElement(ElementName = "context", Namespace = "http://www.sonos.com/Services/1.1")]
        public SoapHeaderContext? Context { get; set; }

        [XmlElement(ElementName = "credentials", Namespace = "http://www.sonos.com/Services/1.1")]
        public SoapHeaderCredentials Credentials { get; set; }
    }
}