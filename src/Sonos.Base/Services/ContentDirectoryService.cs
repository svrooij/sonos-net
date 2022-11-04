namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

public partial class ContentDirectoryService
{
    /// <summary>
    /// Browse with defaults
    /// </summary>
    /// <param name="ObjectID">Object ID for browsing</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>(1) If the title contains an apostrophe the returned uri will contain a `&apos;`. (2) Some libraries support a BrowseAndParse, so you don't have to parse the xml.</remarks>
    /// <returns>BrowseResponse</returns>
    public Task<BrowseResponse> Browse(string ObjectID, string BrowseFlag = "BrowseDirectChildren", string Filter = "*", int StartingIndex = 0, int Count = 0, string SortCriteria = "", CancellationToken cancellationToken = default)
    {
        return Browse(new BrowseRequest
        {
            ObjectID = ObjectID,
            BrowseFlag = BrowseFlag,
            Filter = Filter,
            StartingIndex = StartingIndex,
            RequestedCount = Count,
            SortCriteria = SortCriteria
        }, cancellationToken);
    }

    public Task<BrowseResponse> CurrentQueue(int count = 100, CancellationToken cancellationToken = default) => Browse("Q:0", Count: count, cancellationToken: cancellationToken);
    public Task<BrowseResponse> FavoriteRadioShows(int count = 100, CancellationToken cancellationToken = default) => Browse("R:0/1", Count: count, cancellationToken: cancellationToken);
    public Task<BrowseResponse> FavoriteRadioStations(int count = 100, CancellationToken cancellationToken = default) => Browse("R:0/0", Count: count, cancellationToken: cancellationToken);
    public Task<BrowseResponse> Favorites(int count = 100, CancellationToken cancellationToken = default) => Browse("FV:2", Count: count, cancellationToken: cancellationToken);

    public partial class BrowseResponse
    {
        public Metadata.Didl? ResultObject
        {
            get
            {
                return Metadata.DidlSerializer.DeserializeMetadata(Result);
            }
        }
    }

}
