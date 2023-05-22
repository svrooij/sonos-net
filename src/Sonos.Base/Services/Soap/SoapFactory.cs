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

using System.Xml;
using System.Xml.Serialization;

internal static class SoapFactory
{
    internal static HttpRequestMessage CreateRequest<TPayload>(Uri baseUri, string path, TPayload payload, string? action) where TPayload : class
    {
        var attr = SonosServiceRequestAttribute.GetSonosServiceRequestAttribute<TPayload>();
        if (action is null)
        {
            throw new ArgumentException("Could not determine action");
        }

        var xml = GenerateXmlStream(attr.ServiceName, attr.Action ?? action, payload);
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(baseUri, attr.Path))
        {
            // Content = new StringContent(xml, System.Text.Encoding.UTF8, "text/xml")
            Content = new StreamContent(xml)
        };
        // Sonos doesn't like content-type 'text/xml; charset=utf-8'
        request.Content.Headers.Remove("content-type");
        request.Content.Headers.TryAddWithoutValidation("Content-Type", "text/xml; charset=\"utf-8\"");
        request.Headers.TryAddWithoutValidation("soapaction", $"urn:schemas-upnp-org:service:{attr.ServiceName}:1#{attr.Action ?? action}");
        return request;
    }

    internal static string GenerateXml<TPayload>(string service, string action, TPayload payload) where TPayload : class
    {
        var envelope = new Soap.Envelope<TPayload>(payload);
        var overrides = GenerateOverrides<EnvelopeBody<TPayload>>(service, action, nameof(envelope.Body.Message));
        var ns = SoapNamespaces();

        var serializer = new XmlSerializer(envelope.GetType(), overrides);
        using var textWriter = new Utf8StringWriter();
        serializer.Serialize(textWriter, envelope, ns);
        return textWriter.ToString();
    }

    internal static Stream GenerateXmlStream<TPayload>(string service, string action, TPayload payload) where TPayload : class
    {
        var stream = new MemoryStream();
        var envelope = new Soap.Envelope<TPayload>(payload);
        var overrides = GenerateOverrides<EnvelopeBody<TPayload>>(service, action, nameof(envelope.Body.Message));
        var ns = SoapNamespaces();

        var serializer = new XmlSerializer(envelope.GetType(), overrides);
        using var xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
        {
            Indent = false,
            NewLineHandling = NewLineHandling.None,
        });
        
        serializer.Serialize(xmlWriter, envelope, ns);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    internal static XmlAttributeOverrides GenerateOverrides<TBody>(string service, string elementName, string property = "Message") where TBody : class
    {
        XmlElementAttribute messageAttribute = new XmlElementAttribute(elementName) { Namespace = $"urn:schemas-upnp-org:service:{service}:1", Form = System.Xml.Schema.XmlSchemaForm.Qualified };
        var myAttributes = new XmlAttributes();
        myAttributes.XmlElements.Add(messageAttribute);
        var overrides = new XmlAttributeOverrides();
        overrides.Add(typeof(TBody), property, myAttributes);
        return overrides;
    }

    internal static XmlAttributeOverrides GenerateResponseOverrides<TBody>(string service, string property = "Message") where TBody : class
    {
        XmlElementAttribute messageAttribute = new XmlElementAttribute(typeof(TBody).Name) { Namespace = $"urn:schemas-upnp-org:service:{service}:1", Type = typeof(TBody) };
        var myAttributes = new XmlAttributes();
        myAttributes.XmlElements.Add(messageAttribute);
        var overrides = new XmlAttributeOverrides();
        overrides.Add(typeof(EnvelopeBody<TBody>), property, myAttributes);
        return overrides;
    }

    internal static XmlAttributeOverrides GenerateFaultResponseOverrides()
    {
        XmlElementAttribute messageAttribute = new XmlElementAttribute("Fault") { Namespace = "http://schemas.xmlsoap.org/soap/envelope/", Type = typeof(SoapFault) };
        var myAttributes = new XmlAttributes();
        myAttributes.XmlElements.Add(messageAttribute);
        var overrides = new XmlAttributeOverrides();
        overrides.Add(typeof(EnvelopeBody<SoapFault>), nameof(EnvelopeBody<SoapFault>.Message), myAttributes);
        return overrides;
    }

    internal static XmlSerializerNamespaces SoapNamespaces()
    {
        var ns = new XmlSerializerNamespaces();
        ns.Add("", ""); // Remove unwanted xsd namespaces.
        ns.Add("s", "http://schemas.xmlsoap.org/soap/envelope/");
        return ns;
    }

    internal static TOut ParseXml<TOut>(string service, string xml) where TOut : class
    {
        var overrides = GenerateResponseOverrides<TOut>(service);
        var serializer = new XmlSerializer(typeof(Envelope<TOut>), overrides);
        using var textReader = new StringReader(xml);
        var result = (Envelope<TOut>?)serializer.Deserialize(textReader);
        if (result is null)
        {
            throw new FormatException("Response does not contain expected result");
        }
        return result.Body.Message;
    }

    internal static TOut ParseXml<TOut>(string service, Stream stream) where TOut : class
    {
        var overrides = GenerateResponseOverrides<TOut>(service);
        var serializer = new XmlSerializer(typeof(Envelope<TOut>), overrides);

        var result = (Envelope<TOut>?)serializer.Deserialize(stream);
        if (result is null)
        {
            throw new FormatException("Response does not contain expected result");
        }
        return result.Body.Message;
    }

    internal static TOut ParseEmbeddedXml<TOut>(string xml) where TOut : class
    {
        var serializer = new XmlSerializer(typeof(TOut));
        using var textReader = new StringReader(xml);
        var result = (TOut?)serializer.Deserialize(textReader);
        if (result is null)
        {
            throw new FormatException("Response does not contain expected result");
        }
        return result;
    }

    internal static SoapFault? ParseFaultXml(string xml)
    {
        var overrides = GenerateFaultResponseOverrides();
        var serializer = new XmlSerializer(typeof(Envelope<SoapFault>), overrides);
        using var textReader = new StringReader(xml);
        var result = (Envelope<SoapFault>?)serializer.Deserialize(textReader);
        if (result is null)
        {
            return null;
            //throw new FormatException("Response does not contain expected result");
        }
        return result.Body.Message;
    }

    internal static SoapFault? ParseFaultXml(Stream stream)
    {
        var overrides = GenerateFaultResponseOverrides();
        var serializer = new XmlSerializer(typeof(Envelope<SoapFault>), overrides);
        var result = (Envelope<SoapFault>?)serializer.Deserialize(stream);
        if (result is null)
        {
            return null;
            //throw new FormatException("Response does not contain expected result");
        }
        return result.Body.Message;
    }
}