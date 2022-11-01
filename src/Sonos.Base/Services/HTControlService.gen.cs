namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// HTControlService  - Service related to the TV remote control
/// </summary>
public partial class HTControlService : SonosBaseService
{
    /// <summary>
    /// Create a new HTControlService
    /// </summary>
    /// <param name="sonosUri">Base URL of the speaker</param>
    /// <param name="httpClient">Optionally, a custom HttpClient.</param>
    public HTControlService(SonosServiceOptions options) : base("HTControl", "/HTControl/Control", "/HTControl/Event", options) { }


    /// <summary>
    /// CommitLearnedIRCodes
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> CommitLearnedIRCodes(CommitLearnedIRCodesRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<CommitLearnedIRCodesRequest>("CommitLearnedIRCodes", request, cancellationToken);

    /// <summary>
    /// GetIRRepeaterState
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetIRRepeaterStateResponse</returns>
    public Task<GetIRRepeaterStateResponse> GetIRRepeaterState(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetIRRepeaterStateResponse>("GetIRRepeaterState", new BaseRequest(), cancellationToken);

    /// <summary>
    /// GetLEDFeedbackState
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetLEDFeedbackStateResponse</returns>
    public Task<GetLEDFeedbackStateResponse> GetLEDFeedbackState(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, GetLEDFeedbackStateResponse>("GetLEDFeedbackState", new BaseRequest(), cancellationToken);

    /// <summary>
    /// IdentifyIRRemote
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> IdentifyIRRemote(IdentifyIRRemoteRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<IdentifyIRRemoteRequest>("IdentifyIRRemote", request, cancellationToken);

    /// <summary>
    /// IsRemoteConfigured
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>IsRemoteConfiguredResponse</returns>
    public Task<IsRemoteConfiguredResponse> IsRemoteConfigured(CancellationToken cancellationToken = default) => ExecuteRequest<BaseRequest, IsRemoteConfiguredResponse>("IsRemoteConfigured", new BaseRequest(), cancellationToken);

    /// <summary>
    /// LearnIRCode
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> LearnIRCode(LearnIRCodeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<LearnIRCodeRequest>("LearnIRCode", request, cancellationToken);

    /// <summary>
    /// SetIRRepeaterState
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetIRRepeaterState(SetIRRepeaterStateRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetIRRepeaterStateRequest>("SetIRRepeaterState", request, cancellationToken);

    /// <summary>
    /// SetLEDFeedbackState
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetLEDFeedbackState(SetLEDFeedbackStateRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetLEDFeedbackStateRequest>("SetLEDFeedbackState", request, cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:HTControl:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class CommitLearnedIRCodesRequest : BaseRequest
    {

        public string Name { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetIRRepeaterStateResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:HTControl:1")]
    public partial class GetIRRepeaterStateResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentIRRepeaterState { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetLEDFeedbackStateResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:HTControl:1")]
    public partial class GetLEDFeedbackStateResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string LEDFeedbackState { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class IdentifyIRRemoteRequest : BaseRequest
    {

        public int Timeout { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("IsRemoteConfiguredResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:HTControl:1")]
    public partial class IsRemoteConfiguredResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool RemoteConfigured { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class LearnIRCodeRequest : BaseRequest
    {

        public string IRCode { get; set; }

        public int Timeout { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetIRRepeaterStateRequest : BaseRequest
    {

        public string DesiredIRRepeaterState { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetLEDFeedbackStateRequest : BaseRequest
    {

        public string LEDFeedbackState { get; set; }
    }
}
