namespace Sonos.Base;
/// <summary>
/// Calls to sonos use this attribute to generate the correct xml
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class MusicServiceRequestAttribute : Attribute
{
    /// <summary>
    /// Name of the Action to call, in the soapaction header and in the xml
    /// </summary>
    public string Action { get; }

    /// <summary>
    /// Create SonosServiceAttribute to mark a service call
    /// </summary>
    /// <param name="action">Action to call</param>
    public MusicServiceRequestAttribute(string? action = null)
    {
        Action = action;
    }

    /// <summary>
    /// Get SonosServiceRequestAttribute for some request type.
    /// </summary>
    /// <typeparam name="TInput">The Type to check</typeparam>
    /// <returns>SonosServiceRequestAttribute</returns>
    /// <exception cref="ArgumentException">Is thrown when the class does not have this attribute defined</exception>
    public static MusicServiceRequestAttribute GetMusicServiceRequestAttribute<TInput>() where TInput : class
    {
        var result = Attribute.GetCustomAttribute(typeof(TInput), typeof(MusicServiceRequestAttribute), false);
        if (result is null)
        {
            throw new ArgumentException("Type is not a music service request");
        }
        return (MusicServiceRequestAttribute)result;
    }
}
