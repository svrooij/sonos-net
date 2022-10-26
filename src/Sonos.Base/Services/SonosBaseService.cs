namespace Sonos.Base.Services;

using Sonos.Base.Soap;
using System.Threading;
using System.Threading.Tasks;
public class SonosBaseService {
  private readonly string ControlPath;
  private readonly string EventPath;
  private readonly string ServiceName;
  private readonly Uri BaseUri;
  private readonly HttpClient httpClient;

  internal SonosBaseService(string serviceName, string controlPath, string eventPath, Uri sonosUri, HttpClient? httpClient) {
    this.ControlPath = controlPath;
    this.EventPath = eventPath;
    this.ServiceName = serviceName;
    this.BaseUri = sonosUri;
    this.httpClient = httpClient ?? new HttpClient();
  }

  internal async Task<bool> ExecuteRequest<TPayload>(string action, TPayload payload, CancellationToken cancellationToken) {
    var request = SoapFactory.CreateRequest(this.BaseUri, this.ControlPath, this.ServiceName, action, payload);
    var response = await httpClient.SendAsync(request, cancellationToken);
    if(!response.IsSuccessStatusCode) {
      await HandleErrorResponse(response, cancellationToken);
    }
    return true;
  }

  internal async Task<TOut> ExecuteRequest<TPayload,TOut>(string action, TPayload payload,CancellationToken cancellationToken) {
    var request = SoapFactory.CreateRequest(this.BaseUri, this.ControlPath, this.ServiceName, action, payload);
    var response = await httpClient.SendAsync(request, cancellationToken);
    if(!response.IsSuccessStatusCode) {
      await HandleErrorResponse(response, cancellationToken);
    }
    return await ParseResponse<TOut>(response, cancellationToken);
  }

  internal async Task HandleErrorResponse(HttpResponseMessage response, CancellationToken cancellationToken) {
    // TODO HandleErrorResponse needs implementation
    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
    throw new Exception();
  }

  // internal async Task<TOut> ParseResponse<TOut>(HttpResponseMessage response, CancellationToken cancellationToken) {
  //   var xml = await response.Content.ReadAsStringAsync(cancellationToken);
  //   return SoapFactory.ParseXml<TOut>(this.ServiceName, xml);
  // }

  internal async Task<TOut> ParseResponse<TOut>(HttpResponseMessage response, CancellationToken cancellationToken) {
    using var xml = await response.Content.ReadAsStreamAsync(cancellationToken);
    return SoapFactory.ParseXml<TOut>(this.ServiceName, xml);
  }
}
