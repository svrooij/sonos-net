using Sonos.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Events
{
    internal class SonosEventSubscription
    {
        public SonosEventSubscription(string uuid, SonosService service, Uri eventUri, string sid, Action<IServiceEvent> callback)
        {
            Uuid = uuid;
            Service = service;
            EventUri = eventUri;
            Sid = sid;
            Callback = callback;
        }

        public string Uuid { get; init; }
        public SonosService Service { get; init; }
        public Uri EventUri { get; init; }
        public string Sid { get; init; }
        public Action<IServiceEvent> Callback { get; init;}
    }
}
