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

        public async Task<bool> GetEQBool(string eqType, CancellationToken cancellationToken = default)
        {
            var eqResponse = await GetEQInt(eqType, cancellationToken);
            return eqResponse == 1;
        }

        public Task<bool> GetEQDialogLevel(CancellationToken cancellationToken = default) => GetEQBool(EQTypeDialogLevel, cancellationToken);

        public Task<int> GetEQHeightChannelLevel(CancellationToken cancellationToken = default) => GetEQInt(EQTypeHeightChannelLevel, cancellationToken);

        public async Task<int> GetEQInt(string eqType, CancellationToken cancellationToken = default)
        {
            var eqResponse = await GetEQ(new GetEQRequest { EQType = eqType, InstanceID = 0 }, cancellationToken);
            return eqResponse.CurrentValue;
        }

        public Task<int> GetEQMusicSurroundLevel(CancellationToken cancellationToken = default) => GetEQInt(EQTypeMusicSurroundLevel, cancellationToken);

        public Task<bool> GetEQNightMode(CancellationToken cancellationToken = default) => GetEQBool(EQTypeNightMode, cancellationToken);

        public Task<int> GetEQSubGain(CancellationToken cancellationToken = default) => GetEQInt(EQTypeSubGain, cancellationToken);

        public Task<bool> GetEQSurroundEnabled(CancellationToken cancellationToken = default) => GetEQBool(EQTypeSurroundEnable, cancellationToken);

        public Task<int> GetEQSurroundLevel(CancellationToken cancellationToken = default) => GetEQInt(EQTypeSurroundLevel, cancellationToken);

        public Task<bool> GetEQSurroundMode(CancellationToken cancellationToken = default) => GetEQBool(EQTypeSurroundMode, cancellationToken);

        public async Task<int> GetVolume(string channel = ChannelMaster, CancellationToken cancellationToken = default)
        {
            var resp = await GetVolume(new GetVolumeRequest { InstanceID = 0, Channel = channel }, cancellationToken);
            return resp.CurrentVolume;
        }
    }
}