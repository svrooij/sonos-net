namespace Sonos.Base.Services;

public class SonosServiceOptions
{
    public Uri DeviceUri { get; set; }
    public HttpClient? HttpClient { get; set; }
}
