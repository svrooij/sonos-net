using System.Runtime;
using System.Xml;
using System.Xml.Serialization;

namespace Sonos.Base.Music.Soap
{
    internal static class SoapFactory
    {
        internal static HttpRequestMessage CreateRequest<TPayload>(Uri baseUri, SoapHeader header, TPayload payload, string action) where TPayload : class
        {
            //if (action is null)
            //{
            //    throw new ArgumentException("Could not determine action");
            //}

            var xml = GenerateXmlStream(action, header, payload);
            var request = new HttpRequestMessage(HttpMethod.Post, baseUri)
            {
                // Content = new StringContent(xml, System.Text.Encoding.UTF8, "text/xml")
                Content = new StreamContent(xml)
            };
            // Sonos doesn't like content-type 'text/xml; charset=utf-8'
            request.Content.Headers.Remove("content-type");
            request.Content.Headers.TryAddWithoutValidation("Content-Type", "text/xml; charset=\"utf-8\"");
            request.Headers.TryAddWithoutValidation("SOAP-Action", $"http://www.sonos.com/Services/1.1#{action}");
            return request;
        }

        internal static string GenerateXml<TPayload>(string action, SoapHeader header, TPayload payload) where TPayload : class
        {
            var envelope = new SoapEnvelopeWithHeader<SoapHeader, TPayload>(header, payload);
            var overrides = GenerateOverrides<EnvelopeBody<TPayload>>(action, nameof(envelope.Body.Message));
            var ns = SoapNamespaces();

            var serializer = new XmlSerializer(envelope.GetType(), overrides);
            using var textWriter = new Base.Soap.Utf8StringWriter();
            serializer.Serialize(textWriter, envelope, ns);
            return textWriter.ToString();
        }

        internal static Stream GenerateXmlStream<TPayload>(string action, SoapHeader header, TPayload payload) where TPayload : class
        {
            var stream = new MemoryStream();
            var envelope = new SoapEnvelopeWithHeader<SoapHeader, TPayload>(header, payload);
            var overrides = GenerateOverrides<EnvelopeBody<TPayload>>(action, nameof(envelope.Body.Message));
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

        internal static XmlAttributeOverrides GenerateOverrides<TBody>(string elementName, string property = "Message") where TBody : class
        {
            XmlElementAttribute messageAttribute = new XmlElementAttribute(elementName) { Namespace = "http://www.sonos.com/Services/1.1" };
            var myAttributes = new XmlAttributes();
            myAttributes.XmlElements.Add(messageAttribute);
            var overrides = new XmlAttributeOverrides();
            overrides.Add(typeof(TBody), property, myAttributes);
            return overrides;
        }

        internal static XmlSerializerNamespaces SoapNamespaces()
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", ""); // Remove unwanted xsd namespaces.
            ns.Add("s", "http://schemas.xmlsoap.org/soap/envelope/");
            ns.Add("u", "http://www.sonos.com/Services/1.1");
            return ns;
        }

        internal static XmlAttributeOverrides GenerateResponseOverrides<TBody>(string action, string property = "Message") where TBody : class
        {
            XmlElementAttribute messageAttribute = new XmlElementAttribute($"{action}Response") { Namespace = $"http://www.sonos.com/Services/1.1", Type = typeof(TBody) };
            var myAttributes = new XmlAttributes();
            myAttributes.XmlElements.Add(messageAttribute);
            var overrides = new XmlAttributeOverrides();
            overrides.Add(typeof(EnvelopeBody<TBody>), property, myAttributes);
            return overrides;
        }

        internal static TOut ParseXml<TOut>(string action, string xml) where TOut : class
        {
            var overrides = GenerateResponseOverrides<TOut>(action);
            var serializer = new XmlSerializer(typeof(Envelope<TOut>), overrides);
            using var textReader = new StringReader(xml);
            var result = (Envelope<TOut>?)serializer.Deserialize(textReader);
            if (result is null)
            {
                throw new FormatException("Response does not contain expected result");
            }
            return result.Body.Message;
        }

        internal static SoapEnvelopeWithHeader<SoapHeader,TOut> ParseRequestXml<TOut>(string action, string xml) where TOut : class
        {
            var overrides = GenerateOverrides<EnvelopeBody<TOut>>($"{action}");
            
            var serializer = new XmlSerializer(typeof(SoapEnvelopeWithHeader<SoapHeader, TOut>), overrides);
            using var textReader = new StringReader(xml);
            var result = (SoapEnvelopeWithHeader<SoapHeader, TOut>?)serializer.Deserialize(textReader);
            if (result is null)
            {
                throw new FormatException("Response does not contain expected result");
            }
            return result;
        }

        internal static TOut ParseXml<TOut>(string action, Stream stream) where TOut : class
        {
            var overrides = GenerateResponseOverrides<TOut>(action);
            var serializer = new XmlSerializer(typeof(Envelope<TOut>), overrides);

            var result = (Envelope<TOut>?)serializer.Deserialize(stream);
            if (result is null)
            {
                throw new FormatException("Response does not contain expected result");
            }
            return result.Body.Message;
        }
    }
}