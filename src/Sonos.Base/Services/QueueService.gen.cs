/*
 * Sonos-net QueueService
 *
 * File is generated by [@svrooij/sonos-docs](https://github.com/svrooij/sonos-api-docs/tree/main/generator/sonos-docs)
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

/// <summary>
/// QueueService  - Modify and browse queues
/// </summary>
public partial class QueueService : SonosBaseService<QueueService.IQueueEvent>
{
    /// <summary>
    /// Create a new QueueService
    /// </summary>
    /// <param name="options">Service options</param>
    public QueueService(SonosServiceOptions options) : base(SonosService.Queue, "/MediaRenderer/Queue/Control", "/MediaRenderer/Queue/Event", options) { }


    /// <summary>
    /// AddMultipleURIs
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>AddMultipleURIsResponse</returns>
    public Task<AddMultipleURIsResponse> AddMultipleURIs(AddMultipleURIsRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<AddMultipleURIsRequest, AddMultipleURIsResponse>(request, cancellationToken);

    /// <summary>
    /// AddURI
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>AddURIResponse</returns>
    public Task<AddURIResponse> AddURI(AddURIRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<AddURIRequest, AddURIResponse>(request, cancellationToken);

    /// <summary>
    /// AttachQueue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>AttachQueueResponse</returns>
    public Task<AttachQueueResponse> AttachQueue(AttachQueueRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<AttachQueueRequest, AttachQueueResponse>(request, cancellationToken);

    /// <summary>
    /// Backup
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> Backup(CancellationToken cancellationToken = default) => ExecuteRequest(new BaseRequest(), cancellationToken);

    /// <summary>
    /// Browse
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>BrowseResponse</returns>
    public Task<BrowseResponse> Browse(BrowseRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<BrowseRequest, BrowseResponse>(request, cancellationToken);

    /// <summary>
    /// CreateQueue
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>CreateQueueResponse</returns>
    public Task<CreateQueueResponse> CreateQueue(CreateQueueRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<CreateQueueRequest, CreateQueueResponse>(request, cancellationToken);

    /// <summary>
    /// RemoveAllTracks
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>RemoveAllTracksResponse</returns>
    public Task<RemoveAllTracksResponse> RemoveAllTracks(RemoveAllTracksRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RemoveAllTracksRequest, RemoveAllTracksResponse>(request, cancellationToken);

    /// <summary>
    /// RemoveTrackRange
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>RemoveTrackRangeResponse</returns>
    public Task<RemoveTrackRangeResponse> RemoveTrackRange(RemoveTrackRangeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<RemoveTrackRangeRequest, RemoveTrackRangeResponse>(request, cancellationToken);

    /// <summary>
    /// ReorderTracks
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>ReorderTracksResponse</returns>
    public Task<ReorderTracksResponse> ReorderTracks(ReorderTracksRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<ReorderTracksRequest, ReorderTracksResponse>(request, cancellationToken);

    /// <summary>
    /// ReplaceAllTracks
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>ReplaceAllTracksResponse</returns>
    public Task<ReplaceAllTracksResponse> ReplaceAllTracks(ReplaceAllTracksRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<ReplaceAllTracksRequest, ReplaceAllTracksResponse>(request, cancellationToken);

    /// <summary>
    /// SaveAsSonosPlaylist
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>SaveAsSonosPlaylistResponse</returns>
    public Task<SaveAsSonosPlaylistResponse> SaveAsSonosPlaylist(SaveAsSonosPlaylistRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SaveAsSonosPlaylistRequest, SaveAsSonosPlaylistResponse>(request, cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:Queue:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "AddMultipleURIs")]
    public class AddMultipleURIsRequest : BaseRequest
    {
        public int QueueID { get; set; }

        public int UpdateID { get; set; }

        public string ContainerURI { get; set; }

        public string ContainerMetaData { get; set; }

        /// <summary>
        /// Generate ContainerMetaData xml from Didl data
        /// </summary>
        public Metadata.Didl ContainerMetaDataObject
        {
            set
            {
                ContainerMetaData = Metadata.DidlSerializer.SerializeMetadata(value);
            }
        }

        public int DesiredFirstTrackNumberEnqueued { get; set; }

        public bool EnqueueAsNext { get; set; }

        public int NumberOfURIs { get; set; }

        public string EnqueuedURIsAndMetaData { get; set; }

        /// <summary>
        /// Generate EnqueuedURIsAndMetaData xml from Didl data
        /// </summary>
        public Metadata.Didl EnqueuedURIsAndMetaDataObject
        {
            set
            {
                EnqueuedURIsAndMetaData = Metadata.DidlSerializer.SerializeMetadata(value);
            }
        }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("AddMultipleURIsResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class AddMultipleURIsResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int FirstTrackNumberEnqueued { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NumTracksAdded { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewQueueLength { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "AddURI")]
    public class AddURIRequest : BaseRequest
    {
        public int QueueID { get; set; }

        public int UpdateID { get; set; }

        public string EnqueuedURI { get; set; }

        public string EnqueuedURIMetaData { get; set; }

        /// <summary>
        /// Generate EnqueuedURIMetaData xml from Didl data
        /// </summary>
        public Metadata.Didl EnqueuedURIMetaDataObject
        {
            set
            {
                EnqueuedURIMetaData = Metadata.DidlSerializer.SerializeMetadata(value);
            }
        }

        public int DesiredFirstTrackNumberEnqueued { get; set; }

        public bool EnqueueAsNext { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("AddURIResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class AddURIResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int FirstTrackNumberEnqueued { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NumTracksAdded { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewQueueLength { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "AttachQueue")]
    public class AttachQueueRequest : BaseRequest
    {
        public string QueueOwnerID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("AttachQueueResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class AttachQueueResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int QueueID { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string QueueOwnerContext { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "Browse")]
    public class BrowseRequest : BaseRequest
    {
        public int QueueID { get; set; }

        public int StartingIndex { get; set; }

        public int RequestedCount { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("BrowseResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class BrowseResponse
    {
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
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "CreateQueue")]
    public class CreateQueueRequest : BaseRequest
    {
        public string QueueOwnerID { get; set; }

        public string QueueOwnerContext { get; set; }

        public string QueuePolicy { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("CreateQueueResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class CreateQueueResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int QueueID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "RemoveAllTracks")]
    public class RemoveAllTracksRequest : BaseRequest
    {
        public int QueueID { get; set; }

        public int UpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("RemoveAllTracksResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class RemoveAllTracksResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "RemoveTrackRange")]
    public class RemoveTrackRangeRequest : BaseRequest
    {
        public int QueueID { get; set; }

        public int UpdateID { get; set; }

        public int StartingIndex { get; set; }

        public int NumberOfTracks { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("RemoveTrackRangeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class RemoveTrackRangeResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "ReorderTracks")]
    public class ReorderTracksRequest : BaseRequest
    {
        public int QueueID { get; set; }

        public int StartingIndex { get; set; }

        public int NumberOfTracks { get; set; }

        public int InsertBefore { get; set; }

        public int UpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("ReorderTracksResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class ReorderTracksResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "ReplaceAllTracks")]
    public class ReplaceAllTracksRequest : BaseRequest
    {
        public int QueueID { get; set; }

        public int UpdateID { get; set; }

        public string ContainerURI { get; set; }

        public string ContainerMetaData { get; set; }

        /// <summary>
        /// Generate ContainerMetaData xml from Didl data
        /// </summary>
        public Metadata.Didl ContainerMetaDataObject
        {
            set
            {
                ContainerMetaData = Metadata.DidlSerializer.SerializeMetadata(value);
            }
        }

        public int CurrentTrackIndex { get; set; }

        public string NewCurrentTrackIndices { get; set; }

        public int NumberOfURIs { get; set; }

        public string EnqueuedURIsAndMetaData { get; set; }

        /// <summary>
        /// Generate EnqueuedURIsAndMetaData xml from Didl data
        /// </summary>
        public Metadata.Didl EnqueuedURIsAndMetaDataObject
        {
            set
            {
                EnqueuedURIsAndMetaData = Metadata.DidlSerializer.SerializeMetadata(value);
            }
        }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("ReplaceAllTracksResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class ReplaceAllTracksResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewQueueLength { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int NewUpdateID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/MediaRenderer/Queue/Control", "Queue", "SaveAsSonosPlaylist")]
    public class SaveAsSonosPlaylistRequest : BaseRequest
    {
        public int QueueID { get; set; }

        public string Title { get; set; }

        public string ObjectID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("SaveAsSonosPlaylistResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:Queue:1")]
    public partial class SaveAsSonosPlaylistResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AssignedObjectID { get; set; }
    }

    /// <summary>
    /// Queue is set to might emit these properties in events
    /// </summary>
    public partial interface IQueueEvent : IServiceEvent
    {
        public bool? Curated { get; }

        public string? LastChange { get; }

        public int? UpdateID { get; }
    }
}
