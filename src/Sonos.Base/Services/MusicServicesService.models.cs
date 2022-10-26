using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Services
{
    public partial class MusicServicesService
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        [System.Xml.Serialization.XmlRoot("Services", Namespace = "", IsNullable = false)]
        public partial class MusicServiceCollection
        {

            private MusicService[] serviceField;

            private byte schemaVersionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElement("Service")]
            public MusicService[] Services
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public byte SchemaVersion
            {
                get
                {
                    return this.schemaVersionField;
                }
                set
                {
                    this.schemaVersionField = value;
                }
            }
        }

        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public partial class MusicService
        {

            private ServicesServicePolicy policyField;

            private ServicesServicePresentation presentationField;

            private ServicesServiceManifest manifestField;

            private ushort idField;

            private string nameField;

            private decimal versionField;

            private string uriField;

            private string secureUriField;

            private string containerTypeField;

            private uint capabilitiesField;

            /// <remarks/>
            public ServicesServicePolicy Policy
            {
                get
                {
                    return this.policyField;
                }
                set
                {
                    this.policyField = value;
                }
            }

            /// <remarks/>
            public ServicesServicePresentation Presentation
            {
                get
                {
                    return this.presentationField;
                }
                set
                {
                    this.presentationField = value;
                }
            }

            /// <remarks/>
            public ServicesServiceManifest Manifest
            {
                get
                {
                    return this.manifestField;
                }
                set
                {
                    this.manifestField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public ushort Id
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
            [System.Xml.Serialization.XmlAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public decimal Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string Uri
            {
                get
                {
                    return this.uriField;
                }
                set
                {
                    this.uriField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string SecureUri
            {
                get
                {
                    return this.secureUriField;
                }
                set
                {
                    this.secureUriField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string ContainerType
            {
                get
                {
                    return this.containerTypeField;
                }
                set
                {
                    this.containerTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public uint Capabilities
            {
                get
                {
                    return this.capabilitiesField;
                }
                set
                {
                    this.capabilitiesField = value;
                }
            }
        }

        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public partial class ServicesServicePolicy
        {

            private string authField;

            private ushort pollIntervalField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string Auth
            {
                get
                {
                    return this.authField;
                }
                set
                {
                    this.authField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public ushort PollInterval
            {
                get
                {
                    return this.pollIntervalField;
                }
                set
                {
                    this.pollIntervalField = value;
                }
            }
        }

        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public partial class ServicesServicePresentation
        {

            private ServicesServicePresentationStrings stringsField;

            private ServicesServicePresentationPresentationMap presentationMapField;

            /// <remarks/>
            public ServicesServicePresentationStrings Strings
            {
                get
                {
                    return this.stringsField;
                }
                set
                {
                    this.stringsField = value;
                }
            }

            /// <remarks/>
            public ServicesServicePresentationPresentationMap PresentationMap
            {
                get
                {
                    return this.presentationMapField;
                }
                set
                {
                    this.presentationMapField = value;
                }
            }
        }

        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public partial class ServicesServicePresentationStrings
        {

            private byte versionField;

            private string uriField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public byte Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string Uri
            {
                get
                {
                    return this.uriField;
                }
                set
                {
                    this.uriField = value;
                }
            }
        }

        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public partial class ServicesServicePresentationPresentationMap
        {

            private byte versionField;

            private string uriField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public byte Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string Uri
            {
                get
                {
                    return this.uriField;
                }
                set
                {
                    this.uriField = value;
                }
            }
        }

        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public partial class ServicesServiceManifest
        {

            private byte versionField;

            private string uriField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public byte Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttribute()]
            public string Uri
            {
                get
                {
                    return this.uriField;
                }
                set
                {
                    this.uriField = value;
                }
            }
        }


    }
}
