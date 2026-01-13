using System.CommandLine;

using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Microsoft.Extensions.Logging;

using Sonos.Base;

namespace Sonos.Cli.Commands;

public enum ControlAction
{
    Stop = 0,
    Play = 1,
    Pause = 2,
    Next = 3,
    Previous = 4,
}

[Verb<ControlCommandHandler>("control", Description = "Control Sonos speaker playback")]
public record class ControlCommandOptions : SonosOptions
{
    [Argument(Description = "Action to perform on the speaker")]
    public ControlAction Action { get; init; }
}

public class ControlCommandHandler : IAsyncCommandHandler
{
    private readonly ControlCommandOptions _options;
    private readonly ILogger _logger;
    private readonly ISonosServiceProvider _sonosServiceProvider;

    public ControlCommandHandler(
        ControlCommandOptions options,
        ILogger<ControlCommandHandler> logger,
        ISonosServiceProvider sonosServiceProvider)
    {
        _options = options;
        _logger = logger;
        _sonosServiceProvider = sonosServiceProvider;
    }

    public async Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Execute control command {ip} {action}", _options.Host, _options.Action);
        var sonos = new SonosDevice(new SonosDeviceOptions(new Uri($"http://{_options.Host}:1400/"), _sonosServiceProvider));
        switch (_options.Action)
        {
            case ControlAction.Stop:
                await sonos.Stop(cancellationToken);
                break;

            case ControlAction.Play:
                await sonos.Play(cancellationToken);
                break;

            case ControlAction.Pause:
                await sonos.Pause(cancellationToken);
                break;

            case ControlAction.Next:
                await sonos.Next(cancellationToken);
                break;

            case ControlAction.Previous:
                await sonos.Previous(cancellationToken);
                break;
        }
        return 0;
    }
}