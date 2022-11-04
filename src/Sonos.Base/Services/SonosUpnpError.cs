namespace Sonos.Base.Services;

public partial class SonosUpnpError
{
    public SonosUpnpError(int code, string message)
    {
        Code = code;
        Message = message;
    }
    public int Code { get; init; }
    public string Message { get; init; }
}

internal static class SonosUpnpErrorDictionaryExtensions
{
    internal static Dictionary<int, SonosUpnpError> Merge(this Dictionary<int, SonosUpnpError> input, Dictionary<int, SonosUpnpError> additionalErrors)
    {
        foreach (var item in additionalErrors)
        {
            input[item.Key] = item.Value;
        }
        return input;
    }
}
