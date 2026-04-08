using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace Sonos.Base.Soap;

internal class QuoteEscapingXmlWriter : XmlTextWriter
{
    public QuoteEscapingXmlWriter(Stream stream, System.Text.Encoding encoding)
        : base(stream, encoding) { }

    
    public override void WriteString(string? text)
    {
        // Encode quotes in text content
        if (text?.IndexOf('<') > -1)
        {
            text = text
                .Replace("\"", "&quot;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
            base.WriteRaw(text);
        } else
        {
            base.WriteString(text);
        }
        //base.WriteString(text);
    }

    public override void WriteValue(bool value)
    {
        base.WriteRaw(value ? "1" : "0");
    }

    // This is overriden to not close the stream when the writer is closed, as the stream will be disposed by the caller.
    public override void Close()
    {
        //base.Close();
    }

    public override void WriteStartDocument()
    {
        // Do nothing - omit XML declaration
    }

    public override void WriteStartDocument(bool standalone)
    {
        // Do nothing - omit XML declaration
    }
}