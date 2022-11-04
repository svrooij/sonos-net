namespace Sonos.Base.Services;

public class SonosServiceException : SonosException
{
    const string DefaultMessage = "Sonos service exception occurred";
    public string FaultCode { get; init; }
    public string FaultString { get; init; }
    public int? UpnpErrorCode { get; init; }
    public string? UpnpErrorMessage { get; init; }

    public SonosServiceException(string faultCode, string faultString, int? upnpErrorCode = null, string? upnpErrorMessage = null) : base(upnpErrorMessage ?? DefaultMessage)
    {
        FaultCode = faultCode;
        FaultString = faultString;
        UpnpErrorCode = upnpErrorCode;
        UpnpErrorMessage = upnpErrorMessage;
    }
}
