using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Sonos.Base.Soap;

namespace Sonos.Base.Music.Models;
internal static class SoapParser
{
    internal static XmlAttributeOverrides GenerateResponseOverrides<TBody>() where TBody : class
    {
        string elementName = typeof(TBody).Name;
        // make sure the first letter is lowercase
        elementName = char.ToLower(elementName[0]) + elementName.Substring(1);
        XmlElementAttribute messageAttribute = new XmlElementAttribute(elementName) { Namespace = $"http://www.sonos.com/Services/1.1", Type = typeof(TBody) };
        var myAttributes = new XmlAttributes();
        myAttributes.XmlElements.Add(messageAttribute);
        var overrides = new XmlAttributeOverrides();
        overrides.Add(typeof(EnvelopeBody<TBody>), nameof(EnvelopeBody<TBody>.Message), myAttributes);
        return overrides;
    }

    internal static TOut ParseXml<TOut>(string stream) where TOut : class
    {
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(stream));
        return ParseXml<TOut>(memoryStream);
    }

    internal static TOut ParseXml<TOut>(Stream stream) where TOut : class
    {
        var overrides = GenerateResponseOverrides<TOut>();
        var serializer = new XmlSerializer(typeof(Envelope<TOut, MusicServiceSoapFault>), overrides);

        var result = (Envelope<TOut, MusicServiceSoapFault>?)serializer.Deserialize(stream);
        if (result?.Body is null)
        {
            throw new SonosException("Response does not contain expected soap body");
        }

        if (result.Body.Fault is not null)
        {
            throw new SmapiException(result.Body.Fault.FaultCode, result.Body.Fault.FaultString, result.Body.Fault.Detail?.RefreshAuthTokenResult);
        }

        return result.Body.Message!;
    }
}
