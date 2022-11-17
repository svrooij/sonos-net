using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Services
{
    public partial class AVTransportService
    {
        public Task<bool> Play(CancellationToken cancellationToken = default) => Play(new PlayRequest {  Speed = "1" }, cancellationToken);
    }
}
