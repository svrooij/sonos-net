using System.Xml.Serialization;

namespace Sonos.Base.Soap;

internal static class SoapFactory
{
    internal static HttpRequestMessage CreateRequest<TPayload>(Uri baseUri, string path, string service, string action, TPayload payload)
    {
        var xml = GenerateXmlStream<TPayload>(service, action, payload);
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(baseUri, path))
        {
            // Content = new StringContent(xml, System.Text.Encoding.UTF8, "text/xml")
            Content = new StreamContent(xml)
        };
        // Sonos doesn't like content-type 'text/xml; charset=utf-8'
        request.Content.Headers.Remove("content-type");
        request.Content.Headers.TryAddWithoutValidation("Content-Type", "text/xml; charset=\"utf-8\"");
        request.Headers.TryAddWithoutValidation("soapaction", $"urn:schemas-upnp-org:service:{service}:1#{action}");
        return request;
    }

    internal static string GenerateXml<TPayload>(string service, string action, TPayload payload)
    {
        var envelope = new Soap.Envelope<TPayload>(payload);
        var overrides = GenerateOverrides<EnvelopeBody<TPayload>>(service, action, nameof(envelope.Body.Message));
        var ns = SoapNamespaces();

        var serializer = new XmlSerializer(envelope.GetType(), overrides);
        using var textWriter = new Utf8StringWriter();
        serializer.Serialize(textWriter, envelope, ns);
        return textWriter.ToString();
    }

    internal static Stream GenerateXmlStream<TPayload>(string service, string action, TPayload payload)
    {
        var stream = new MemoryStream();
        var envelope = new Soap.Envelope<TPayload>(payload);
        var overrides = GenerateOverrides<EnvelopeBody<TPayload>>(service, action, nameof(envelope.Body.Message));
        var ns = SoapNamespaces();

        var serializer = new XmlSerializer(envelope.GetType(), overrides);
        serializer.Serialize(stream, envelope, ns);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    internal static XmlAttributeOverrides GenerateOverrides<TBody>(string service, string elementName, string property = "Message")
    {
        XmlElementAttribute messageAttribute = new XmlElementAttribute(elementName) { Namespace = $"urn:schemas-upnp-org:service:{service}:1", Form = System.Xml.Schema.XmlSchemaForm.Qualified };
        var myAttributes = new XmlAttributes();
        myAttributes.XmlElements.Add(messageAttribute);
        var overrides = new XmlAttributeOverrides();
        overrides.Add(typeof(TBody), property, myAttributes);
        return overrides;
    }

    internal static XmlAttributeOverrides GenerateResponseOverrides<TBody>(string service, string property = "Message")
    {
        XmlElementAttribute messageAttribute = new XmlElementAttribute(typeof(TBody).Name) { Namespace = $"urn:schemas-upnp-org:service:{service}:1", Type = typeof(TBody) };
        var myAttributes = new XmlAttributes();
        myAttributes.XmlElements.Add(messageAttribute);
        var overrides = new XmlAttributeOverrides();
        overrides.Add(typeof(EnvelopeBody<TBody>), property, myAttributes);
        return overrides;
    }

    internal static XmlSerializerNamespaces SoapNamespaces()
    {
        var ns = new XmlSerializerNamespaces();
        ns.Add("", ""); // Remove unwanted xsd namespaces.
        ns.Add("s", "http://schemas.xmlsoap.org/soap/envelope/");
        return ns;
    }

    internal static TOut ParseXml<TOut>(string service, string xml)
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

    internal static TOut ParseXml<TOut>(string service, Stream stream)
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

    internal static TOut ParseEmbeddedXml<TOut>(string xml)
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
}