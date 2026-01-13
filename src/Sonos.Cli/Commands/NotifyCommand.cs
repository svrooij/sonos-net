using System.CommandLine;

using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Microsoft.Extensions.Logging;

using Sonos.Base;

namespace Sonos.Cli.Commands;

[Verb<NotifyCommandHandler>("notify", Description = "Play a notification on your speaker")]
public record class NotifyCommandOptions : SonosOptions
{
    [Argument(Description = "Uri of the mp3 you want to play")]
    public required Uri Sound { get; init; }
    [Option("--volume", Description = "The Sound volume")]
    public int Volume { get; set; } = 25;
}

public class NotifyCommandHandler : IAsyncCommandHandler
{
    private readonly NotifyCommandOptions _options;
    private readonly ILogger<NotifyCommandHandler> _logger;
    private readonly ISonosServiceProvider _sonosServiceProvider;

    public NotifyCommandHandler(
        NotifyCommandOptions options,
        ILogger<NotifyCommandHandler> logger,
        ISonosServiceProvider sonosServiceProvider)
    {
        _options = options;
        _logger = logger;
        _sonosServiceProvider = sonosServiceProvider;
    }

    public async Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Play notification on {ip} {sound}", _options.Host, _options.Sound);
        var sonos = new SonosDevice(new SonosDeviceOptions(new Uri($"http://{_options.Host}:1400/"), _sonosServiceProvider));

        await sonos.QueueNotification(new Base.NotificationOptions(_options.Sound!, _options.Volume));
        return 0;
    }

    
}