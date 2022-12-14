namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// ContentDirectoryService  - Browse for local content
/// </summary>
public partial class ContentDirectoryService : SonosBaseService
{
    /// <summary>
    /// Create a new ContentDirectoryService
    /// </summary>
    /// <param name="sonosUri">Base URL of the speaker</param>
    /// <param name="httpClient">Optionally, a custom HttpClient.</param>
    public ContentDirectoryService(SonosServiceOptions options) : base("ContentDirectory", "/MediaServer/ContentDirectory/Control", "/MediaServer/ContentDirectory/Event", options) { }


    /// <summary>
    /// Browse for content: Music library (A), share(S:), Sonos playlists(SQ:), Sonos favorites(FV:2), radio stations(R:0/0), radio shows(R:0/1). Recommendation: Send one request, check the `TotalMatches` and - if necessary - do additional requests with higher `StartingIndex`. In case of duplicates only the first is returned! Example: albums with same title, even if artists are different
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>(1) If the title contains an apostrophe the returned uri will contain a `&apos;`. (2) Some libraries support a BrowseAndParse, so you don't have to parse the xml.</remarks>
    /// <returns>BrowseResponse</returns>
    public Task<BrowseResponse> Browse(BrowseRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<BrowseRequest, BrowseResponse>("Browse", request, cancellationToken);

    /// <summary>
    /// CreateObject
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>CreateObjectResponse</returns>
    public Task<CreateObjectResponse> CreateObject(CreateObjectRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<CreateObjectRequest, CreateObjectResponse>("CreateObject", request, cancellationToken);

    /// <summary>
    /// DestroyObject
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> DestroyObject(DestroyObjectRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<DestroyObjectRequest>("DestroyObject", request, cancellationToken);

    /// <summary>
    /// FindPrefix
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>FindPrefixResponse</returns>
    public Task<FindPrefixResponse> FindPrefix(FindPrefixRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<FindPrefixRequest, FindPrefixResponse>("FindPrefix", request, cancellationToken);

    /// <summary>
    /// GetAlbumArtistDisplayOption
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetAlbumArtistDisplayOptionResponse</returns>
    public Task<GetAlbumArtistDisplayOptionResponse> GetAlbumArtistDisplayOption(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetAlbumArtistDisplayOptionResponse>("GetAlbumArtistDisplayOption", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetAllPrefixLocations
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetAllPrefixLocationsResponse</returns>
    public Task<GetAllPrefixLocationsResponse> GetAllPrefixLocations(GetAllPrefixLocationsRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetAllPrefixLocationsRequest, GetAllPrefixLocationsResponse>("GetAllPrefixLocations", request, cancellationToken);

    /// <summary>
    /// GetBrowseable
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetBrowseableResponse</returns>
    public Task<GetBrowseableResponse> GetBrowseable(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetBrowseableResponse>("GetBrowseable", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetLastIndexChange
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetLastIndexChangeResponse</returns>
    public Task<GetLastIndexChangeResponse> GetLastIndexChange(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetLastIndexChangeResponse>("GetLastIndexChange", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetSearchCapabilities
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetSearchCapabilitiesResponse</returns>
    public Task<GetSearchCapabilitiesResponse> GetSearchCapabilities(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetSearchCapabilitiesResponse>("GetSearchCapabilities", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetShareIndexInProgress
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetShareIndexInProgressResponse</returns>
    public Task<GetShareIndexInProgressResponse> GetShareIndexInProgress(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetShareIndexInProgressResponse>("GetShareIndexInProgress", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetSortCapabilities
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetSortCapabilitiesResponse</returns>
    public Task<GetSortCapabilitiesResponse> GetSortCapabilities(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetSortCapabilitiesResponse>("GetSortCapabilities", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetSystemUpdateID
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetSystemUpdateIDResponse</returns>
    public Task<GetSystemUpdateIDResponse> GetSystemUpdateID(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetSystemUpdateIDResponse>("GetSystemUpdateID", new BaseRequest(), cancellationToken);

    /// <summary>
    /// RefreshShareIndex
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RefreshShareIndex(RefreshShareIndexRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RefreshShareIndexRequest>("RefreshShareIndex", request, cancellationToken);

    /// <summary>
    /// RequestResort
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RequestResort(RequestResortRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RequestResortRequest>("RequestResort", request, cancellationToken);

    /// <summary>
    /// SetBrowseable
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetBrowseable(SetBrowseableRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetBrowseableRequest>("SetBrowseable", request, cancellationToken);

    /// <summary>
    /// UpdateObject
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> UpdateObject(UpdateObjectRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<UpdateObjectRequest>("UpdateObject", request, cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:ContentDirectory:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BrowseRequest : BaseRequest
    {

        /// <summary>
        /// The search query, (`A:ARTIST` / `A:ALBUMARTIST` / `A:ALBUM` / `A:GENRE` / `A:COMPOSER` / `A:TRACKS` / `A:PLAYLISTS` / `S:` / `SQ:` / `FV:2` / `R:0/0` / `R:0/1`) with optionally `:search+query` behind it.
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// How to browse
        /// </summary>
        public string BrowseFlag { get; set; }

        /// <summary>
        /// Which fields should be returned `*` for all.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Paging, where to start, usually 0
        /// </summary>
        public int StartingIndex { get; set; }

        /// <summary>
        /// Paging, number of items, maximum is 1,000. This parameter does NOT restrict the number of items being searched (filter) but only the number being returned. 
        /// </summary>
        public int RequestedCount { get; set; }

        /// <summary>
        /// Sort the results based on metadata fields. `+upnp:artist,+dc:title` for sorting on artist then on title.
        /// </summary>
        public string SortCriteria { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("BrowseResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class BrowseResponse
    {

        /// <summary>
        /// Encoded DIDL-Lite XML. See remark (2)
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string Result { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NumberReturned { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int TotalMatches { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int UpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class CreateObjectRequest : BaseRequest
    {

        public string ContainerID { get; set; }

        public string Elements { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("CreateObjectResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class CreateObjectResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string ObjectID { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string Result { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class DestroyObjectRequest : BaseRequest
    {

        public string ObjectID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class FindPrefixRequest : BaseRequest
    {

        public string ObjectID { get; set; }

        public string Prefix { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("FindPrefixResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class FindPrefixResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int StartingIndex { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int UpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetAlbumArtistDisplayOptionResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class GetAlbumArtistDisplayOptionResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AlbumArtistDisplayOption { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetAllPrefixLocationsRequest : BaseRequest
    {

        public string ObjectID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetAllPrefixLocationsResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class GetAllPrefixLocationsResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int TotalPrefixes { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string PrefixAndIndexCSV { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int UpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetBrowseableResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class GetBrowseableResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool IsBrowseable { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetLastIndexChangeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class GetLastIndexChangeResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string LastIndexChange { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetSearchCapabilitiesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class GetSearchCapabilitiesResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string SearchCaps { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetShareIndexInProgressResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class GetShareIndexInProgressResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool IsIndexing { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetSortCapabilitiesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class GetSortCapabilitiesResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string SortCaps { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetSystemUpdateIDResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
    public partial class GetSystemUpdateIDResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int Id { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RefreshShareIndexRequest : BaseRequest
    {

        public string AlbumArtistDisplayOption { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RequestResortRequest : BaseRequest
    {

        public string SortOrder { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetBrowseableRequest : BaseRequest
    {

        public bool Browseable { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class UpdateObjectRequest : BaseRequest
    {

        public string ObjectID { get; set; }

        public string CurrentTagValue { get; set; }

        public string NewTagValue { get; set; }
    }
}
