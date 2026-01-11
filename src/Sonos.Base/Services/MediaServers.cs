using System;
using System.Collections.Generic;
using System.Text;

namespace Sonos.Base.Services.ZoneGroup;


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class MediaServers
{

    private MediaServersService[] serviceField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Service")]
    public MediaServersService[] Service
    {
        get
        {
            return this.serviceField;
        }
        set
        {
            this.serviceField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class MediaServersService
{

    private string uDNField;

    private byte numAccountsField;

    private string md0Field;

    private string username0Field;

    private string nickname0Field;

    private byte serialNum0Field;

    private byte flags0Field;

    private byte tier0Field;

    private string token0Field;

    private string key0Field;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string UDN
    {
        get
        {
            return this.uDNField;
        }
        set
        {
            this.uDNField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte NumAccounts
    {
        get
        {
            return this.numAccountsField;
        }
        set
        {
            this.numAccountsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Md0
    {
        get
        {
            return this.md0Field;
        }
        set
        {
            this.md0Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Username0
    {
        get
        {
            return this.username0Field;
        }
        set
        {
            this.username0Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Nickname0
    {
        get
        {
            return this.nickname0Field;
        }
        set
        {
            this.nickname0Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SerialNum0
    {
        get
        {
            return this.serialNum0Field;
        }
        set
        {
            this.serialNum0Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte Flags0
    {
        get
        {
            return this.flags0Field;
        }
        set
        {
            this.flags0Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte Tier0
    {
        get
        {
            return this.tier0Field;
        }
        set
        {
            this.tier0Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Token0
    {
        get
        {
            return this.token0Field;
        }
        set
        {
            this.token0Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Key0
    {
        get
        {
            return this.key0Field;
        }
        set
        {
            this.key0Field = value;
        }
    }
}

