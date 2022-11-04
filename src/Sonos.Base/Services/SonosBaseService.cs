namespace Sonos.Base.Services;

using Sonos.Base.Soap;
using System.Threading;
using System.Threading.Tasks;

public class SonosBaseService
{
    private readonly string ControlPath;
    private readonly string EventPath;
    private readonly string ServiceName;
    private readonly Uri BaseUri;
    private readonly HttpClient httpClient;

    internal SonosBaseService(string serviceName, string controlPath, string eventPath, SonosServiceOptions options)
    {
        this.ControlPath = controlPath;
        this.EventPath = eventPath;
        this.ServiceName = serviceName;
        this.BaseUri = options.DeviceUri;
        this.httpClient = options.HttpClient ?? new HttpClient();
    }

    internal async Task<bool> ExecuteRequest<TPayload>(string action, TPayload payload, CancellationToken cancellationToken)
    {
        var request = SoapFactory.CreateRequest(this.BaseUri, this.ControlPath, this.ServiceName, action, payload);
        var response = await httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponse(response, cancellationToken);
        }
        return true;
    }

    internal async Task<TOut> ExecuteRequest<TPayload, TOut>(string action, TPayload payload, CancellationToken cancellationToken)
    {
        var request = SoapFactory.CreateRequest(this.BaseUri, this.ControlPath, this.ServiceName, action, payload);
        var response = await httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponse(response, cancellationToken);
        }
        return await ParseResponse<TOut>(response, cancellationToken);
    }

    internal async Task HandleErrorResponse(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        // TODO HandleErrorResponse needs implementation
        // var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
        using var errorContent = await response.Content.ReadAsStreamAsync(cancellationToken);
        var error = SoapFactory.ParseFaultXml(errorContent);
        if (error is not null)
        {
            var code = error.UpnpErrorCode;
            string? message = code is not null ? ServiceErrors[(int)code].Message : null;
            throw new SonosServiceException(error.FaultCode, error.FaultString, code, message);
        }
        throw new Exception();
    }

    internal async Task<TOut> ParseResponse<TOut>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        using var xml = await response.Content.ReadAsStreamAsync(cancellationToken);
        return SoapFactory.ParseXml<TOut>(this.ServiceName, xml);
    }

    internal virtual Dictionary<int, SonosUpnpError> ServiceErrors { get => SonosUpnpError.DefaultErrors; }
}