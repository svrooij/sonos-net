using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Services
{
    public partial class AVTransportService
    {
        public Task<bool> Play(CancellationToken cancellationToken = default) => Play(new PlayRequest { Speed = "1" }, cancellationToken);

        public static class TransportState
        {
            public const string Stopped = "STOPPED";
            public const string Playing = "PLAYING";
            public const string Paused = "PAUSED_PLAYBACK";
            public const string Transitioning = "TRANSITIONING";

            public static bool? IsPlaying(string? state)
            {
                if (string.IsNullOrEmpty(state))
                {
                    return null;
                }
                return state == Playing || state == Transitioning;
            }
        }
    }
}
