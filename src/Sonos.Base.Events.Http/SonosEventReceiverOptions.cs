namespace Sonos.Base.Events.Http;

public class SonosEventReceiverOptions
{
    /// <summary>
    /// Used to compute the event callback url
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// The port where to listen to, used to compute the callback url
    /// </summary>
    public int Port { get; set; } = 6329;

}
