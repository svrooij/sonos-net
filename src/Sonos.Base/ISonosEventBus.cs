using Sonos.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base
{
    /// <summary>
    /// Interface to allow receiving events from sonos speakers
    /// </summary>
    /// <remarks>Register to dependency injection</remarks>
    public interface ISonosEventBus
    {
        /// <summary>
        /// Subscribe for events from this Sonos service
        /// </summary>
        /// <param name="uuid">UUID of the player</param>
        /// <param name="service">Sonos Service that wants the events</param>
        /// <param name="eventEndpoint">Endpoint of the events</param>
        /// <param name="callback">Action that is called when a new event is parsed</param>
        /// <param name="cancellationToken">CancellationToken to cancel the create subscription request</param>
        /// <returns>true when the subscription was made successfully</returns>
        public Task<bool> Subscribe(string uuid, SonosService service, Uri eventEndpoint, Action<IServiceEvent> callback, CancellationToken cancellationToken = default);

        /// <summary>
        /// Renew a subscription to a sonos service
        /// </summary>
        /// <param name="uuid">UUID of the player</param>
        /// <param name="service">Sonos Service that wants the events</param>
        /// <param name="cancellationToken">CancellationToken to cancel the renew subscription request</param>
        /// <returns>true when the subscription was renewed successfully</returns>
        public Task<bool> RenewSubscription(string uuid, SonosService service, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancel an existing subscription, so the events will stop
        /// </summary>
        /// <param name="uuid">UUID of the player</param>
        /// <param name="service">Sonos Service you subscribed to</param>
        /// <param name="cancellationToken">CancellationToken to cancel the cancel subscription request</param>
        /// <returns>true when the unsubscription request was successfull</returns>
        public Task<bool> Unsubscribe(string uuid, SonosService service, CancellationToken cancellationToken = default);
    }
}
