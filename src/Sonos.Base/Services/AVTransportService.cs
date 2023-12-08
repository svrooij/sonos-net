using Sonos.Base.Metadata;

namespace Sonos.Base.Services
{
    public partial class AVTransportService
    {
        /// <summary>
        /// Add a playlist from a music service to the queue
        /// </summary>
        /// <param name="musicServiceId">ID of the specific music service '9' for spotify</param>
        /// <param name="itemId">ID of the playlist</param>
        /// <param name="flags">No clue '8300' is used by official app</param>
        /// <param name="sn">(guess) account number to use, no clue where to get.</param>
        /// <param name="didlDescription">Use the default or 'SA_RINCON2311_X_#Svc2311-0-Token' for spotify in europe (not sure where to get). Check constants in `DidlDesc` </param>
        /// <param name="cancellationToken">CancellationToken to cancel the request if needed</param>
        /// <returns></returns>
        public Task<AddURIToQueueResponse> AddURIToQueuePlaylistAsync(int musicServiceId, string itemId, int flags = 8300, int sn = 2, string didlDescription = DidlDesc.SpotifyEurope, CancellationToken cancellationToken = default)
        {
            if (itemId == null) throw new ArgumentNullException(nameof(itemId));
            return AddURIToQueueAsync(new AddURIToQueueRequest
            {
                EnqueuedURI = $"x-rincon-cpcontainer:1006206c{itemId.Replace(":", "%3a")}?sid={musicServiceId}&flags={flags}&sn={sn}",
                EnqueuedURIMetaDataObject = Didl.GetMetadataForPlaylist(itemId, didlDescription),
                DesiredFirstTrackNumberEnqueued = 1,
                EnqueueAsNext = true,
            }, cancellationToken);
        }

        public Task<bool> SetAVTransportURIAsync(string trackUri, Didl? metadata, CancellationToken cancellationToken = default) => ExecuteRequestAsync(new SetAVTransportURIRequest { CurrentURI = trackUri, CurrentURIMetaDataObject = metadata, InstanceID = 0 }, cancellationToken, "SetAVTransportURI");

        /// <summary>
        /// Join another player
        /// </summary>
        /// <param name="otherPlayersUuid">UUID of the group coordinator</param>
        /// <param name="cancellationToken">CancellationToken to cancel the request if needed</param>
        /// <remarks>If the group you wish to join consists of several players be sure to pick the right one (the one on top)</remarks>
        public Task<bool> SetAVTransportURIToOtherPlayerAsync(string otherPlayersUuid, CancellationToken cancellationToken = default) => SetAVTransportURIAsync($"x-rincon:{otherPlayersUuid}", null, cancellationToken);

        /// <summary>
        /// Change your speaker to play a stream from a connected music service
        /// </summary>
        /// <param name="musicServiceId">ID of the specific music service '254' for tunein</param>
        /// <param name="itemId">ID of the item within this music service.</param>
        /// <param name="flags">No clue '8224' seems to be used a lot</param>
        /// <param name="sn">(guess) account number to use, no clue where to get.</param>
        /// <param name="didlDescription">Use the default or 'SA_RINCON2311_X_#Svc2311-0-Token' for spotify in europe (not sure where to get). Check constants in `DidlDesc` </param>
        /// <param name="cancellationToken">CancellationToken to cancel the request if needed</param>
        /// <remarks>This does not start playback, you need to start playback afterwards</remarks>
        public Task<bool> SetAVTransportURIToStreamItemAsync(int musicServiceId, string itemId, int flags = 8224, int sn = 0, string didlDescription = DidlDesc.Default, CancellationToken cancellationToken = default)
        {
            return SetAVTransportURIAsync($"x-sonosapi-stream:{itemId}?sid={musicServiceId}&flags={flags}&sn={sn}", Didl.GetMetadataForBroadcast(itemId, didlDescription), cancellationToken);
        }
        public Task<bool> PlayAsync(CancellationToken cancellationToken = default) => PlayAsync(new PlayRequest { Speed = "1" }, cancellationToken);

        [Obsolete("Use PlayAsync instead")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Task<bool> Play(CancellationToken cancellationToken = default) => PlayAsync(cancellationToken);
    }
}