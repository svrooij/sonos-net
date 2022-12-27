using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Events;

internal static class DictionaryExtensions
{
    internal static bool? TryGetBool(this Dictionary<string, string> dic, string key)
    {
        return dic.ContainsKey(key) && bool.TryParse(dic[key], out var value) ? value : null;
    }

    internal static int? TryGetInt(this Dictionary<string, string> dic, string key)
    {
        return dic.ContainsKey(key) && int.TryParse(dic[key], out var value) ? value : null;
    }

    private static string[] DefaultChannels => new[] { "Master", "LF", "RF" }; 

    internal static ChannelMapBool? TryGetMapBool(this Dictionary<string, string> dic, string key, params string[]? channels)
    {
        if (channels is null || channels.Length == 0)
        {
            channels = DefaultChannels;
        }
        var result = new ChannelMapBool(channels.Length);
        foreach (var channel in channels)
        {
            result.Add(channel, dic.TryGetBool($"{key}{channel}") == true);
        }
        return result;
    }

    internal static ChannelMapInt? TryGetMapInt(this Dictionary<string, string> dic, string key, params string[]? channels)
    {
        if (channels is null || channels.Length == 0)
        {
            channels = DefaultChannels;
        }
        var result = new ChannelMapInt(channels.Length);
        foreach (var channel in channels)
        {
            var value = dic.TryGetInt($"{key}{channel}");
            if(value is not null)
            {
                result.Add(channel, value.Value);
            }
        }
        return result;
    }

    internal static string? TryGetString(this Dictionary<string, string> dic, string key)
    {
        return dic.ContainsKey(key) ? dic[key] : null;
    }
}
