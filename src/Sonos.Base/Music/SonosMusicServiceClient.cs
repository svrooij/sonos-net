using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

using Sonos.Base.Music.Models;
using Sonos.Base.Services;
using static Sonos.Base.Services.MusicServicesService;

namespace Sonos.Base.Music;

public class SonosMusicServiceClient : IDisposable, IAsyncDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly SonosMusicServiceClientOptions _options;
    private string? _currentAuthToken;
    private string? _currentKey;

    public SonosMusicServiceClient(HttpClient httpClient, ILogger logger, SonosMusicServiceClientOptions options)
    {
        _httpClient = httpClient;
        _logger = logger;
        _options = options;
        _currentAuthToken = options.AuthToken;
        _currentKey = options.Key;

        _httpClient.BaseAddress = options.BaseUri;
    }

    public string? Udn => _options.UDN;
    public int? SerialNumber => _options.SerialNumber;

    /// <summary>
    /// Get the login link for this music service to connect it to your Sonos system.
    /// </summary>
    public async Task<DeviceLink?> GetLoginLinkAsync(CancellationToken cancellationToken = default)
    {
        if (_options.Authentication == MusicServiceAuthentication.AppLink)
        {
            var appLink = await GetAppLinkAsync(cancellationToken);
            // Use GetAppLinkResult on the response to access AuthorizeAccount
            if (appLink.GetAppLinkResult?.AuthorizeAccount != null)
            {
                return appLink.GetAppLinkResult.AuthorizeAccount.DeviceLink;
            }
        }

        if (_options.Authentication == MusicServiceAuthentication.DeviceLink)
        {
            return await GetDeviceLinkCodeAsync(cancellationToken);
        }

        _logger.LogWarning("GetLoginLinkAsync is only supported for DeviceLink or AppLink authentication types.");
        return null;
    }

    /// <summary>
    /// Get the AppLink for this music service, to connect it to your Sonos system.
    /// Only for services with Auth == 'AppLink'
    /// </summary>
    public async Task<GetAppLinkResponse> GetAppLinkAsync(CancellationToken cancellationToken = default)
    {
        var request = new GetAppLinkRequest
        {
            HouseholdId = _options.HouseholdId ?? throw new ArgumentException("HouseholdId is required for AppLink")
        };

        return await SoapRequestWithBodyAsync<GetAppLinkRequest, GetAppLinkResponse>("getAppLink", request, cancellationToken);
    }

    /// <summary>
    /// Get credentials for the remote service, you'll need the linkcode from GetAppLink
    /// </summary>
    /// <remarks>This will also save the credentials for next time.</remarks>
    public async Task<DeviceAuthResponse> GetDeviceAuthTokenAsync(string linkCode, CancellationToken cancellationToken = default)
    {
        var request = new GetDeviceAuthTokenRequest
        {
            HouseholdId = _options.HouseholdId!,
            LinkCode = linkCode,
            LinkDeviceId = _options.HouseholdId
        };

        var response = await SoapRequestWithBodyAsync<GetDeviceAuthTokenRequest, DeviceAuthResponse>("getDeviceAuthToken", request, cancellationToken);

        // Save new tokens if callback is provided
        if (_options.SaveNewToken != null)
        {
            await _options.SaveNewToken(_options.Id, response.PrivateKey, response.AuthToken);
        }

        // Update current tokens
        _currentKey = response.PrivateKey;
        _currentAuthToken = response.AuthToken;

        return response;
    }

    /// <summary>
    /// Get device link code for DeviceLink authentication
    /// </summary>
    public async Task<DeviceLink> GetDeviceLinkCodeAsync(CancellationToken cancellationToken = default)
    {
        var request = new GetDeviceLinkCodeRequest
        {
            HouseholdId = _options.HouseholdId!
        };

        return await SoapRequestWithBodyAsync<GetDeviceLinkCodeRequest, DeviceLink>("getDeviceLinkCode", request, cancellationToken);
    }

    /// <summary>
    /// This is the main entrypoint for the external Music service.
    /// You can browse lists, sometimes referring to other lists.
    /// </summary>
    public async Task<MediaList> GetMetadataAsync(string id, int index = 0, int count = 100, bool recursive = false, CancellationToken cancellationToken = default)
    {
        var request = new GetMetadataRequest
        {
            Id = id,
            Index = index,
            Count = count,
            Recursive = recursive
        };

        var response = await SoapRequestWithBodyAsync<GetMetadataRequest, GetMetadataResponse>("getMetadata", request, cancellationToken);
        return PostProcessMediaResult(response);
    }

    /// <summary>
    /// Get extended metadata for a media item
    /// </summary>
    public async Task<MediaList> GetExtendedMetadataAsync(string id, CancellationToken cancellationToken = default)
    {
        var request = new GetExtendedMetadataRequest { Id = id };
        var response = await SoapRequestWithBodyAsync<GetExtendedMetadataRequest, GetMetadataResponse>("getExtendedMetadata", request, cancellationToken);
        return PostProcessMediaResult(response);
    }

    /// <summary>
    /// Get media metadata for a specific item
    /// </summary>
    public async Task<GetMediaMetadataResponse> GetMediaMetadataAsync(string id, CancellationToken cancellationToken = default)
    {
        var request = new GetMediaMetadataRequest { Id = id };
        return await SoapRequestWithBodyAsync<GetMediaMetadataRequest, GetMediaMetadataResponse>("getMediaMetadata", request, cancellationToken);
    }

    /// <summary>
    /// Get the media URI for playback
    /// </summary>
    public async Task<MediaUri> GetMediaUriAsync(string id, CancellationToken cancellationToken = default)
    {
        var request = new GetMediaUriRequest { Id = id };
        return await SoapRequestWithBodyAsync<GetMediaUriRequest, MediaUri>("getMediaURI", request, cancellationToken);
    }

    /// <summary>
    /// Search for content in the music service
    /// </summary>
    public async Task<MediaList> SearchAsync(string id, string term, int index = 0, int count = 100, CancellationToken cancellationToken = default)
    {
        var request = new SearchRequest
        {
            Id = id,
            Term = term,
            Index = index,
            Count = count
        };

        var response = await SoapRequestWithBodyAsync<SearchRequest, GetMetadataResponse>("search", request, cancellationToken);
        return PostProcessMediaResult(response);
    }

    private MediaList PostProcessMediaResult(GetMetadataResponse input)
    {
        var result = new MediaList
        {
            Index = input.MetadataResult.Index,
            Count = input.MetadataResult.Count,
            Total = input.MetadataResult.Total,
            MediaMetadata = input.MetadataResult.MediaMetadata?.Select(ProcessMediaMetadata).ToArray(),
            MediaCollection = input.MetadataResult.MediaCollection
        };

        return result;
    }

    private MediaMetadata ProcessMediaMetadata(MediaMetadata metadata)
    {
        // Add track URI for streams
        if (metadata.ItemType == "stream")
        {
            metadata.TrackUri = $"x-sonosapi-stream:{metadata.Id}?sid={_options.Id}";
        }

        return metadata;
    }

    private async Task<TResponse> SoapRequestWithBodyAsync<TRequest, TResponse>(
        string action, 
        TRequest requestBody, 
        CancellationToken cancellationToken,
        bool isRetry = false,
        [CallerMemberName] string? caller = null)
        where TRequest : class
        where TResponse : class
    {
        _logger.LogDebug("Executing SOAP action {Action} for service {ServiceName} called from {Caller}", 
            action, _options.Name, caller);

        try
        {
            return await HandleRequestAndParseResponseAsync<TRequest, TResponse>(requestBody, action, cancellationToken);
        }
        catch (SmapiException ex) when (ex.RefreshAuthResult is not null && !isRetry)
        {
            _logger.LogInformation("Token refresh required, retrying with new credentials");
            _currentAuthToken = ex.RefreshAuthResult.AuthToken;
            _currentKey = ex.RefreshAuthResult.PrivateKey;
            // Save new tokens
            if (_options.SaveNewToken != null)
            {
                await _options.SaveNewToken(_options.Id, ex.RefreshAuthResult.PrivateKey!, ex.RefreshAuthResult.AuthToken!);
            }

            return await SoapRequestWithBodyAsync<TRequest, TResponse>(action, requestBody, cancellationToken, true, caller);
        }
    }

    private async Task<TResponse> HandleRequestAndParseResponseAsync<TRequest, TResponse>(
        TRequest requestBody,
        string action,
        CancellationToken cancellationToken)
        where TRequest : class
        where TResponse : class
    {
        var soapEnvelope = GenerateRequestBody(action, requestBody);
        var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");

        var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress)
        {
            Content = content
        };

        request.Headers.Add("SOAPAction", MessageAction(action));

        var response = await _httpClient.SendAsync(request, cancellationToken);
        //using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        //return SoapParser.ParseXml<TResponse>(responseStream);

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        return SoapParser.ParseXml<TResponse>(responseString);
    }

    //private async Task HandleSoapFaultAsync(XElement fault, string action)
    //{
    //    var faultCode = fault.Element("faultcode")?.Value ?? "Unknown";
    //    var faultString = fault.Element("faultstring")?.Value ?? "SOAP Fault occurred";
        
    //    // Check for token refresh
    //    if (faultString == "tokenRefreshRequired")
    //    {
    //        var detail = fault.Element("detail");
    //        var refreshResult = detail?.Element("refreshAuthTokenResult");
            
    //        if (refreshResult != null)
    //        {
    //            var newAuthToken = refreshResult.Element("authToken")?.Value;
    //            var newPrivateKey = refreshResult.Element("privateKey")?.Value;
                
    //            if (!string.IsNullOrEmpty(newAuthToken) && !string.IsNullOrEmpty(newPrivateKey))
    //            {
    //                _logger.LogDebug("Saving new tokens from fault response");
    //                _currentAuthToken = newAuthToken;
    //                _currentKey = newPrivateKey;
                    
    //                if (_options.SaveNewToken != null)
    //                {
    //                    await _options.SaveNewToken(_options.Id, newPrivateKey, newAuthToken);
    //                }
    //            }
    //        }
    //    }

    //    throw new SmapiException(_options.Name, action, faultCode, faultString);
    //}

    private string MessageAction(string action)
    {
        return $"\"http://www.sonos.com/Services/1.1#{action}\"";
    }

    private string GenerateRequestBody<TRequest>(string action, TRequest body) where TRequest : class
    {
        var soapHeader = GenerateSoapHeader();
        var soapBody = GenerateSoapBody(action, body);
        return GenerateSoapEnvelope(soapHeader, soapBody);
    }

    private string GenerateSoapEnvelope(string header, string body)
    {
        return $"""
            <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:s="http://www.sonos.com/Services/1.1">
            {header}
            {body}
            </soap:Envelope>
            """;
    }

    private string GenerateSoapHeader()
    {
        var context = GenerateContextHeader(_options.Timezone ?? "+01:00");
        var credentials = GenerateCredentialHeader();
        
        return $"""
            <soap:Header>
            {context}
            {credentials}
            </soap:Header>
            """;
    }

    private string GenerateContextHeader(string timezone)
    {
        return $"""
              <s:context>
                <s:timeZone>{timezone}</s:timeZone>
              </s:context>
            """;
    }

    private string GenerateCredentialHeader()
    {
        var sb = new StringBuilder();
        sb.AppendLine("  <s:credentials>");
        
        if (!string.IsNullOrEmpty(_options.HouseholdId))
        {
            sb.AppendLine($"    <s:deviceId>{_options.HouseholdId}</s:deviceId>");
        }
        
        sb.AppendLine("    <s:deviceProvider>Sonos</s:deviceProvider>");
        
        if (_options.Authentication == MusicServiceAuthentication.DeviceLink || 
            _options.Authentication == MusicServiceAuthentication.AppLink)
        {
            sb.AppendLine("    <s:loginToken>");
            sb.AppendLine($"      <s:token>{_currentAuthToken ?? ""}</s:token>");
            sb.AppendLine($"      <s:key>{_currentKey ?? ""}</s:key>");
            sb.AppendLine("    </s:loginToken>");
        }
        
        sb.AppendLine("  </s:credentials>");
        return sb.ToString();
    }

    private string GenerateSoapBody<TRequest>(string action, TRequest body) where TRequest : class
    {
        var sb = new StringBuilder();
        sb.AppendLine("<soap:Body>");
        sb.AppendLine($"<s:{action}>");
        
        if (body != null)
        {
            var properties = typeof(TRequest).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(body);
                if (value != null)
                {
                    var elementName = char.ToLowerInvariant(prop.Name[0]) + prop.Name[1..];
                    var elementValue = value is bool boolValue ? (boolValue ? "1" : "0") : value.ToString();
                    sb.AppendLine($"<s:{elementName}>{elementValue}</s:{elementName}>");
                }
            }
        }
        
        sb.AppendLine($"</s:{action}>");
        sb.AppendLine("</soap:Body>");
        return sb.ToString();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await Task.CompletedTask;
        GC.SuppressFinalize(this);
    }
}
