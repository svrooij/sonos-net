namespace Sonos.Base;
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class SonosServiceRequestAttribute : Attribute {
    public string? Action { get; }
    public string Path { get; }
    public string ServiceName { get; }

    internal SonosServiceRequestAttribute(string path, string serviceName, string? action = null) {
        Action = action;
        Path = path;
        ServiceName = serviceName;
    }

    internal static SonosServiceRequestAttribute GetSonosServiceRequestAttribute<TInput>(){
        var result = Attribute.GetCustomAttribute(typeof(TInput), typeof(SonosServiceRequestAttribute), false);
        if (result is null) {
            throw new ArgumentException("Type is not a sonos request");
        }
        return (SonosServiceRequestAttribute)result;
    }
}
