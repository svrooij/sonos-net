/*
 * Sonos-net AudioInService
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
/// AudioInService  - Control line in
/// </summary>
public partial class AudioInService : SonosBaseService<AudioInService.IAudioInEvent>
{
    /// <summary>
    /// Create a new AudioInService
    /// </summary>
    /// <param name="options">Service options</param>
    public AudioInService(SonosServiceOptions options) : base(SonosService.AudioIn, "/AudioIn/Control", "/AudioIn/Event", options) { }


    /// <summary>
    /// GetAudioInputAttributes
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>GetAudioInputAttributesResponse</returns>
    public Task<GetAudioInputAttributesResponse> GetAudioInputAttributesAsync(CancellationToken cancellationToken = default) => ExecuteRequestAsync<BaseRequest, GetAudioInputAttributesResponse>(new BaseRequest(), cancellationToken, "GetAudioInputAttributes");

    /// <summary>
    /// GetLineInLevel
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>GetLineInLevelResponse</returns>
    public Task<GetLineInLevelResponse> GetLineInLevelAsync(CancellationToken cancellationToken = default) => ExecuteRequestAsync<BaseRequest, GetLineInLevelResponse>(new BaseRequest(), cancellationToken, "GetLineInLevel");

    /// <summary>
    /// SelectAudio
    /// </summary>
    /// <param name="request"><see cref="SelectAudioRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>Success boolean</returns>
    public Task<bool> SelectAudioAsync(SelectAudioRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync(request, cancellationToken, "SelectAudio");

    /// <summary>
    /// SetAudioInputAttributes
    /// </summary>
    /// <param name="request"><see cref="SetAudioInputAttributesRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetAudioInputAttributesAsync(SetAudioInputAttributesRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync(request, cancellationToken, "SetAudioInputAttributes");

    /// <summary>
    /// SetLineInLevel
    /// </summary>
    /// <param name="request"><see cref="SetLineInLevelRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetLineInLevelAsync(SetLineInLevelRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync(request, cancellationToken, "SetLineInLevel");

    /// <summary>
    /// StartTransmissionToGroup
    /// </summary>
    /// <param name="request"><see cref="StartTransmissionToGroupRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>StartTransmissionToGroupResponse</returns>
    public Task<StartTransmissionToGroupResponse> StartTransmissionToGroupAsync(StartTransmissionToGroupRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync<StartTransmissionToGroupRequest, StartTransmissionToGroupResponse>(request, cancellationToken, "StartTransmissionToGroup");

    /// <summary>
    /// StopTransmissionToGroup
    /// </summary>
    /// <param name="request"><see cref="StopTransmissionToGroupRequest"/> payload</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
    /// <returns>Success boolean</returns>
    public Task<bool> StopTransmissionToGroupAsync(StopTransmissionToGroupRequest request, CancellationToken cancellationToken = default) => ExecuteRequestAsync(request, cancellationToken, "StopTransmissionToGroup");

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/AudioIn/Control", "AudioIn")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:AudioIn:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetAudioInputAttributesResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AudioIn:1")]
    public partial class GetAudioInputAttributesResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentName { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentIcon { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetLineInLevelResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AudioIn:1")]
    public partial class GetLineInLevelResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentLeftLineInLevel { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public int CurrentRightLineInLevel { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/AudioIn/Control", "AudioIn", "SelectAudio")]
    public class SelectAudioRequest : BaseRequest
    {
        public string ObjectID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/AudioIn/Control", "AudioIn", "SetAudioInputAttributes")]
    public class SetAudioInputAttributesRequest : BaseRequest
    {
        public string DesiredName { get; set; }

        public string DesiredIcon { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/AudioIn/Control", "AudioIn", "SetLineInLevel")]
    public class SetLineInLevelRequest : BaseRequest
    {
        public int DesiredLeftLineInLevel { get; set; }

        public int DesiredRightLineInLevel { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/AudioIn/Control", "AudioIn", "StartTransmissionToGroup")]
    public class StartTransmissionToGroupRequest : BaseRequest
    {
        public string CoordinatorID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("StartTransmissionToGroupResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:AudioIn:1")]
    public partial class StartTransmissionToGroupResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentTransportSettings { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    [SonosServiceRequest("/AudioIn/Control", "AudioIn", "StopTransmissionToGroup")]
    public class StopTransmissionToGroupRequest : BaseRequest
    {
        public string CoordinatorID { get; set; }
    }

    /// <summary>
    /// AudioIn is set to might emit these properties in events
    /// </summary>
    public partial interface IAudioInEvent : IServiceEvent
    {
        public string? AudioInputName { get; }

        public string? Icon { get; }

        public int? LeftLineInLevel { get; }

        public bool? LineInConnected { get; }

        public bool? Playing { get; }

        public int? RightLineInLevel { get; }
    }

    /// <summary>
    /// GetAudioInputAttributes
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetAudioInputAttributesResponse</returns>
    [Obsolete("This method is obsolete. Use GetAudioInputAttributesAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<GetAudioInputAttributesResponse> GetAudioInputAttributes(CancellationToken cancellationToken = default) => GetAudioInputAttributesAsync(cancellationToken);

    /// <summary>
    /// GetLineInLevel
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetLineInLevelResponse</returns>
    [Obsolete("This method is obsolete. Use GetLineInLevelAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<GetLineInLevelResponse> GetLineInLevel(CancellationToken cancellationToken = default) => GetLineInLevelAsync(cancellationToken);

    /// <summary>
    /// SelectAudio
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    [Obsolete("This method is obsolete. Use SelectAudioAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<bool> SelectAudio(SelectAudioRequest request, CancellationToken cancellationToken = default) => SelectAudioAsync(request, cancellationToken);

    /// <summary>
    /// SetAudioInputAttributes
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    [Obsolete("This method is obsolete. Use SetAudioInputAttributesAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<bool> SetAudioInputAttributes(SetAudioInputAttributesRequest request, CancellationToken cancellationToken = default) => SetAudioInputAttributesAsync(request, cancellationToken);

    /// <summary>
    /// SetLineInLevel
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    [Obsolete("This method is obsolete. Use SetLineInLevelAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<bool> SetLineInLevel(SetLineInLevelRequest request, CancellationToken cancellationToken = default) => SetLineInLevelAsync(request, cancellationToken);

    /// <summary>
    /// StartTransmissionToGroup
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>StartTransmissionToGroupResponse</returns>
    [Obsolete("This method is obsolete. Use StartTransmissionToGroupAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<StartTransmissionToGroupResponse> StartTransmissionToGroup(StartTransmissionToGroupRequest request, CancellationToken cancellationToken = default) => StartTransmissionToGroupAsync(request, cancellationToken);

    /// <summary>
    /// StopTransmissionToGroup
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    [Obsolete("This method is obsolete. Use StopTransmissionToGroupAsync instead.")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Task<bool> StopTransmissionToGroup(StopTransmissionToGroupRequest request, CancellationToken cancellationToken = default) => StopTransmissionToGroupAsync(request, cancellationToken);
}
