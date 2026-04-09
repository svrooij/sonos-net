using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Services
{
    public partial class RenderingControlService
    {
        public async Task<int> GetVolume(string channel = "Master", CancellationToken cancellationToken = default)
        {
            var resp = await GetVolume(new GetVolumeRequest { InstanceID = 0, Channel = channel }, cancellationToken);
            return resp.CurrentVolume;
        }
    }
}
