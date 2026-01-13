using System.CommandLine;

using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Microsoft.Extensions.Logging;

using Sonos.Base;

namespace Sonos.Cli.Commands;

public record class VolumeCommandOptions : SonosOptions
{
    [Option("--newVolume", "Set the volume")]
    public int? NewVolume { get; set; }
    [Option("--channel", "Get/set volume for different channel than 'Master'")]
    public string Channel { get; set; } = "Master";
}

public class VolumeCommandHandler : IAsyncCommandHandler
{
    private readonly VolumeCommandOptions _options;
    private readonly ILogger<VolumeCommandHandler> _logger;
    private readonly ISonosServiceProvider _sonosServiceProvider;

    public VolumeCommandHandler(VolumeCommandOptions options, ILogger<VolumeCommandHandler> logger, ISonosServiceProvider sonosServiceProvider)
    {
        _options = options;
        _logger = logger;
        _sonosServiceProvider = sonosServiceProvider;
    }

    public async Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Execute volume command {host}", _options.Host);
        var sonos = new SonosDevice(new SonosDeviceOptions(new Uri($"http://{_options.Host}:1400/"), _sonosServiceProvider));

        if (_options.NewVolume.HasValue)
        {
            await sonos.RenderingControlService.SetVolume(new Base.Services.RenderingControlService.SetVolumeRequest { Channel = _options.Channel, DesiredVolume = _options.NewVolume.Value, InstanceID = 0 });
            CommandHelpers.WriteJson(_options.NewVolume.Value);
        }
        else
        {
            CommandHelpers.WriteJson(await sonos.RenderingControlService.GetVolume(_options.Channel));
        }
        return 0;
    }


}