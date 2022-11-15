namespace Sonos.Base;
/// <summary>
/// Calls to sonos use this attribute to generate the correct xml
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class SonosServiceRequestAttribute : Attribute {
    /// <summary>
    /// Name of the Action to call, in the soapaction header and in the xml
    /// </summary>
    public string? Action { get; }

    /// <summary>
    /// SubPath for this service
    /// </summary>
    public string Path { get; }
    
    /// <summary>
    /// Name of the service, in the soapaction header and the xml
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// Create SonosServiceAttribute to mark a service call
    /// </summary>
    /// <param name="path">SubPath for this service</param>
    /// <param name="serviceName">Name of the service</param>
    /// <param name="action">Action to call</param>
    public SonosServiceRequestAttribute(string path, string serviceName, string? action = null) {
        Action = action;
        Path = path;
        ServiceName = serviceName;
    }

    /// <summary>
    /// Get SonosServiceRequestAttribute for some request type.
    /// </summary>
    /// <typeparam name="TInput">The Type to check</typeparam>
    /// <returns>SonosServiceRequestAttribute</returns>
    /// <exception cref="ArgumentException">Is thrown when the class does not have this attribute defined</exception>
    public static SonosServiceRequestAttribute GetSonosServiceRequestAttribute<TInput>() where TInput : class {
        var result = Attribute.GetCustomAttribute(typeof(TInput), typeof(SonosServiceRequestAttribute), false);
        if (result is null) {
            throw new ArgumentException("Type is not a sonos request");
        }
        return (SonosServiceRequestAttribute)result;
    }
}
