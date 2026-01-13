using System.CommandLine;

using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Microsoft.Extensions.Logging;

using Sonos.Base;

namespace Sonos.Cli.Commands;

public enum SonosInfo
{
    Position = 1,
    Transport = 2,

    //Volume = 3,
    Media = 4,
}

[Verb<InfoCommandHandler>("info", Description = "Get details from your sonos speakers")]
public record class InfoCommandOptions : SonosOptions
{
    [Argument(Description = "What info do you want")]
    public SonosInfo Info { get; init; }
}

public class InfoCommandHandler : IAsyncCommandHandler
{
    private readonly InfoCommandOptions _options;
    private readonly ILogger _logger;
    private readonly ISonosServiceProvider _sonosServiceProvider;

    public InfoCommandHandler(InfoCommandOptions options, ILogger<InfoCommandHandler> logger, ISonosServiceProvider sonosServiceProvider)
    {
        _options = options;
        _logger = logger;
        _sonosServiceProvider = sonosServiceProvider;
    }

    public async Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Execute info command {host} {info}", _options.Host, _options.Info);
        var sonos = new SonosDevice(new SonosDeviceOptions(new Uri($"http://{_options.Host}:1400/"), _sonosServiceProvider));
        switch (_options.Info)
        {
            case SonosInfo.Position:
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetPositionInfo(cancellationToken));
                break;

            case SonosInfo.Transport:
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetTransportInfo(cancellationToken));
                break;
            //case SonosInfo.Volume:
            //CommandHelpers.WriteJson(await sonos.RenderingControlService.GetVolume());
            //break;
            case SonosInfo.Media:
                CommandHelpers.WriteJson(await sonos.AVTransportService.GetMediaInfo(cancellationToken));
                break;
        }
        return 0;
    }
}