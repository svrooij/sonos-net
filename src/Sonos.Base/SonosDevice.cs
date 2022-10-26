using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base
{
    public partial class SonosDevice
    {
        private readonly Uri deviceUri;
        private readonly HttpClient httpClient;
        public string Uuid { get; private set; }

        public SonosDevice(Uri deviceUri, string? uuid, HttpClient? httpClient)
        {
            this.deviceUri = deviceUri;
            this.httpClient = httpClient ?? new HttpClient();
            this.Uuid = uuid ?? Guid.NewGuid().ToString();
        }
    }
}
