using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Services
{
    public partial class AVTransportService
    {
        public Task<bool> PlayAsync(CancellationToken cancellationToken = default) => PlayAsync(new PlayRequest { Speed = "1" }, cancellationToken);

        [Obsolete("Use PlayAsync instead")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Task<bool> Play(CancellationToken cancellationToken = default) => PlayAsync(cancellationToken);
    }
}
