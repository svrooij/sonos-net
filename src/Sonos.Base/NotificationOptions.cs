using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base
{
    public record NotificationOptions (Uri SoundUri, int Volume = 25, bool OnlyWhenPlaying = false);

}
