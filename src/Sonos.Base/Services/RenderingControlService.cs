namespace Sonos.Base.Services
{
    public partial class RenderingControlService
    {
        public const string ChannelMaster = "Master";
        public const string EQTypeDialogLevel = "DialogLevel";

        public const string EQTypeHeightChannelLevel = "HeightChannelLevel";

        public const string EQTypeMusicSurroundLevel = "MusicSurroundLevel";

        public const string EQTypeNightMode = "NightMode";

        public const string EQTypeSubGain = "SubGain";

        public const string EQTypeSurroundEnable = "SurroundEnable";

        public const string EQTypeSurroundLevel = "SurroundLevel";

        public const string EQTypeSurroundMode = "SurroundMode";

        public async Task<bool> GetEQBoolAsync(string eqType, CancellationToken cancellationToken = default)
        {
            var eqResponse = await GetEQIntAsync(eqType, cancellationToken);
            return eqResponse == 1;
        }

        public Task<bool> GetEQDialogLevelAsync(CancellationToken cancellationToken = default) => GetEQBoolAsync(EQTypeDialogLevel, cancellationToken);

        public Task<int> GetEQHeightChannelLevelAsync(CancellationToken cancellationToken = default) => GetEQIntAsync(EQTypeHeightChannelLevel, cancellationToken);

        public async Task<int> GetEQIntAsync(string eqType, CancellationToken cancellationToken = default)
        {
            var eqResponse = await GetEQAsync(new GetEQRequest { EQType = eqType, InstanceID = 0 }, cancellationToken);
            return eqResponse.CurrentValue;
        }

        public Task<int> GetEQMusicSurroundLevelAsync(CancellationToken cancellationToken = default) => GetEQIntAsync(EQTypeMusicSurroundLevel, cancellationToken);

        public Task<bool> GetEQNightModeAsync(CancellationToken cancellationToken = default) => GetEQBoolAsync(EQTypeNightMode, cancellationToken);

        public Task<int> GetEQSubGainAsync(CancellationToken cancellationToken = default) => GetEQIntAsync(EQTypeSubGain, cancellationToken);

        public Task<bool> GetEQSurroundEnabledAsync(CancellationToken cancellationToken = default) => GetEQBoolAsync(EQTypeSurroundEnable, cancellationToken);

        public Task<int> GetEQSurroundLevelAsync(CancellationToken cancellationToken = default) => GetEQIntAsync(EQTypeSurroundLevel, cancellationToken);

        public Task<bool> GetEQSurroundModeAsync(CancellationToken cancellationToken = default) => GetEQBoolAsync(EQTypeSurroundMode, cancellationToken);

        public async Task<int> GetVolumeAsync(string channel = ChannelMaster, CancellationToken cancellationToken = default)
        {
            var resp = await GetVolumeAsync(new GetVolumeRequest { InstanceID = 0, Channel = channel }, cancellationToken);
            return resp.CurrentVolume;
        }
    }
}