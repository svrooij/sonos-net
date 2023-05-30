using System.Xml.Serialization;

namespace Sonos.Base.Music.Soap
{
    [Serializable()]
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class Envelope<TBody>
    {
        public Envelope()
        { }

        [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public EnvelopeBody<TBody> Body { get; set; }
    }

    [Serializable()]
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class SoapEnvelopeWithHeader<THeader, TBody>
    {
        public SoapEnvelopeWithHeader()
        { }

        public SoapEnvelopeWithHeader(THeader header, TBody body)
        {
            this.Header = header;
            Body = new EnvelopeBody<TBody> { Message = body };
        }

        [XmlElement("Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public THeader Header { get; set; }

        [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public EnvelopeBody<TBody> Body { get; set; }
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeBody<TBody>
    {
        public TBody Message { get; set; }

        [XmlElement("Fault", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public SoapFault? Fault { get; set; }
    }
}