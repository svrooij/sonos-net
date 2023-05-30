using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sonos.Base.Music.Models
{
    [Serializable()]
    [XmlRoot(Namespace = "http://www.sonos.com/Services/1.1")]
    public class GetAppLinkRequest : MusicClientBaseRequest
    {
        /// <summary>
        /// Load from DevicePropertiesService.GetHouseholdID()
        /// </summary>
        [XmlElement(ElementName = "householdId", Namespace = "http://www.sonos.com/Services/1.1")]
        public string HouseholdId { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    [XmlRoot(Namespace = "http://www.sonos.com/Services/1.1", IsNullable = false)]
    public partial class GetAppLinkResponse : ISmapiResponse<GetAppLinkResult>
    {
        [XmlElement("getAppLinkResult")]
        public GetAppLinkResult Result { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    public partial class GetAppLinkResult
    {
        [XmlElement("authorizeAccount")]
        public GetAppLinkAuthorizeAccount AuthorizeAccount { get; set; }

        [XmlElement("createAccount")]
        public GetAppLinkAccount CreateAccount { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    public partial class GetAppLinkAccount
    {
        [XmlElement("appUrlStringId")]
        public string AppUrlStringId { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.sonos.com/Services/1.1")]
    public partial class GetAppLinkAuthorizeAccount : GetAppLinkAccount
    {
        [XmlElement("deviceLink")]
        public GetAppLinkDeviceLink DeviceLink { get; set; }
    }


    public partial class GetAppLinkDeviceLink
    {
        [XmlElement("regUrl")]

        public string RegistrationUrl { get; set; }
        [XmlElement("linkCode")]

        public string LinkCode { get; set; }
        [XmlElement("showLinkCode")]

        public bool ShowLinkCode { get; set; }
    }
}
