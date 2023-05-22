using System.Xml.Serialization;

namespace Sonos.Base.Music.Models
{
    [Serializable()]
    [XmlRoot(Namespace = "http://www.sonos.com/Services/1.1")]
    public class GetMetadataRequest : MusicClientBaseRequest
    {
        [XmlElement(ElementName = "id", Namespace = "http://www.sonos.com/Services/1.1")]
        public string Id { get; set; }

        [XmlElement(ElementName = "index", Namespace = "http://www.sonos.com/Services/1.1")]
        public int Index { get; set; } = 0;

        [XmlElement(ElementName = "count", Namespace = "http://www.sonos.com/Services/1.1")]
        public int Count { get; set; } = 100;

        [XmlElement(ElementName = "recursive", Namespace = "http://www.sonos.com/Services/1.1")]
        public bool? Recursive { get; set; } = null;

        public bool ShouldSerializeRecursive() => Recursive.HasValue;
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    [XmlRoot(Namespace = "http://www.sonos.com/Services/1.1", IsNullable = false)]
    public partial class GetMetadataResponse: ISmapiResponse<GetMetadataResult>
    {
        [XmlElement("getMetadataResult")]
        public GetMetadataResult Result { get; set; }

        //public GetMetadataResult GetResult() => Result;
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    public partial class GetMetadataResult
    {
        [XmlElement("index")]
        public int Index { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("total")]
        public int Total { get; set; }

        /// <remarks/>
        [XmlElement("mediaCollection")]
        public MediaCollection[]? MediaCollection
        {
            get; set;
        }

        [XmlElement("mediaMetadata")]
        public MediaMetadata[]? MediaMetadata
        {
            get; set;
        }

        public override string ToString()
        {
            return $"Metadata result with {Count} results";
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    public partial class MediaCollection : MediaMetadataBase
    {
        [XmlElement("authRequired")]
        public bool AuthRequired { get; set; }

        [XmlElement("canPlay")]
        public bool CanPlay { get; set; }

        [XmlElement("canEnumerate")]
        public bool CanEnumerate { get; set; }

        [XmlElement("canCache")]
        public bool CanCache
        {
            get; set;
        }

        [XmlElement("homogeneous")]
        public bool Homogeneous
        {
            get; set;
        }

        [XmlElement("canAddToFavorite")]
        public bool CanAddToFavorite
        {
            get; set;
        }

        [XmlElement("canScroll")]
        public bool CanScroll
        {
            get; set;
        }

        [XmlElement("albumArtURI")]
        public string AlbumArtURI
        {
            get; set;
        }

        [XmlElement("releaseDate")]
        public System.DateTime ReleaseDate
        {
            get; set;
        }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    public partial class MediaMetadataBase
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("itemType")]
        public string ItemType { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Title} ({ItemType})";
        }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    public partial class MediaMetadata : MediaMetadataBase
    {
        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("genreId")]
        public string GenreId { get; set; }

        [XmlElement("genre")]
        public string Genre { get; set; }

        [XmlElement("twitterId")]
        public string TwitterId { get; set; }

        [XmlElement("liveNow")]
        public bool LiveNow { get; set; }

        [XmlElement("onDemand")]
        public bool OnDemand { get; set; }

        [XmlElement("streamMetadata")]
        public StreamMetadata? StreamMetadata { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    public partial class StreamMetadata
    {
        [XmlElement("currentShowId")]
        public string CurrentShowId { get; set; }

        [XmlElement("currentShow")]
        public string CurrentShow { get; set; }

        [XmlElement("currentHost")]
        public string CurrentHost { get; set; }

        [XmlElement("logo")]
        public string Logo { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("subtitle")]
        public string Subtitle { get; set; }

        [XmlElement("secondsRemaining")]
        public int? SecondsRemaining { get; set; }

        [XmlElement("secondsToNextShow")]
        public int? SecondsToNextShow { get; set; }

        [XmlElement("nextShowSeconds")]
        public int? NextShowSeconds { get; set; }
    }
}