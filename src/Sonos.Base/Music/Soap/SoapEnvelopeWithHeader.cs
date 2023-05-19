namespace Sonos.Base.Music.Soap
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class Envelope<TBody>
    {
        public Envelope()
        { }

        [System.Xml.Serialization.XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public EnvelopeBody<TBody> Body { get; set; }
    }

    [Serializable()]
    [System.Xml.Serialization.XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class SoapEnvelopeWithHeader<THeader, TBody>
    {
        public SoapEnvelopeWithHeader()
        { }

        public SoapEnvelopeWithHeader(THeader header, TBody body)
        {
            this.Header = header;
            Body = new EnvelopeBody<TBody> { Message = body };
        }

        [System.Xml.Serialization.XmlElement("Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public THeader Header { get; set; }

        [System.Xml.Serialization.XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public EnvelopeBody<TBody> Body { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeBody<TBody>
    {
        //[System.Xml.Serialization.XmlElement("GetBassResponse", Namespace = "urn:schemas-upnp-org:service:RenderingControl:1", Type = typeof(Services.RenderingControlService.GetBassResponse))]
        //[System.Xml.Serialization.XmlAnyElement()]
        public TBody Message { get; set; }
    }
}