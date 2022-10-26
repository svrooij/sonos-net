using Sonos.Base.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Services
{
    public partial class MusicServicesService
    {
        public partial class ListAvailableServicesResponse
        {
            private MusicServiceCollection _musicServiceCollection;
            public MusicService[] MusicServices
            {
                get
                {
                    if (_musicServiceCollection is null)
                    {
                        _musicServiceCollection = SoapFactory.ParseEmbeddedXml<MusicServiceCollection>(this.AvailableServiceDescriptorList);
                    }
                    return _musicServiceCollection.Services;
                }
            }
        }
    }
}
