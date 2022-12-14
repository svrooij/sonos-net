namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// VirtualLineInService 
/// </summary>
public partial class VirtualLineInService : SonosBaseService
{
    /// <summary>
    /// Create a new VirtualLineInService
    /// </summary>
    /// <param name="sonosUri">Base URL of the speaker</param>
    /// <param name="httpClient">Optionally, a custom HttpClient.</param>
    public VirtualLineInService(SonosServiceOptions options) : base("VirtualLineIn", "/MediaRenderer/VirtualLineIn/Control", "/MediaRenderer/VirtualLineIn/Event", options) { }


    /// <summary>
    /// Next
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> Next(CancellationToken cancellationToken = default) => ExecuteRequest<NextRequest>("Next", new NextRequest(), cancellationToken);

    /// <summary>
    /// Pause
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> Pause(CancellationToken cancellationToken = default) => ExecuteRequest<PauseRequest>("Pause", new PauseRequest(), cancellationToken);

    /// <summary>
    /// Play
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> Play(PlayRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<PlayRequest>("Play", request, cancellationToken);

    /// <summary>
    /// Previous
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> Previous(CancellationToken cancellationToken = default) => ExecuteRequest<PreviousRequest>("Previous", new PreviousRequest(), cancellationToken);

    /// <summary>
    /// SetVolume
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetVolume(SetVolumeRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<SetVolumeRequest>("SetVolume", request, cancellationToken);

    /// <summary>
    /// StartTransmission
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>StartTransmissionResponse</returns>
    public Task<StartTransmissionResponse> StartTransmission(StartTransmissionRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<StartTransmissionRequest, StartTransmissionResponse>("StartTransmission", request, cancellationToken);

    /// <summary>
    /// Stop
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> Stop(CancellationToken cancellationToken = default) => ExecuteRequest<StopRequest>("Stop", new StopRequest(), cancellationToken);

    /// <summary>
    /// StopTransmission
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> StopTransmission(StopTransmissionRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<StopTransmissionRequest>("StopTransmission", request, cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BaseRequest
    {
        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:VirtualLineIn:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class NextRequest : BaseRequest
    {

        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class PauseRequest : BaseRequest
    {

        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class PlayRequest : BaseRequest
    {

        public int InstanceID { get; set; } = 0;

        public string Speed { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class PreviousRequest : BaseRequest
    {

        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetVolumeRequest : BaseRequest
    {

        public int InstanceID { get; set; } = 0;

        public int DesiredVolume { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class StartTransmissionRequest : BaseRequest
    {

        public int InstanceID { get; set; } = 0;

        public string CoordinatorID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("StartTransmissionResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:VirtualLineIn:1")]
    public partial class StartTransmissionResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string CurrentTransportSettings { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class StopRequest : BaseRequest
    {

        public int InstanceID { get; set; } = 0;
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class StopTransmissionRequest : BaseRequest
    {

        public int InstanceID { get; set; } = 0;

        public string CoordinatorID { get; set; }
    }
}
