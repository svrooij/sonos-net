using Sonos.Base.Music.Models;
using Sonos.Base.Music.Soap;

namespace Sonos.Base.Music
{
    public class MusicClient
    {
        private readonly HttpClient httpClient;
        private readonly MusicClientOptions options;

        public MusicClient(MusicClientOptions options, HttpClient? httpClient = null)
        {
            this.options = options;
            this.httpClient = httpClient ?? new HttpClient();
        }

        public Task<Models.GetAppLinkResult?> GetAppLinkAsync(CancellationToken cancellationToken = default) {
            if (string.IsNullOrWhiteSpace(options.HouseholdId))
            {
                throw new ArgumentNullException(nameof(options.HouseholdId));
            }
            return GetAppLinkAsync(new Models.GetAppLinkRequest { HouseholdId = options.HouseholdId }, cancellationToken);
        }

        public Task<Models.GetAppLinkResult?> GetAppLinkAsync(Models.GetAppLinkRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<Models.GetAppLinkRequest, Models.GetAppLinkResponse, Models.GetAppLinkResult>("getAppLink", request, true, cancellationToken);

        public async Task<bool> FinishLoginAsync(string linkCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(options.HouseholdId))
            {
                throw new ArgumentNullException(nameof(MusicClientOptions.HouseholdId));
            }
            if (string.IsNullOrEmpty(options.DeviceId))
            {
                throw new ArgumentNullException(nameof(MusicClientOptions.DeviceId));
            }
            if(string.IsNullOrEmpty(linkCode))
            {
                throw new ArgumentNullException(nameof(linkCode));
            }
            if(options.CredentialStore == null)
            {
                throw new ArgumentNullException(nameof(MusicClientOptions.CredentialStore));
            }

            var result = await GetDeviceAuthTokenAsync(new GetDeviceAuthTokenRequest { HouseholdId = options.HouseholdId, LinkCode = linkCode, LinkDeviceId = options.DeviceId }, cancellationToken);

            if (string.IsNullOrEmpty(result?.Key) || string.IsNullOrEmpty(result?.AuthenticationToken))
            {
                throw new Exception("Error logging in");
            }

            return await options.CredentialStore.SaveAccount(options.ServiceId, result.Key, result.AuthenticationToken, cancellationToken);
        }

        public Task<Models.GetDeviceAuthTokenResult?> GetDeviceAuthTokenAsync(Models.GetDeviceAuthTokenRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<Models.GetDeviceAuthTokenRequest, Models.GetDeviceAuthTokenResponse, Models.GetDeviceAuthTokenResult>("getDeviceAuthToken", request, true, cancellationToken);

        public Task<Models.GetMetadataResult?> GetMetadataAsync(Models.GetMetadataRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<Models.GetMetadataRequest, Models.GetMetadataResponse, Models.GetMetadataResult>("getMetadata", request, false, cancellationToken);

        private async Task<TResponse?> ExecuteRequest<TBody, TSoap, TResponse>(string action, TBody body, bool skipKeyAndToken, CancellationToken cancellationToken) where TBody : Models.MusicClientBaseRequest where TSoap : class, ISmapiResponse<TResponse>
        {
            var header = await GetSoapHeader(cancellationToken, skipKeyAndToken);
            var request = SoapFactory.CreateRequest(new Uri(options.BaseUri), header, body, action);
            var response = await httpClient.SendAsync(request, cancellationToken);

            var xml = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something");
            }

            var parsedObject = SoapFactory.ParseXml<TSoap>(action, xml);
            if (parsedObject == null)
            {
                return default;
            }
            return parsedObject.Result;
        }

        private async Task<SoapHeader> GetSoapHeader(CancellationToken cancellationToken, bool skipKeyAndToken = false)
        {
            var context = new SoapHeaderContext() { TimeZone = options.TimeZone };

            if (options.AuthenticationType == AuthenticationType.Anonymous)
            {
                return new SoapHeader
                {
                    Context = context,
                    Credentials = new SoapHeaderCredentials { DeviceId = options.DeviceId }
                };
            }

            if (options.AuthenticationType == AuthenticationType.AppLink || options.AuthenticationType == AuthenticationType.DeviceLink)
            {
                if (options.CredentialStore == null && !skipKeyAndToken)
                {
                    throw new ArgumentNullException(nameof(options.CredentialStore), "CredentialStore is needed for these music services!");
                }
                var account = skipKeyAndToken == false ? null: await options.CredentialStore.GetAccountAsync(options.ServiceId, cancellationToken);
                return new SoapHeader
                {
                    Context = context,
                    Credentials = new SoapHeaderCredentials
                    {
                        DeviceId = options.DeviceId,
                        LoginToken = new SoapHeaderToken(account?.Token ?? string.Empty, account?.Key ?? string.Empty, options.HouseholdId ?? string.Empty)
                    }
                };
            }

            throw new NotImplementedException("This service requires an authentication type that is not supported (yet)");
        }
    }
}