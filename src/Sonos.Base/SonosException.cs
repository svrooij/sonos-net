namespace Sonos.Base;

[System.Serializable]
public class SonosException : Exception
{
    public SonosException() { }
    public SonosException(string message) : base(message) { }
    //public SonosException(string message, Exception inner) : base(message, inner) { }
    //protected SonosException(
    //  System.Runtime.Serialization.SerializationInfo info,
    //  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
