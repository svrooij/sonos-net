using System;

namespace Sonos.Base.Music;

/// <summary>
/// Exception thrown when a SOAP Music API (SMAPI) operation fails
/// </summary>
public class SmapiException : SonosException
{
    public string? FaultCode { get; }
    public string? FaultString { get; }

    public RefreshAuthTokenResult? RefreshAuthResult { get; init; }

    public SmapiException(string? faultCode, string? faultString)
        : this(faultCode, faultString, null) { }

    public SmapiException(string? faultCode, string? faultString, RefreshAuthTokenResult? refreshAuthResult)
        : base($"SMAPI error {faultCode} - {faultString}")
    {
        FaultCode = faultCode;
        FaultString = faultString;
        RefreshAuthResult = refreshAuthResult;
    }
}