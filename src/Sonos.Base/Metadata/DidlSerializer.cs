using System.Xml.Serialization;

namespace Sonos.Base.Metadata;

public static class DidlSerializer
{
    internal const string NotImplemented = "NOT_IMPLEMENTED";
    public static Didl? DeserializeMetadata(string xml)
    {
        if (string.IsNullOrWhiteSpace(xml) || xml == NotImplemented)
        {
            return null;
        }
        var serializer = new XmlSerializer(typeof(Didl));
        // serializer.UnknownNode += (sender, args) =>
        //     {
        //         Console.WriteLine($"Unknown Node:{args.Name}\t{args.Text}");
        //     };
        // serializer.UnknownElement +=
        //     (sender, args) =>
        //         Console.WriteLine("Unknown Element:"
        //             + args.Element.Name + "\t" + args.Element.InnerXml);
        // serializer.UnreferencedObject +=
        //     (sender, args) =>
        //         Console.WriteLine("Unreferenced Object:"
        //             + args.UnreferencedId + "\t" + args.UnreferencedObject.ToString());
        using var textReader = new StringReader(xml);
        var result = (Didl?)serializer.Deserialize(textReader);
        if (result is null)
        {
            throw new FormatException("Metadata not found");
        }
        return result;
    }

    public static string SerializeMetadata(Didl metadata)
    {
        return string.Empty;
    }
}