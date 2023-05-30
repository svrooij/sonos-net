/*
 * Sonos-net
 *
 * Repository https://github.com/svrooij/sonos-net
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Sonos.Base.Soap;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
public class Envelope<TBody>
{
    public Envelope()
    { }

    public Envelope(TBody body)
    {
        Body = new EnvelopeBody<TBody> { Message = body };
    }

    [System.Xml.Serialization.XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
    public EnvelopeBody<TBody> Body { get; set; }

    [System.Xml.Serialization.XmlAttribute("encodingStyle", Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public string EncodingStyle { get; set; } = "http://schemas.xmlsoap.org/soap/encoding/";
}

[Serializable()]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class EnvelopeBody<TBody>
{
    //[System.Xml.Serialization.XmlElement("GetBassResponse", Namespace = "urn:schemas-upnp-org:service:RenderingControl:1", Type = typeof(Services.RenderingControlService.GetBassResponse))]
    //[System.Xml.Serialization.XmlAnyElement()]
    public TBody Message { get; set; }
}

[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class SoapFault
{
    [System.Xml.Serialization.XmlElement("faultcode", Namespace = "")]

    public string FaultCode { get; set; }
    [System.Xml.Serialization.XmlElement("faultstring", Namespace = "")]

    public string FaultString { get; set; }

    [System.Xml.Serialization.XmlElement("detail", Namespace = "")]

    public FaultDetail? Detail { get; set; }

    [System.Xml.Serialization.XmlIgnore]
    public int? UpnpErrorCode
    {
        get => Detail?.UpnpError?.ErrorCode;
    }
}

public class FaultDetail
{
    [System.Xml.Serialization.XmlElement("UPnPError", Namespace = "urn:schemas-upnp-org:control-1-0")]

    public UpnpError UpnpError { get; set; }
}

public class UpnpError
{
    [System.Xml.Serialization.XmlElement("errorCode")]
    public int ErrorCode { get; set; }

}

