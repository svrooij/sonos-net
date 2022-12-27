using Sonos.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base
{
    public interface ISonosEventBus
    {
        public Task<bool> Subscribe(string uuid, SonosService service, Uri eventEndpoint, Action<IServiceEvent> callback, CancellationToken cancellationToken = default);
        public Task<bool> RenewSubscription(string uuid, SonosService service, CancellationToken cancellationToken = default);
        public Task<bool> Unsubscribe(string uuid, SonosService service, CancellationToken cancellationToken = default);
    }
}
