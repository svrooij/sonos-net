namespace Sonos.Base.Models;


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType("root", AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
[System.Xml.Serialization.XmlRoot("root", Namespace = "urn:schemas-upnp-org:device-1-0", IsNullable = false)]
public partial class SonosDeviceDescription
{

    private rootSpecVersion specVersionField;

    private rootDevice deviceField;

    /// <remarks/>
    public rootSpecVersion specVersion
    {
        get
        {
            return this.specVersionField;
        }
        set
        {
            this.specVersionField = value;
        }
    }

    /// <remarks/>
    public rootDevice device
    {
        get
        {
            return this.deviceField;
        }
        set
        {
            this.deviceField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
public partial class rootSpecVersion
{

    private byte majorField;

    private byte minorField;

    /// <remarks/>
    public byte major
    {
        get
        {
            return this.majorField;
        }
        set
        {
            this.majorField = value;
        }
    }

    /// <remarks/>
    public byte minor
    {
        get
        {
            return this.minorField;
        }
        set
        {
            this.minorField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
public partial class rootDevice
{

    private string deviceTypeField;

    private string friendlyNameField;

    private string manufacturerField;

    private string manufacturerURLField;

    private string modelNumberField;

    private string modelDescriptionField;

    private string modelNameField;

    private string modelURLField;

    private string softwareVersionField;

    private byte swGenField;

    private string hardwareVersionField;

    private string serialNumField;

    private string mACAddressField;

    private string uDNField;

    private rootDeviceIconList iconListField;

    private string minCompatibleVersionField;

    private string legacyCompatibleVersionField;

    private string apiVersionField;

    private string minApiVersionField;

    private decimal displayVersionField;

    private object extraVersionField;

    private byte nsVersionField;

    private string roomNameField;

    private string displayNameField;

    private byte zoneTypeField;

    private string feature1Field;

    private string feature2Field;

    private string feature3Field;

    private string seriesidField;

    private byte variantField;

    private byte internalSpeakerSizeField;

    private ushort memoryField;

    private ushort flashField;

    private int ampOnTimeField;

    private int retailModeField;

    private ushort sSLPortField;

    private ushort securehhSSLPortField;

    private rootDeviceService[] serviceListField;

    private rootDeviceDevice[] deviceListField;

    /// <remarks/>
    public string deviceType
    {
        get
        {
            return this.deviceTypeField;
        }
        set
        {
            this.deviceTypeField = value;
        }
    }

    /// <remarks/>
    public string friendlyName
    {
        get
        {
            return this.friendlyNameField;
        }
        set
        {
            this.friendlyNameField = value;
        }
    }

    /// <remarks/>
    public string manufacturer
    {
        get
        {
            return this.manufacturerField;
        }
        set
        {
            this.manufacturerField = value;
        }
    }

    /// <remarks/>
    public string manufacturerURL
    {
        get
        {
            return this.manufacturerURLField;
        }
        set
        {
            this.manufacturerURLField = value;
        }
    }

    /// <remarks/>
    public string modelNumber
    {
        get
        {
            return this.modelNumberField;
        }
        set
        {
            this.modelNumberField = value;
        }
    }

    /// <remarks/>
    public string modelDescription
    {
        get
        {
            return this.modelDescriptionField;
        }
        set
        {
            this.modelDescriptionField = value;
        }
    }

    /// <remarks/>
    public string modelName
    {
        get
        {
            return this.modelNameField;
        }
        set
        {
            this.modelNameField = value;
        }
    }

    /// <remarks/>
    public string modelURL
    {
        get
        {
            return this.modelURLField;
        }
        set
        {
            this.modelURLField = value;
        }
    }

    /// <remarks/>
    public string softwareVersion
    {
        get
        {
            return this.softwareVersionField;
        }
        set
        {
            this.softwareVersionField = value;
        }
    }

    /// <remarks/>
    public byte swGen
    {
        get
        {
            return this.swGenField;
        }
        set
        {
            this.swGenField = value;
        }
    }

    /// <remarks/>
    public string hardwareVersion
    {
        get
        {
            return this.hardwareVersionField;
        }
        set
        {
            this.hardwareVersionField = value;
        }
    }

    /// <remarks/>
    public string serialNum
    {
        get
        {
            return this.serialNumField;
        }
        set
        {
            this.serialNumField = value;
        }
    }

    /// <remarks/>
    public string MACAddress
    {
        get
        {
            return this.mACAddressField;
        }
        set
        {
            this.mACAddressField = value;
        }
    }

    /// <remarks/>
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
    public rootDeviceIconList iconList
    {
        get
        {
            return this.iconListField;
        }
        set
        {
            this.iconListField = value;
        }
    }

    /// <remarks/>
    public string minCompatibleVersion
    {
        get
        {
            return this.minCompatibleVersionField;
        }
        set
        {
            this.minCompatibleVersionField = value;
        }
    }

    /// <remarks/>
    public string legacyCompatibleVersion
    {
        get
        {
            return this.legacyCompatibleVersionField;
        }
        set
        {
            this.legacyCompatibleVersionField = value;
        }
    }

    /// <remarks/>
    public string apiVersion
    {
        get
        {
            return this.apiVersionField;
        }
        set
        {
            this.apiVersionField = value;
        }
    }

    /// <remarks/>
    public string minApiVersion
    {
        get
        {
            return this.minApiVersionField;
        }
        set
        {
            this.minApiVersionField = value;
        }
    }

    /// <remarks/>
    public decimal displayVersion
    {
        get
        {
            return this.displayVersionField;
        }
        set
        {
            this.displayVersionField = value;
        }
    }

    /// <remarks/>
    public object extraVersion
    {
        get
        {
            return this.extraVersionField;
        }
        set
        {
            this.extraVersionField = value;
        }
    }

    /// <remarks/>
    public byte nsVersion
    {
        get
        {
            return this.nsVersionField;
        }
        set
        {
            this.nsVersionField = value;
        }
    }

    /// <remarks/>
    public string roomName
    {
        get
        {
            return this.roomNameField;
        }
        set
        {
            this.roomNameField = value;
        }
    }

    /// <remarks/>
    public string displayName
    {
        get
        {
            return this.displayNameField;
        }
        set
        {
            this.displayNameField = value;
        }
    }

    /// <remarks/>
    public byte zoneType
    {
        get
        {
            return this.zoneTypeField;
        }
        set
        {
            this.zoneTypeField = value;
        }
    }

    /// <remarks/>
    public string feature1
    {
        get
        {
            return this.feature1Field;
        }
        set
        {
            this.feature1Field = value;
        }
    }

    /// <remarks/>
    public string feature2
    {
        get
        {
            return this.feature2Field;
        }
        set
        {
            this.feature2Field = value;
        }
    }

    /// <remarks/>
    public string feature3
    {
        get
        {
            return this.feature3Field;
        }
        set
        {
            this.feature3Field = value;
        }
    }

    /// <remarks/>
    public string seriesid
    {
        get
        {
            return this.seriesidField;
        }
        set
        {
            this.seriesidField = value;
        }
    }

    /// <remarks/>
    public byte variant
    {
        get
        {
            return this.variantField;
        }
        set
        {
            this.variantField = value;
        }
    }

    /// <remarks/>
    public byte internalSpeakerSize
    {
        get
        {
            return this.internalSpeakerSizeField;
        }
        set
        {
            this.internalSpeakerSizeField = value;
        }
    }

    /// <remarks/>
    public ushort memory
    {
        get
        {
            return this.memoryField;
        }
        set
        {
            this.memoryField = value;
        }
    }

    /// <remarks/>
    public ushort flash
    {
        get
        {
            return this.flashField;
        }
        set
        {
            this.flashField = value;
        }
    }

    /// <remarks/>
    public int ampOnTime
    {
        get
        {
            return this.ampOnTimeField;
        }
        set
        {
            this.ampOnTimeField = value;
        }
    }

    /// <remarks/>
    public int retailMode
    {
        get
        {
            return this.retailModeField;
        }
        set
        {
            this.retailModeField = value;
        }
    }

    /// <remarks/>
    public ushort SSLPort
    {
        get
        {
            return this.sSLPortField;
        }
        set
        {
            this.sSLPortField = value;
        }
    }

    /// <remarks/>
    public ushort securehhSSLPort
    {
        get
        {
            return this.securehhSSLPortField;
        }
        set
        {
            this.securehhSSLPortField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItem("service", IsNullable = false)]
    public rootDeviceService[] serviceList
    {
        get
        {
            return this.serviceListField;
        }
        set
        {
            this.serviceListField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItem("device", IsNullable = false)]
    public rootDeviceDevice[] deviceList
    {
        get
        {
            return this.deviceListField;
        }
        set
        {
            this.deviceListField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
public partial class rootDeviceIconList
{

    private rootDeviceIconListIcon iconField;

    /// <remarks/>
    public rootDeviceIconListIcon icon
    {
        get
        {
            return this.iconField;
        }
        set
        {
            this.iconField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
public partial class rootDeviceIconListIcon
{

    private byte idField;

    private string mimetypeField;

    private byte widthField;

    private byte heightField;

    private byte depthField;

    private string urlField;

    /// <remarks/>
    public byte id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string mimetype
    {
        get
        {
            return this.mimetypeField;
        }
        set
        {
            this.mimetypeField = value;
        }
    }

    /// <remarks/>
    public byte width
    {
        get
        {
            return this.widthField;
        }
        set
        {
            this.widthField = value;
        }
    }

    /// <remarks/>
    public byte height
    {
        get
        {
            return this.heightField;
        }
        set
        {
            this.heightField = value;
        }
    }

    /// <remarks/>
    public byte depth
    {
        get
        {
            return this.depthField;
        }
        set
        {
            this.depthField = value;
        }
    }

    /// <remarks/>
    public string url
    {
        get
        {
            return this.urlField;
        }
        set
        {
            this.urlField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
public partial class rootDeviceService
{

    private string serviceTypeField;

    private string serviceIdField;

    private string controlURLField;

    private string eventSubURLField;

    private string sCPDURLField;

    /// <remarks/>
    public string serviceType
    {
        get
        {
            return this.serviceTypeField;
        }
        set
        {
            this.serviceTypeField = value;
        }
    }

    /// <remarks/>
    public string serviceId
    {
        get
        {
            return this.serviceIdField;
        }
        set
        {
            this.serviceIdField = value;
        }
    }

    /// <remarks/>
    public string controlURL
    {
        get
        {
            return this.controlURLField;
        }
        set
        {
            this.controlURLField = value;
        }
    }

    /// <remarks/>
    public string eventSubURL
    {
        get
        {
            return this.eventSubURLField;
        }
        set
        {
            this.eventSubURLField = value;
        }
    }

    /// <remarks/>
    public string SCPDURL
    {
        get
        {
            return this.sCPDURLField;
        }
        set
        {
            this.sCPDURLField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
public partial class rootDeviceDevice
{

    private string deviceTypeField;

    private string friendlyNameField;

    private string manufacturerField;

    private string manufacturerURLField;

    private string modelNumberField;

    private string modelDescriptionField;

    private string modelNameField;

    private string modelURLField;

    private string uDNField;

    private rootDeviceDeviceService[] serviceListField;

    private X_RhapsodyExtension x_RhapsodyExtensionField;

    private string x_QPlay_SoftwareCapabilityField;

    private rootDeviceDeviceIconList iconListField;

    /// <remarks/>
    public string deviceType
    {
        get
        {
            return this.deviceTypeField;
        }
        set
        {
            this.deviceTypeField = value;
        }
    }

    /// <remarks/>
    public string friendlyName
    {
        get
        {
            return this.friendlyNameField;
        }
        set
        {
            this.friendlyNameField = value;
        }
    }

    /// <remarks/>
    public string manufacturer
    {
        get
        {
            return this.manufacturerField;
        }
        set
        {
            this.manufacturerField = value;
        }
    }

    /// <remarks/>
    public string manufacturerURL
    {
        get
        {
            return this.manufacturerURLField;
        }
        set
        {
            this.manufacturerURLField = value;
        }
    }

    /// <remarks/>
    public string modelNumber
    {
        get
        {
            return this.modelNumberField;
        }
        set
        {
            this.modelNumberField = value;
        }
    }

    /// <remarks/>
    public string modelDescription
    {
        get
        {
            return this.modelDescriptionField;
        }
        set
        {
            this.modelDescriptionField = value;
        }
    }

    /// <remarks/>
    public string modelName
    {
        get
        {
            return this.modelNameField;
        }
        set
        {
            this.modelNameField = value;
        }
    }

    /// <remarks/>
    public string modelURL
    {
        get
        {
            return this.modelURLField;
        }
        set
        {
            this.modelURLField = value;
        }
    }

    /// <remarks/>
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
    [System.Xml.Serialization.XmlArrayItem("service", IsNullable = false)]
    public rootDeviceDeviceService[] serviceList
    {
        get
        {
            return this.serviceListField;
        }
        set
        {
            this.serviceListField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("X_Rhapsody-Extension", Namespace = "http://www.real.com/rhapsody/xmlns/upnp-1-0")]
    public X_RhapsodyExtension X_RhapsodyExtension
    {
        get
        {
            return this.x_RhapsodyExtensionField;
        }
        set
        {
            this.x_RhapsodyExtensionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement(Namespace = "http://www.tencent.com")]
    public string X_QPlay_SoftwareCapability
    {
        get
        {
            return this.x_QPlay_SoftwareCapabilityField;
        }
        set
        {
            this.x_QPlay_SoftwareCapabilityField = value;
        }
    }

    /// <remarks/>
    public rootDeviceDeviceIconList iconList
    {
        get
        {
            return this.iconListField;
        }
        set
        {
            this.iconListField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
public partial class rootDeviceDeviceService
{

    private string serviceTypeField;

    private string serviceIdField;

    private string controlURLField;

    private string eventSubURLField;

    private string sCPDURLField;

    /// <remarks/>
    public string serviceType
    {
        get
        {
            return this.serviceTypeField;
        }
        set
        {
            this.serviceTypeField = value;
        }
    }

    /// <remarks/>
    public string serviceId
    {
        get
        {
            return this.serviceIdField;
        }
        set
        {
            this.serviceIdField = value;
        }
    }

    /// <remarks/>
    public string controlURL
    {
        get
        {
            return this.controlURLField;
        }
        set
        {
            this.controlURLField = value;
        }
    }

    /// <remarks/>
    public string eventSubURL
    {
        get
        {
            return this.eventSubURLField;
        }
        set
        {
            this.eventSubURLField = value;
        }
    }

    /// <remarks/>
    public string SCPDURL
    {
        get
        {
            return this.sCPDURLField;
        }
        set
        {
            this.sCPDURLField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.real.com/rhapsody/xmlns/upnp-1-0")]
[System.Xml.Serialization.XmlRoot("X_Rhapsody-Extension", Namespace = "http://www.real.com/rhapsody/xmlns/upnp-1-0", IsNullable = false)]
public partial class X_RhapsodyExtension
{

    private string deviceIDField;

    private X_RhapsodyExtensionDeviceCapabilities deviceCapabilitiesField;

    /// <remarks/>
    public string deviceID
    {
        get
        {
            return this.deviceIDField;
        }
        set
        {
            this.deviceIDField = value;
        }
    }

    /// <remarks/>
    public X_RhapsodyExtensionDeviceCapabilities deviceCapabilities
    {
        get
        {
            return this.deviceCapabilitiesField;
        }
        set
        {
            this.deviceCapabilitiesField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.real.com/rhapsody/xmlns/upnp-1-0")]
public partial class X_RhapsodyExtensionDeviceCapabilities
{

    private X_RhapsodyExtensionDeviceCapabilitiesInteractionPattern interactionPatternField;

    /// <remarks/>
    public X_RhapsodyExtensionDeviceCapabilitiesInteractionPattern interactionPattern
    {
        get
        {
            return this.interactionPatternField;
        }
        set
        {
            this.interactionPatternField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.real.com/rhapsody/xmlns/upnp-1-0")]
public partial class X_RhapsodyExtensionDeviceCapabilitiesInteractionPattern
{

    private string typeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
public partial class rootDeviceDeviceIconList
{

    private rootDeviceDeviceIconListIcon iconField;

    /// <remarks/>
    public rootDeviceDeviceIconListIcon icon
    {
        get
        {
            return this.iconField;
        }
        set
        {
            this.iconField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:schemas-upnp-org:device-1-0")]
public partial class rootDeviceDeviceIconListIcon
{

    private string mimetypeField;

    private byte widthField;

    private byte heightField;

    private byte depthField;

    private string urlField;

    /// <remarks/>
    public string mimetype
    {
        get
        {
            return this.mimetypeField;
        }
        set
        {
            this.mimetypeField = value;
        }
    }

    /// <remarks/>
    public byte width
    {
        get
        {
            return this.widthField;
        }
        set
        {
            this.widthField = value;
        }
    }

    /// <remarks/>
    public byte height
    {
        get
        {
            return this.heightField;
        }
        set
        {
            this.heightField = value;
        }
    }

    /// <remarks/>
    public byte depth
    {
        get
        {
            return this.depthField;
        }
        set
        {
            this.depthField = value;
        }
    }

    /// <remarks/>
    public string url
    {
        get
        {
            return this.urlField;
        }
        set
        {
            this.urlField = value;
        }
    }
}

