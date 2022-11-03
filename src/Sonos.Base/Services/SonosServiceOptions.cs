namespace Sonos.Base.Services;

public class SonosServiceOptions
{
    /// <summary>
    /// Base URI of the device, eg. http://ip:1400/
    /// </summary>
    public Uri DeviceUri { get; set; }

    /// <summary>
    /// Pre-configured HTTP Client
    /// </summary>
    public HttpClient? HttpClient { get; set; }
}
