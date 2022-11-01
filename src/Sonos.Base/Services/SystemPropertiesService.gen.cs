namespace Sonos.Base.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// SystemPropertiesService  - Manage system-wide settings, mainly account stuff
/// </summary>
public partial class SystemPropertiesService : SonosBaseService
{
    /// <summary>
    /// Create a new SystemPropertiesService
    /// </summary>
    /// <param name="sonosUri">Base URL of the speaker</param>
    /// <param name="httpClient">Optionally, a custom HttpClient.</param>
    public SystemPropertiesService(SonosServiceOptions options): base("SystemProperties", "/SystemProperties/Control", "/SystemProperties/Event", options) {}


    /// <summary>
    /// AddAccountX
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>AddAccountXResponse</returns>
    public Task<AddAccountXResponse> AddAccountX(AddAccountXRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<AddAccountXRequest, AddAccountXResponse>("AddAccountX", request, cancellationToken);

    /// <summary>
    /// AddOAuthAccountX
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>AddOAuthAccountXResponse</returns>
    public Task<AddOAuthAccountXResponse> AddOAuthAccountX(AddOAuthAccountXRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<AddOAuthAccountXRequest, AddOAuthAccountXResponse>("AddOAuthAccountX", request, cancellationToken);

    /// <summary>
    /// DoPostUpdateTasks
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> DoPostUpdateTasks(CancellationToken cancellationToken = default) =>  ExecuteRequest<BaseRequest>("DoPostUpdateTasks", new BaseRequest(), cancellationToken);

    /// <summary>
    /// EditAccountMd
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> EditAccountMd(EditAccountMdRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<EditAccountMdRequest>("EditAccountMd", request, cancellationToken);

    /// <summary>
    /// EditAccountPasswordX
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> EditAccountPasswordX(EditAccountPasswordXRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<EditAccountPasswordXRequest>("EditAccountPasswordX", request, cancellationToken);

    /// <summary>
    /// EnableRDM
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> EnableRDM(EnableRDMRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<EnableRDMRequest>("EnableRDM", request, cancellationToken);

    /// <summary>
    /// GetRDM
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetRDMResponse</returns>
    public Task<GetRDMResponse> GetRDM(CancellationToken cancellationToken = default) =>  ExecuteRequest<BaseRequest, GetRDMResponse>("GetRDM", new BaseRequest(), cancellationToken);

    /// <summary>
    /// Get a saved string.
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Strings are saved in the system with SetString, every speaker should return the same data. Will error when not existing</remarks>
    /// <returns>GetStringResponse</returns>
    public Task<GetStringResponse> GetString(GetStringRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<GetStringRequest, GetStringResponse>("GetString", request, cancellationToken);

    /// <summary>
    /// GetWebCode
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GetWebCodeResponse</returns>
    public Task<GetWebCodeResponse> GetWebCode(GetWebCodeRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<GetWebCodeRequest, GetWebCodeResponse>("GetWebCode", request, cancellationToken);

    /// <summary>
    /// ProvisionCredentialedTrialAccountX
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>ProvisionCredentialedTrialAccountXResponse</returns>
    public Task<ProvisionCredentialedTrialAccountXResponse> ProvisionCredentialedTrialAccountX(ProvisionCredentialedTrialAccountXRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<ProvisionCredentialedTrialAccountXRequest, ProvisionCredentialedTrialAccountXResponse>("ProvisionCredentialedTrialAccountX", request, cancellationToken);

    /// <summary>
    /// RefreshAccountCredentialsX
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RefreshAccountCredentialsX(RefreshAccountCredentialsXRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<RefreshAccountCredentialsXRequest>("RefreshAccountCredentialsX", request, cancellationToken);

    /// <summary>
    /// Remove a saved string
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Not sure what happens if you call this with a VariableName that doesn't exists.</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> Remove(RemoveRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<RemoveRequest>("Remove", request, cancellationToken);

    /// <summary>
    /// RemoveAccount
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> RemoveAccount(RemoveAccountRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<RemoveAccountRequest>("RemoveAccount", request, cancellationToken);

    /// <summary>
    /// ReplaceAccountX
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>ReplaceAccountXResponse</returns>
    public Task<ReplaceAccountXResponse> ReplaceAccountX(ReplaceAccountXRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<ReplaceAccountXRequest, ReplaceAccountXResponse>("ReplaceAccountX", request, cancellationToken);

    /// <summary>
    /// ResetThirdPartyCredentials
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> ResetThirdPartyCredentials(CancellationToken cancellationToken = default) =>  ExecuteRequest<BaseRequest>("ResetThirdPartyCredentials", new BaseRequest(), cancellationToken);

    /// <summary>
    /// SetAccountNicknameX
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Success boolean</returns>
    public Task<bool> SetAccountNicknameX(SetAccountNicknameXRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<SetAccountNicknameXRequest>("SetAccountNicknameX", request, cancellationToken);

    /// <summary>
    /// Save a string in the system
    /// </summary>
    /// <param name="request">Body payload</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Strings are saved in the system, retrieve values with GetString.</remarks>
    /// <returns>Success boolean</returns>
    public Task<bool> SetString(SetStringRequest request, CancellationToken cancellationToken = default) =>  ExecuteRequest<SetStringRequest>("SetString", request, cancellationToken);

    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class BaseRequest
    {
      [System.Xml.Serialization.XmlNamespaceDeclarations]
      public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
        new[] { new System.Xml.XmlQualifiedName("u", "urn:schemas-upnp-org:service:SystemProperties:1"), });
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class AddAccountXRequest : BaseRequest
    {

        public int AccountType { get; set; }

        public string AccountID { get; set; }

        public string AccountPassword { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("AddAccountXResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:SystemProperties:1")]
    public partial class AddAccountXResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AccountUDN { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class AddOAuthAccountXRequest : BaseRequest
    {

        public int AccountType { get; set; }

        public string AccountToken { get; set; }

        public string AccountKey { get; set; }

        public string OAuthDeviceID { get; set; }

        public string AuthorizationCode { get; set; }

        public string RedirectURI { get; set; }

        public string UserIdHashCode { get; set; }

        public int AccountTier { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("AddOAuthAccountXResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:SystemProperties:1")]
    public partial class AddOAuthAccountXResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AccountUDN { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AccountNickname { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class EditAccountMdRequest : BaseRequest
    {

        public int AccountType { get; set; }

        public string AccountID { get; set; }

        public string NewAccountMd { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class EditAccountPasswordXRequest : BaseRequest
    {

        public int AccountType { get; set; }

        public string AccountID { get; set; }

        public string NewAccountPassword { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class EnableRDMRequest : BaseRequest
    {

        public bool RDMValue { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetRDMResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:SystemProperties:1")]
    public partial class GetRDMResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool RDMValue { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetStringRequest : BaseRequest
    {

        /// <summary>
        /// The key for this variable
        /// </summary>
        public string VariableName { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetStringResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:SystemProperties:1")]
    public partial class GetStringResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string StringValue { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class GetWebCodeRequest : BaseRequest
    {

        public int AccountType { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("GetWebCodeResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:SystemProperties:1")]
    public partial class GetWebCodeResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string WebCode { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class ProvisionCredentialedTrialAccountXRequest : BaseRequest
    {

        public int AccountType { get; set; }

        public string AccountID { get; set; }

        public string AccountPassword { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("ProvisionCredentialedTrialAccountXResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:SystemProperties:1")]
    public partial class ProvisionCredentialedTrialAccountXResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public bool IsExpired { get; set; }

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string AccountUDN { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RefreshAccountCredentialsXRequest : BaseRequest
    {

        public int AccountType { get; set; }

        public int AccountUID { get; set; }

        public string AccountToken { get; set; }

        public string AccountKey { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RemoveRequest : BaseRequest
    {

        /// <summary>
        /// The key for this variable
        /// </summary>
        public string VariableName { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class RemoveAccountRequest : BaseRequest
    {

        public int AccountType { get; set; }

        public string AccountID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class ReplaceAccountXRequest : BaseRequest
    {

        public string AccountUDN { get; set; }

        public string NewAccountID { get; set; }

        public string NewAccountPassword { get; set; }

        public string AccountToken { get; set; }

        public string AccountKey { get; set; }

        public string OAuthDeviceID { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlType("ReplaceAccountXResponse", AnonymousType = true, Namespace = "urn:schemas-upnp-org:service:SystemProperties:1")]
    public partial class ReplaceAccountXResponse
    {

        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public string NewAccountUDN { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetAccountNicknameXRequest : BaseRequest
    {

        public string AccountUDN { get; set; }

        public string AccountNickname { get; set; }
    }

    [System.Serializable()]
    [System.Xml.Serialization.XmlRoot(Namespace = "")]
    public class SetStringRequest : BaseRequest
    {

        /// <summary>
        /// The key for this variable, use something unique
        /// </summary>
        public string VariableName { get; set; }

        public string StringValue { get; set; }
    }
}
