using System.Xml.Serialization;

namespace Sonos.Base.Music.Soap
{
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class EnvelopeWithFault
    {
        public EnvelopeWithFault()
        { }

        [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public EnvelopeBodyFault Body { get; set; }
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeBodyFault
    {
        [XmlElement("Fault")]
        public SoapFault? Fault { get; set; }
    }

    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class SoapFault
    {
        [XmlElement("faultcode", Namespace = "")]
        public string Code { get; set; }

        [XmlElement("faultstring", Namespace = "")]
        public SoapFaultMessage? Message { get; set; }

        [XmlElement("detail", Namespace = "")]
        public SonosFaultDetails? Details { get; set; }
    }

    public class SoapFaultMessage
    {
        [XmlAttribute("lang", Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Language { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "")]
    [XmlRoot(Namespace = "")]
    public class SonosFaultDetails
    {
        [XmlElement("refreshAuthTokenResult")]
        public RefreshAuthTokenResult? NewToken { get; set; }
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    public class RefreshAuthTokenResult
    {
        [XmlElement("authToken", Namespace = "http://www.sonos.com/Services/1.1")]
        public string AuthToken { get; set; }

        [XmlElement("privateKey", Namespace = "http://www.sonos.com/Services/1.1")]
        public string PrivateKey { get; set; }
    }
}