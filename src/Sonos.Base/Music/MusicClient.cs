using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sonos.Base.Music.Models;
using Sonos.Base.Music.Soap;

namespace Sonos.Base.Music
{
    public class MusicClient
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<MusicClient> logger;
        private readonly MusicClientOptions options;
        private const string TokenRefreshRequiredCode = ":Client.TokenRefreshRequired";

        public MusicClient(MusicClientOptions options, HttpClient? httpClient = null, ILogger<MusicClient>? logger = null)
        {
            this.options = options;
            this.httpClient = httpClient ?? new HttpClient();
            this.logger = logger ?? NullLogger<MusicClient>.Instance;
        }

        public AuthenticationType AuthenticationType => options.AuthenticationType;

        public Task<GetAppLinkResult?> GetAppLinkAsync(CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(options.HouseholdId))
            {
                throw new ArgumentNullException(nameof(options.HouseholdId));
            }
            return GetAppLinkAsync(new GetAppLinkRequest { HouseholdId = options.HouseholdId }, cancellationToken);
        }

        public Task<GetAppLinkResult?> GetAppLinkAsync(GetAppLinkRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetAppLinkRequest, GetAppLinkResponse, GetAppLinkResult>("getAppLink", request, true, cancellationToken);

        public async Task<bool> FinishLoginAsync(string linkCode, CancellationToken cancellationToken = default)
        {
            if (options.HouseholdId is null || string.IsNullOrEmpty(options.HouseholdId))
            {
                throw new ArgumentNullException(nameof(MusicClientOptions.HouseholdId));
            }
            if (options.DeviceId is null || string.IsNullOrEmpty(options.DeviceId))
            {
                throw new ArgumentNullException(nameof(MusicClientOptions.DeviceId));
            }
            if (string.IsNullOrEmpty(linkCode))
            {
                throw new ArgumentNullException(nameof(linkCode));
            }
            if (options.CredentialStore is null)
            {
                throw new ArgumentNullException(nameof(MusicClientOptions.CredentialStore));
            }

            try
            {
                var result = await GetDeviceAuthTokenAsync(new GetDeviceAuthTokenRequest { HouseholdId = options.HouseholdId, LinkCode = linkCode, LinkDeviceId = options.DeviceId }, cancellationToken);
                if (result is null)
                {
                    return false;
                }
                return await options.CredentialStore.SaveAccountAsync(options.ServiceId, result.Key, result.AuthenticationToken, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Error logging in");
            }

            return false;
        }

        public Task<GetDeviceAuthTokenResult?> GetDeviceAuthTokenAsync(GetDeviceAuthTokenRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetDeviceAuthTokenRequest, GetDeviceAuthTokenResponse, GetDeviceAuthTokenResult>("getDeviceAuthToken", request, true, cancellationToken);

        public Task<GetMetadataResult?> GetMetadataAsync(GetMetadataRequest request, CancellationToken cancellationToken = default) => ExecuteRequest<GetMetadataRequest, GetMetadataResponse, GetMetadataResult>("getMetadata", request, false, cancellationToken);

        private async Task<TResponse?> ExecuteRequest<TBody, TSoap, TResponse>(string action, TBody body, bool skipKeyAndToken, CancellationToken cancellationToken, bool isRetry = false) where TBody : MusicClientBaseRequest where TSoap : class, ISmapiResponse<TResponse>
        {
            var header = await GetSoapHeader(cancellationToken, skipKeyAndToken);
            var request = SoapFactory.CreateRequest(new Uri(options.BaseUri), header, body, action);
            var response = await httpClient.SendAsync(request, cancellationToken);

            var xml = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    var fault = SoapFactory.ParseException(xml);
                    if (fault == null)
                    {
                        return default;
                    }
                    if (options.CredentialStore != null && !isRetry && fault.Details?.NewToken != null)
                    {
                        await options.CredentialStore.SaveAccountAsync(options.ServiceId, fault.Details.NewToken.PrivateKey, fault.Details.NewToken.AuthToken, cancellationToken);
                        return await ExecuteRequest<TBody, TSoap, TResponse>(action, body, skipKeyAndToken, cancellationToken, true);
                    }
                    throw new Exception(fault.Message?.Value ?? $"Sonos error {fault.Code}");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Error executing request to {action}", action);
                    throw;
                }
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
                if (options.CredentialStore == null)
                {
                    throw new ArgumentNullException(nameof(options.CredentialStore), "CredentialStore is needed for this music service!");
                }
                var account = skipKeyAndToken == false ? await options.CredentialStore.GetAccountAsync(options.ServiceId, cancellationToken) : null;
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