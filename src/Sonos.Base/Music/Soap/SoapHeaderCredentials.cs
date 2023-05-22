using System.Xml.Serialization;

namespace Sonos.Base.Music.Soap
{
    public class SoapHeaderCredentials
    {
        [XmlElement(ElementName = "deviceId", Namespace = "http://www.sonos.com/Services/1.1")]
        public string? DeviceId { get; set; }

        [XmlElement(ElementName = "loginToken", Namespace = "http://www.sonos.com/Services/1.1")]
        public SoapHeaderToken? LoginToken { get; set; }
    }

    public class SoapHeaderSessionCredentials : SoapHeaderCredentials
    {
        public SoapHeaderSessionCredentials()
        { }

        public SoapHeaderSessionCredentials(string? sessionId, string deviceId)
        {
            DeviceId = deviceId;
            SessionId = sessionId;
        }

        [XmlElement(ElementName = "sessionId", Namespace = "http://www.sonos.com/Services/1.1")]
        public string? SessionId { get; set; }
    }

    public class SoapHeaderLoginCredentials : SoapHeaderCredentials
    {
        public SoapHeaderLoginCredentials()
        { }

        public SoapHeaderLoginCredentials(string username, string password, string deviceId)
        {
            DeviceId = deviceId;
            Login = new SoapHeaderLogin(username, password);
        }

        [XmlElement(ElementName = "login", Namespace = "http://www.sonos.com/Services/1.1")]
        public SoapHeaderLogin? Login { get; set; }
    }

    public class SoapHeaderLogin
    {
        public SoapHeaderLogin()
        { }

        public SoapHeaderLogin(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [XmlElement(ElementName = "username", Namespace = "http://www.sonos.com/Services/1.1")]
        public string Username { get; set; }

        [XmlElement(ElementName = "password", Namespace = "http://www.sonos.com/Services/1.1")]
        public string Password { get; set; }
    }

    public class SoapHeaderToken
    {
        public SoapHeaderToken()
        { }

        public SoapHeaderToken(string token, string key, string householdId)
        {
            Token = token;
            Key = key;
            HouseholdId = householdId;
        }

        [XmlElement(ElementName = "token", Namespace = "http://www.sonos.com/Services/1.1")]
        public string Token { get; set; }

        [XmlElement(ElementName = "key", Namespace = "http://www.sonos.com/Services/1.1")]
        public string Key { get; set; }

        [XmlElement(ElementName = "householdId", Namespace = "http://www.sonos.com/Services/1.1")]
        public string HouseholdId { get; set; }
    }
}