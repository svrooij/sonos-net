using System.Text;

namespace Sonos.Base.Soap;

internal class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}
