using Sonos.Base.Services;

namespace Sonos.Web.SonosServices;

internal static class SonosResults
{
    internal static IResult DeviceNotFoundResult(string speakerId) =>
        Results.Problem(title: "Speaker Not Found", detail: $"Speaker with ID {speakerId} not found.", statusCode: 404);

    internal static IResult ServiceExceptionResult(SonosServiceException ex, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "") =>
        Results.Problem(
                type: "https://sonos.svrooij.io/services/#upnp-errors",
                title: "Sonos Service Error",
                detail: $"{memberName} caused an upnp error ({ex.UpnpErrorCode}): {ex.UpnpErrorMessage}",
                statusCode: 409,
                extensions: [new(nameof(ex.UpnpErrorCode), ex.UpnpErrorCode)]);
}
