using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sonos.Base.Music;




// Response Models
public class MediaList
{
    public int Index { get; set; }
    public int Count { get; set; }
    public int Total { get; set; }
    public MediaMetadata[]? MediaMetadata { get; set; }
    public MediaItem[]? MediaCollection { get; set; }
}

public class MediaMetadata
{
    [XmlElement("id", Namespace = "http://www.sonos.com/Services/1.1")]
    public string Id { get; set; } = string.Empty;

    [XmlElement("itemType", Namespace = "http://www.sonos.com/Services/1.1")]
    public string ItemType { get; set; } = string.Empty;

    [XmlElement("title", Namespace = "http://www.sonos.com/Services/1.1")]
    public string Title { get; set; } = string.Empty;

    [XmlElement("summary", Namespace = "http://www.sonos.com/Services/1.1")]
    public string? Summary { get; set; }

    [XmlElement("trackUri", Namespace = "http://www.sonos.com/Services/1.1")]
    public string? TrackUri { get; set; }

    [XmlElement("albumArtUri", Namespace = "http://www.sonos.com/Services/1.1")]
    public string? AlbumArtUri { get; set; }


    public string? Artist { get; set; }
    public string? Album { get; set; }
    public int? Duration { get; set; }
    public string? Genre { get; set; }
    public int? TrackNumber { get; set; }
}

public class MediaItem
{
    [XmlElement("id", Namespace = "http://www.sonos.com/Services/1.1")]
    public string Id { get; set; } = string.Empty;

    [XmlElement("itemType", Namespace = "http://www.sonos.com/Services/1.1")]
    public string ItemType { get; set; } = string.Empty;

    [XmlElement("title", Namespace = "http://www.sonos.com/Services/1.1")]
    public string Title { get; set; } = string.Empty;

    [XmlElement("albumArtUri", Namespace = "http://www.sonos.com/Services/1.1")]
    public string? AlbumArtUri { get; set; }

    [XmlElement("canEnumerate", Namespace = "http://www.sonos.com/Services/1.1")]
    public bool? CanEnumerate { get; set; }
    [XmlElement("canPlay", Namespace = "http://www.sonos.com/Services/1.1")]
    public bool? CanPlay { get; set; }
    [XmlElement("containsFavorite", Namespace = "http://www.sonos.com/Services/1.1")]
    public bool? ContainsFavorite { get; set; }

    public string? DisplayType { get; set; }
}

public class MediaUri
{
    public string Uri { get; set; } = string.Empty;
    public int? Duration { get; set; }
    public string? MimeType { get; set; }
}

public class DeviceLink
{
    public string RegUrl { get; set; } = string.Empty;
    public string LinkCode { get; set; } = string.Empty;
    public bool ShowLinkCode { get; set; }
}

public class GetAppLinkResponse
{
    public AuthorizeAccount? AuthorizeAccount { get; set; }
    public CreateAccount? CreateAccount { get; set; }
}

public class AuthorizeAccount
{
    public string AppUrlStringId { get; set; } = string.Empty;
    public DeviceLink DeviceLink { get; set; } = new();
}

public class CreateAccount
{
    public string AppUrlStringId { get; set; } = string.Empty;
}

public class DeviceAuthResponse
{
    public string AuthToken { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
    public UserInfo? UserInfo { get; set; }
}

public class UserInfo
{
    public string? AccountTier { get; set; }
    public string? Nickname { get; set; }
    public string? UserIdHashCode { get; set; }
}

// Request Models
public class GetAppLinkRequest
{
    public string HouseholdId { get; set; } = string.Empty;
}

public class GetDeviceAuthTokenRequest
{
    public string HouseholdId { get; set; } = string.Empty;
    public string LinkCode { get; set; } = string.Empty;
    public string? LinkDeviceId { get; set; }
}

public class GetDeviceLinkCodeRequest
{
    public string HouseholdId { get; set; } = string.Empty;
}

public class GetMetadataRequest
{
    public string Id { get; set; } = string.Empty;
    public int Index { get; set; }
    public int Count { get; set; }
    public bool Recursive { get; set; }
}

public class GetExtendedMetadataRequest
{
    public string Id { get; set; } = string.Empty;
}

public class GetMediaMetadataRequest
{
    public string Id { get; set; } = string.Empty;
}

public class GetMediaUriRequest
{
    public string Id { get; set; } = string.Empty;
}

public class SearchRequest
{
    public string Id { get; set; } = string.Empty;
    public string Term { get; set; } = string.Empty;
    public int Index { get; set; }
    public int Count { get; set; }
}

// Internal Response Models (for SOAP deserialization)
public class GetMetadataResponse
{
    [XmlElement("getMetadataResult", Namespace = "http://www.sonos.com/Services/1.1")]
    public GetMetadataResult MetadataResult { get; set; }
}

public class GetMetadataResult
{
    [XmlElement("index", Namespace = "http://www.sonos.com/Services/1.1")]
    public int Index { get; set; }

    [XmlElement("count", Namespace = "http://www.sonos.com/Services/1.1")]
    public int Count { get; set; }

    [XmlElement("total", Namespace = "http://www.sonos.com/Services/1.1")]
    public int Total { get; set; }

    [XmlElement("mediaMetadata", Namespace = "http://www.sonos.com/Services/1.1")]
    public MediaMetadata[]? MediaMetadata { get; set; }

    [XmlElement("mediaCollection", Namespace = "http://www.sonos.com/Services/1.1")]
    public MediaItem[]? MediaCollection { get; set; }
}


[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class MusicServiceSoapFault : Soap.SoapFault
{
    [System.Xml.Serialization.XmlElement("detail", Namespace = "")]
    public MusicServiceSoapFaultDetail? Detail { get; set; }
}

[Serializable]
public class MusicServiceSoapFaultDetail
{
    [XmlElement("refreshAuthTokenResult", Namespace = "")]
    //[XmlElement("refreshAuthTokenResult", Namespace = "http://www.sonos.com/Services/1.1")]
    public RefreshAuthTokenResult? RefreshAuthTokenResult { get; set; }
}

[Serializable]
public class RefreshAuthTokenResult
{
    [XmlElement("authToken", Namespace = "http://www.sonos.com/Services/1.1")]
    public string? AuthToken { get; set; }

    [XmlElement("privateKey", Namespace = "http://www.sonos.com/Services/1.1")]
    public string? PrivateKey { get; set; }

    [XmlElement("userInfo", Namespace = "http://www.sonos.com/Services/1.1")]
    public UserInfo? UserInfo { get; set; }
}