namespace Sonos.Base.Soap;
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
public class Envelope<TBody>
{
    public Envelope() { }
    public Envelope(TBody body) {
        Body = new EnvelopeBody<TBody> { Message = body };
    }

    [System.Xml.Serialization.XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
    public EnvelopeBody<TBody> Body { get; set; }

    [System.Xml.Serialization.XmlAttribute("encodingStyle", Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public string EncodingStyle { get; set; } = "http://schemas.xmlsoap.org/soap/encoding/";
}

[System.Serializable()]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class EnvelopeBody<TBody>
{
    //[System.Xml.Serialization.XmlElement("GetBassResponse", Namespace = "urn:schemas-upnp-org:service:RenderingControl:1", Type = typeof(Services.RenderingControlService.GetBassResponse))]
    //[System.Xml.Serialization.XmlAnyElement()]
    public TBody Message { get; set; }
}

// [System.Serializable()]
// [System.ComponentModel.DesignerCategory("code")]
// [System.Xml.Serialization.XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
// public class ResponseEnvelope<TBody>
// {
//     public ResponseEnvelope() { }
//     // public ResponseEnvelope(TBody body) {
//     //     Body = new EnvelopeBody<TBody> { Message = body };
//     // }

//     [System.Xml.Serialization.XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
//     public ResponseEnvelopeBody<TBody> Body { get; set; }

//     [System.Xml.Serialization.XmlAttribute("encodingStyle", Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
//     public string EncodingStyle { get; set; } = "http://schemas.xmlsoap.org/soap/encoding/";
// }

// [System.Serializable()]
// [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
// public class ResponseEnvelopeBody<TBody>
// {
//     // [System.Xml.Serialization.XmlElement("GetBassResponse", Namespace = "urn:schemas-upnp-org:service:RenderingControl:1", Type = typeof(Services.RenderingControlService.GetBassResponse))]
//     public TBody Message { get; set; }
    
//     // [System.Xml.Serialization.XmlAnyElement()]
//     // public System.Xml.XmlNode Other { get; set; }
// }

