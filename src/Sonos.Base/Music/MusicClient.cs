using Sonos.Base.Music.Soap;
using Sonos.Base.Smapi;

namespace Sonos.Base.Music
{
    public class MusicClient
    {
        private readonly MusicClientOptions options;
        private readonly HttpClient httpClient;
        private readonly ISonosSoap smapiClient;

        public MusicClient(MusicClientOptions options, HttpClient httpClient = null)
        {
            this.options = options;
            smapiClient = new SonosSoapClient(SonosSoapClient.EndpointConfiguration.BasicHttpsBinding_ISonosSoap, options.BaseUri);
            this.httpClient = httpClient ?? new HttpClient();
        }

        private async Task<SoapHeader> GetSoapHeader(CancellationToken cancellationToken, bool skipKeyAndToken = false)
        {
            var context = new SoapHeaderContext() { TimeZone = options.TimeZone };

            if (options.AuthenticationType == AuthenticationType.Anonymous || skipKeyAndToken)
            {
                return new SoapHeader
                {
                    Context = context,
                    Credentials = new SoapHeaderCredentials { DeviceId = options.DeviceId }
                };
            }

            if (options.AuthenticationType == AuthenticationType.AppLink || options.AuthenticationType == AuthenticationType.DeviceLink)
            {
                if (options.CredentialStore == null)
                {
                    throw new ArgumentNullException(nameof(options.CredentialStore), "CredentialStore is needed for these music services!");
                }
                var account = await options.CredentialStore.GetAccountAsync(options.ServiceId, cancellationToken);
                return new SoapHeader
                {
                    Context = context,
                    Credentials = new SoapHeaderTokenCredentials(account?.Token ?? string.Empty, account?.Key ?? string.Empty, options.HouseholdId, options.DeviceId)
                };
            }

            throw new NotImplementedException("This service requires an authentication type that is not supported (yet)");
        }

        private async Task<TResponse> ExecuteRequest<TBody, TResponse>(string action, TBody body, bool skipKeyAndToken, CancellationToken cancellationToken) where TBody : Models.MusicClientBaseRequest where TResponse : class
        {
            var header = await GetSoapHeader(cancellationToken, skipKeyAndToken);
            var request = SoapFactory.CreateRequest(new Uri(options.BaseUri), header, body, action);
            var response = await httpClient.SendAsync(request, cancellationToken);

            var xml = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something");
            }

            return SoapFactory.ParseXml<TResponse>(action, xml);
        }

        public Task<Models.GetMetadataResponse> GetMetadataAsync(Models.GetMetadataRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<Models.GetMetadataRequest, Models.GetMetadataResponse>("getMetadata", request, false, cancellationToken);
    }
}