using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Sonos.Web.Blazor.Client;

internal class SonosWebClientFactory
{
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly HttpClient _httpClient;

    public SonosWebClientFactory(HttpClient httpClient, IAuthenticationProvider? authenticationProvider = null)
    {
        _authenticationProvider = authenticationProvider ?? new AnonymousAuthenticationProvider();
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public SonosWebClient CreateClient()
    {
        return new SonosWebClient(new HttpClientRequestAdapter(_authenticationProvider, httpClient: _httpClient));
    }
}
