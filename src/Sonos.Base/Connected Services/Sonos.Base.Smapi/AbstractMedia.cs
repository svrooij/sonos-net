using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Smapi
{
    public abstract partial class AbstractMedia
    {
        private bool canPlayField;

        private bool canPlayFieldSpecified;
        private albumArtUrl albumArtURIField;
        public override string ToString()
        {
            return $"Media id: {id} title: {title}"; 
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public bool canPlay
        {
            get
            {
                return this.canPlayField;
            }
            set
            {
                this.canPlayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool canPlaySpecified
        {
            get
            {
                return this.canPlayFieldSpecified;
            }
            set
            {
                this.canPlayFieldSpecified = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public albumArtUrl albumArtURI
        {
            get
            {
                return this.albumArtURIField;
            }
            set
            {
                this.albumArtURIField = value;
            }
        }
    }
}
