using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sonos.Base.Music.Models
{

    [Serializable()]
    [XmlRoot(Namespace = "http://www.sonos.com/Services/1.1")]
    public class GetDeviceAuthTokenRequest : MusicClientBaseRequest
    {
        /// <summary>
        /// Load from DevicePropertiesService.GetHouseholdID()
        /// </summary>
        [XmlElement(ElementName = "householdId", Namespace = "http://www.sonos.com/Services/1.1")]
        public string HouseholdId { get; set; }

        /// <summary>
        /// Get the link code from GetAppLinkAsync or GetDeviceLinkAsync
        /// </summary>
        [XmlElement(ElementName = "linkCode", Namespace = "http://www.sonos.com/Services/1.1")]
        public string LinkCode { get; set; }

        /// <summary>
        /// Get from SystemPropertiesService.GetString("R_TrialZPSerial");
        /// </summary>
        [XmlElement(ElementName = "linkDeviceId", Namespace = "http://www.sonos.com/Services/1.1")]
        public string LinkDeviceId { get; set; }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    [XmlRoot(Namespace = "http://www.sonos.com/Services/1.1", IsNullable = false)]
    public partial class GetDeviceAuthTokenResponse : ISmapiResponse<GetDeviceAuthTokenResult>
    {
        [XmlElement("getDeviceAuthTokenResult")]
        public GetDeviceAuthTokenResult Result { get; set; }

    }

    public partial class GetDeviceAuthTokenResult
    {
        [XmlElement("authToken")]
        public string AuthenticationToken { get; set; }

        [XmlElement("privateKey")]
        public string Key { get; set; }

        [XmlElement("userInfo")]
        public GetDeviceAuthTokenResultUserInfo UserInfo { get; set; }

    }

    public partial class GetDeviceAuthTokenResultUserInfo
    {
        [XmlElement("userIdHashCode")]
        public string UserHash { get; set; }

        [XmlElement("accountTier")]
        public string AccountTier { get; set; }
        [XmlElement("nickname")]
        public string Nickname { get; set; }
    }
}
