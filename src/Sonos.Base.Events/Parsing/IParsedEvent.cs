using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Events.Parsing
{
    //internal interface IParsedEvent
    //{
    //    public object? GetEvent();
    //}
    internal interface IParsedEvent<TEvent> where TEvent : class
    {
        //public Dictionary<string, string>? EventProperties();
        public TEvent? GetEvent();
    }
}
