using System.Xml;
using System.Xml.Serialization;

namespace Sonos.Base.Metadata;

public static class DidlSerializer
{
    internal const string NotImplemented = "NOT_IMPLEMENTED";
    public static Didl? DeserializeMetadata(string? xml)
    {
        if (xml is null || string.IsNullOrWhiteSpace(xml) || xml == NotImplemented)
        {
            return null;
        }
        var serializer = new XmlSerializer(typeof(Didl));
        //serializer.UnknownElement += Serializer_UnknownElement;
        // Hack for not parsing elements with namespace prefix defined but not used. 
        xml = xml.Replace("<desc ", "<r:desc ").Replace("</desc>", "</r:desc>");
        using var textReader = new StringReader(xml);
        var result = (Didl?)serializer.Deserialize(textReader);
        if (result is null)
        {
            throw new FormatException("Metadata not found");
        }
        return result;
    }

    private static void Serializer_UnknownElement(object? sender, XmlElementEventArgs e)
    {
        if (sender == null)
        {

        }
    }

    public static string SerializeMetadata(Didl? metadata)
    {
        if (metadata is null)
        {
            return string.Empty;
        }
        var overrides = DidlWritingOverrides();
        var ns = DidlNamespaces();
        var settings = new XmlWriterSettings();
        settings.OmitXmlDeclaration = true;
        using var stream = new StringWriter();
        using var writer = XmlWriter.Create(stream, settings);

        var serializer = new XmlSerializer(metadata.GetType(), overrides);
        serializer.Serialize(writer, metadata, ns);
        return stream.ToString();
    }

    internal static XmlSerializerNamespaces DidlNamespaces()
    {
        var ns = new XmlSerializerNamespaces();
        ns.Add("", ""); // Remove unwanted xsd namespaces.
        return ns;
    }

    internal static XmlAttributeOverrides DidlWritingOverrides()
    {
        var attributes = new XmlAttributes();
        attributes.XmlIgnore = true;
        var overrides = new XmlAttributeOverrides();
        overrides.Add(typeof(Item), nameof(Item.Album), attributes);
        overrides.Add(typeof(Item), nameof(Item.AlbumArtUri), attributes);
        overrides.Add(typeof(Item), nameof(Item.Creator), attributes);
        overrides.Add(typeof(Resource), nameof(Resource.Duration), attributes);
        return overrides;
    }
}