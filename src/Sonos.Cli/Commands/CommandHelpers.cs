﻿using System.Text.Json;

namespace Sonos.Cli.Commands;

internal static class CommandHelpers
{
    internal static void WriteJson(object? o, bool intended = true)
    {
        if (o is not null)
        {
            Console.WriteLine(JsonSerializer.Serialize(o, new JsonSerializerOptions { WriteIndented = intended, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault }));
        }
    }
}