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

namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

public partial class ContentDirectoryService
{
    /// <summary>
    /// Browse with defaults
    /// </summary>
    /// <param name="ObjectID">Object ID for browsing</param>
    /// <param name="BrowseFlag"></param>
    /// <param name="Filter"></param>
    /// <param name="StartingIndex"></param>
    /// <param name="Count"></param>
    /// <param name="SortCriteria"></param>
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

    /// <summary>
    /// Browse the current queue
    /// </summary>
    /// <param name="count">Number of items to load</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<BrowseResponse> CurrentQueue(int count = 100, CancellationToken cancellationToken = default) => Browse("Q:0", Count: count, cancellationToken: cancellationToken);

    /// <summary>
    /// Browse the favorite radio shows
    /// </summary>
    /// <param name="count"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<BrowseResponse> FavoriteRadioShows(int count = 100, CancellationToken cancellationToken = default) => Browse("R:0/1", Count: count, cancellationToken: cancellationToken);

    /// <summary>
    /// Browse the favorite radio stations
    /// </summary>
    /// <param name="count">Number of items to load</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<BrowseResponse> FavoriteRadioStations(int count = 100, CancellationToken cancellationToken = default) => Browse("R:0/0", Count: count, cancellationToken: cancellationToken);

    /// <summary>
    /// Browse favorites
    /// </summary>
    /// <param name="count">Number of items to load</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
