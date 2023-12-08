using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Music
{
    public class MusicClientException : Exception
    {
        public MusicClientException(string? message, string faultCode) : base(message) { }
        public string FaultCode { get; init; }

    }
}
