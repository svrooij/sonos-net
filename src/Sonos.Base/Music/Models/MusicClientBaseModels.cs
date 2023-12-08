namespace Sonos.Base.Music.Models
{
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class MusicClientBaseRequest
    {
        //[System.Xml.Serialization.XmlNamespaceDeclarations]
        //public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
        //  new[] { new System.Xml.XmlQualifiedName("sns", "http://www.sonos.com/Services/1.1"), });
    }

    internal interface ISmapiResponse<TResult>
    {
        TResult Result { get; }
    }
}