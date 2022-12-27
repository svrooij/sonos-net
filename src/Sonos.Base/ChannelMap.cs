using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base
{
    public class ChannelMapInt : Dictionary<string, int>
    {
        public ChannelMapInt(int capacity) : base(capacity) { }
        public new void Add(string channel, int value) => base.Add(channel, value);
    }

    public class ChannelMapBool : Dictionary<string, bool>
    {
        public ChannelMapBool(int capacity) : base(capacity) { }
        public new void Add(string channel, bool value) => base.Add(channel, value);
    }
}
