namespace Sonos.Base.Services;

public partial class SonosUpnpError
{
    public static readonly Dictionary<int, SonosUpnpError> DefaultErrors = new Dictionary<int, SonosUpnpError>
    {
        { 400, new SonosUpnpError(400, "Bad request") },
        { 401, new SonosUpnpError(401, "Invalid action") },
        { 402, new SonosUpnpError(402, "Invalid args") },
        { 404, new SonosUpnpError(404, "Invalid var") },
        { 412, new SonosUpnpError(412, "Precondition failed") },
        { 501, new SonosUpnpError(501, "Action failed") },
        { 600, new SonosUpnpError(600, "Argument value invalid") },
        { 601, new SonosUpnpError(601, "Argument value out of range") },
        { 602, new SonosUpnpError(602, "Optional action not implemented") },
        { 603, new SonosUpnpError(603, "Out of memory") },
        { 604, new SonosUpnpError(604, "Human intervention required") },
        { 605, new SonosUpnpError(605, "String argument too long") },
        { 606, new SonosUpnpError(606, "Action not authorized") },
        { 607, new SonosUpnpError(607, "Signature failure") },
        { 608, new SonosUpnpError(608, "Signature missing") },
        { 609, new SonosUpnpError(609, "Not encrypted") },
        { 610, new SonosUpnpError(610, "Invalid sequence") },
        { 611, new SonosUpnpError(611, "Invalid control URL") },
        { 612, new SonosUpnpError(612, "No such session") },
    };
}