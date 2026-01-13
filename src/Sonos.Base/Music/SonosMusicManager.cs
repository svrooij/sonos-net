using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using Sonos.Base.Services;

namespace Sonos.Base.Music;
public class SonosMusicManager
{
    private readonly SonosManager _sonosManager;
    private readonly ILogger<SonosMusicManager> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IMemoryCache? _memoryCache;
    private readonly IHttpClientFactory _httpClientFactory;

    private const string GENERIC_DEVICE_ID = "RINCON_00000000000001400";
    private const string ENABLED_MUSIC_SERVICES_KEY = "svr.sonos_music_services";

    public const string MUSIC_SERVICE_CLIENT_NAME = "SonosMusicServiceClient";
    public SonosMusicManager(SonosManager sonosManager, ILogger<SonosMusicManager> logger, IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory, IMemoryCache? memoryCache = null)
    {
        _sonosManager = sonosManager;
        _logger = logger;
        _memoryCache = memoryCache;
        _httpClientFactory = httpClientFactory;
        _loggerFactory = loggerFactory;
    }

    public async Task<IEnumerable<Sonos.Base.Services.MusicServicesService.MusicService>?> GetAllMusicServicesAsync(CancellationToken cancellationToken)
    {
        if (_memoryCache == null)
        {
            return await FetchMusicServicesAsync(cancellationToken);
        }
        return await _memoryCache.GetOrCreateAsync("musicservices", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return await FetchMusicServicesAsync(cancellationToken);
        });
    }

    public async Task<IEnumerable<Sonos.Base.Services.MusicServicesService.MusicService>?> GetEnabledMusicServicesAsync(CancellationToken cancellationToken)
    {
        var enabledServices = _sonosManager.MusicServers?.Select(s => s.ServiceId).ToHashSet();
        if (enabledServices == null || enabledServices.Count == 0)
        {
            _logger.LogWarning("No enabled music services found in SonosManager MusicServers.");
            return null;
        }
        var allServices = await GetAllMusicServicesAsync(cancellationToken);
        if (allServices == null)
        {
            _logger.LogWarning("No music services available to filter for device {deviceId}", GENERIC_DEVICE_ID);
            return null;
        }
        return allServices.Where(s => enabledServices.Contains(s.Id)).OrderBy(m => m.Name);

        //var device = _sonosManager.GetSonosDevice(GENERIC_DEVICE_ID);
        //if (device == null)
        //{
        //    _logger.LogWarning("Generic device with ID {deviceId} not found. Cannot filter enabled music services.", GENERIC_DEVICE_ID);
        //    return null;
        //}

        //try
        //{
        //    var enabledServices = await device.SystemPropertiesService.GetString(new Services.SystemPropertiesService.GetStringRequest
        //    {
        //        VariableName = ENABLED_MUSIC_SERVICES_KEY
        //    }, cancellationToken);
        //    if (string.IsNullOrWhiteSpace(enabledServices?.StringValue))
        //    {
        //        _logger.LogWarning("No enabled music services found in system properties for device {deviceId}", GENERIC_DEVICE_ID);
        //        return null;
        //    }
        //    var enabledServiceIds = enabledServices.StringValue.Split(',').Select(id => ushort.Parse(id)).ToHashSet();
        //    var allServices = await GetAllMusicServicesAsync(cancellationToken);
        //    if (allServices == null)
        //    {
        //        _logger.LogWarning("No music services available to filter for device {deviceId}", GENERIC_DEVICE_ID);
        //        return null;
        //    }
        //    return allServices.Where(s => enabledServiceIds.Contains(s.Id)).OrderBy(m => m.Name);
        //} catch (Exception ex)
        //{
        //    _logger.LogError(ex, "Error fetching enabled music services from device {deviceId}", GENERIC_DEVICE_ID);
        //    return null;
        //}
    }

    private async Task<IEnumerable<Sonos.Base.Services.MusicServicesService.MusicService>?> FetchMusicServicesAsync(CancellationToken cancellationToken)
    {
        var device = _sonosManager.GetSonosDevice(GENERIC_DEVICE_ID);
        if (device == null)
        {
            _logger.LogWarning("Generic device with ID {deviceId} not found. Cannot fetch music services.", GENERIC_DEVICE_ID);
            return null;
        }
        var musicService = await device.MusicServicesService.ListAvailableServices(cancellationToken);
        return musicService?.MusicServices?.OrderBy(m => m.Name);
    }

    public async Task<SonosMusicServiceClient> GetClientForServiceAsync(ushort serviceId, CancellationToken cancellationToken)
    {
        var services = await GetAllMusicServicesAsync(cancellationToken);
        var service = services!.First(s => s.Id == serviceId);

        if (service.Policy.Authentication == Services.MusicServicesService.MusicServiceAuthentication.UserId)
        {
            throw new NotSupportedException("UserId authentication is not supported yet.");
        }
        SonosMusicServiceClientOptions options = new SonosMusicServiceClientOptions
        {
            Name = service.Name,
            Id = service.Id,
            Authentication = service.Policy.Authentication,
            BaseUri = new Uri(service.SecureUri),
        };

        if (service.Policy.Authentication == Services.MusicServicesService.MusicServiceAuthentication.DeviceLink
            || service.Policy.Authentication == Services.MusicServicesService.MusicServiceAuthentication.AppLink)
        {
            // Set authentication properties
            var device = _sonosManager.GetSonosDevice(GENERIC_DEVICE_ID);
            if (device == null)
            {
                throw new InvalidOperationException($"Generic device with ID {GENERIC_DEVICE_ID} not found. Cannot create music service client for service {service.Name} ({service.Id}).");
            }

            var household = await device.DevicePropertiesService.GetHouseholdID(cancellationToken);
            options.HouseholdId = household!.CurrentHouseholdID;

            //var auth = await device.SystemPropertiesService.GetMusicServiceAuth(service.Id, cancellationToken);
            //options.Key = auth?.Key;
            //options.AuthToken = auth?.Token;

            //options.SaveNewToken = device.SystemPropertiesService.SaveMusicServiceAuth;

            var serviceAuth = _sonosManager.MusicServers?.FirstOrDefault(s => s.ServiceId == service.Id);
            if (serviceAuth is not null)
            {
                options.Key = serviceAuth.Key;
                options.AuthToken = serviceAuth.Token;
            }
        }

        return new SonosMusicServiceClient(
                _httpClientFactory.CreateClient(MUSIC_SERVICE_CLIENT_NAME),
                _loggerFactory.CreateLogger($"Sonos.Base.Music.SonosMusicServiceClient{serviceId}"),
                options);
    }
}
