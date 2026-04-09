using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace Sonos.Base.Services;
public partial class SystemPropertiesService
{
    private const string ENABLED_MUSIC_SERVICES_KEY = "svr.sonos_music_services";
    private const string MUSIC_SERVICE_FORMAT = "svr.music_{0}_{1}";
    private const string MUSIC_SERVICE_AUTH = "auth";
    private const string MUSIC_SERVICE_TOKEN = "token";

    /// <summary>
    /// Enables a music service by its identifier if it is not already enabled.
    /// </summary>
    /// <remarks>This method retrieves the current set of enabled music services and adds the specified
    /// service if it is not already present. The updated set of enabled services is then saved.</remarks>
    /// <param name="serviceId">The unique identifier of the music service to enable.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. If the operation is canceled, the task will be terminated.</param>
    /// <returns><see langword="true"/> if the music service was successfully enabled or was already enabled; otherwise, <see
    /// langword="false"/>.</returns>
    public async Task<bool> AddEnabledMusicService(ushort serviceId, CancellationToken cancellationToken)
    {
        var currentServices = await GetMusicServicesEnabled(cancellationToken) ?? new HashSet<ushort>();
        if (currentServices.Contains(serviceId))
        {
            return true; // Already enabled
        }
        currentServices.Add(serviceId);
        return await SetMusicServicesEnabled(currentServices, cancellationToken);
    }



    /// <summary>
    /// Asynchronously retrieves the string value of a specified variable, returning null if an error occurs.
    /// </summary>
    /// <remarks>This method logs any exceptions that occur during the retrieval process and safely returns
    /// <see langword="null"/>  instead of propagating the exception. Use this method when error tolerance is
    /// required.</remarks>
    /// <param name="variableName">The name of the variable whose string value is to be retrieved. Cannot be null or empty.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The string value of the specified variable, or <see langword="null"/> if the retrieval fails or the value is not
    /// found.</returns>
    public async Task<string?> GetStringSafe(string variableName, CancellationToken cancellationToken)
    {
        try
        {
            var result = await GetString(new GetStringRequest
            {
                VariableName = variableName
            }, cancellationToken);
            return result?.StringValue;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Error retrieving system property {variableName}", variableName);
            return null;
        }
    }

    /// <summary>
    /// Load music service authentication information for a given service ID.
    /// </summary>
    /// <param name="serviceId">Music service ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Will return <see langword="null"/> when this service was not enabled (saved by some other means).</returns>
    public async Task<MusicServiceAuthInfo?> GetMusicServiceAuth(ushort serviceId, CancellationToken cancellationToken)
    {
        var enabledServices = await GetMusicServicesEnabled(cancellationToken);
        if (enabledServices == null || !enabledServices.Contains(serviceId))
        {
            return null; // Service not enabled
        }

        var authKey = await GetStringSafe(string.Format(MUSIC_SERVICE_FORMAT, serviceId, MUSIC_SERVICE_AUTH), cancellationToken);
        var token = await GetStringSafe(string.Format(MUSIC_SERVICE_FORMAT, serviceId, MUSIC_SERVICE_TOKEN), cancellationToken);
        return new MusicServiceAuthInfo(authKey, token);
    }

    /// <summary>
    /// Retrieves the set of enabled music service identifiers.
    /// </summary>
    /// <remarks>The identifiers are parsed from a comma-separated string. If the string is null or empty, the
    /// method returns <see langword="null"/>.</remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. If the operation is canceled, the task will be canceled.</param>
    /// <returns>A <see cref="HashSet{T}"/> containing the identifiers of enabled music services, or <see langword="null"/> if an
    /// error occurs.</returns>
    public async Task<HashSet<ushort>?> GetMusicServicesEnabled(CancellationToken cancellationToken)
    {
        var result = await GetStringSafe(ENABLED_MUSIC_SERVICES_KEY, cancellationToken);
        try
        {
            return result?.Split(',').Select(id => ushort.Parse(id)).ToHashSet();
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Error retrieving enabled music services");
            return null;
        }
    }

    /// <summary>
    /// Removes a music service identified by the specified service ID.
    /// </summary>
    /// <remarks>If the specified music service is not currently enabled, the method returns <see
    /// langword="true"/> without performing any removal operations. The method ensures that all associated data for the
    /// music service is removed before updating the enabled services list.</remarks>
    /// <param name="serviceId">The unique identifier of the music service to remove.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the music
    /// service was successfully removed or was not enabled; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> RemoveMusicService(ushort serviceId, CancellationToken cancellationToken)
    {
        var currentServices = await GetMusicServicesEnabled(cancellationToken) ?? new HashSet<ushort>();
        if (!currentServices.Contains(serviceId))
        {
            return true; // Already removed
        }
        currentServices.Remove(serviceId);
        await RemoveSafe(string.Format(MUSIC_SERVICE_FORMAT, serviceId, MUSIC_SERVICE_AUTH), cancellationToken);
        await RemoveSafe(string.Format(MUSIC_SERVICE_FORMAT, serviceId, MUSIC_SERVICE_TOKEN), cancellationToken);
        return await SetMusicServicesEnabled(currentServices, cancellationToken);

    }

    /// <summary>
    /// Remove a system property safely, logging any errors.
    /// </summary>
    /// <param name="variableName">Variable to remove</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> RemoveSafe(string variableName, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Remove(new RemoveRequest
            {
                VariableName = variableName
            }, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Error removing system property {variableName}", variableName);
            return false;
        }
    }

    /// <summary>
    /// Save music service authentication information, and update the list of enabled music services.
    /// </summary>
    /// <param name="serviceId">ID of the music service</param>
    /// <param name="authInfo">New auth info</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see langword="true"/> when everything is saved successfull</returns>
    public async Task<bool> SaveMusicServiceAuth(ushort serviceId, MusicServiceAuthInfo authInfo, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(authInfo.Key) || string.IsNullOrEmpty(authInfo.Token))
        {
            logger?.LogWarning("AuthInfo for service {serviceId} is incomplete. Both AuthKey and Token are required.", serviceId);
            return false;
        }

        return await SetString(string.Format(MUSIC_SERVICE_FORMAT, serviceId, MUSIC_SERVICE_AUTH), authInfo.Key, cancellationToken)
            && await SetString(string.Format(MUSIC_SERVICE_FORMAT, serviceId, MUSIC_SERVICE_TOKEN), authInfo.Token, cancellationToken)
            && await AddEnabledMusicService(serviceId, cancellationToken);
    }

    public Task<bool> SaveMusicServiceAuth(ushort serviceId, string key, string token) => SaveMusicServiceAuth(serviceId, new MusicServiceAuthInfo(key, token), CancellationToken.None);


    /// <summary>
    /// Update the list of enabled music services.
    /// </summary>
    /// <param name="serviceIds"><see cref="HashSet{T}"/> off all services to enable</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see langword="true"/> when sucessfull</returns>
    public async Task<bool> SetMusicServicesEnabled(HashSet<ushort> serviceIds, CancellationToken cancellationToken)
    {
        try
        {
            var value = string.Join(',', serviceIds);
            var result = await SetString(new SetStringRequest
            {
                VariableName = ENABLED_MUSIC_SERVICES_KEY,
                StringValue = value
            }, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Error setting enabled music services");
            return false;
        }
    }

    /// <summary>
    /// Save a string value to a system property safely, logging any errors.
    /// </summary>
    /// <param name="variableName">Name of the variable</param>
    /// <param name="value">Value to save</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see langword="true"/> when successfull</returns>
    public async Task<bool> SetString(string variableName, string value, CancellationToken cancellationToken)
    {
        try
        {
            var result = await SetString(new SetStringRequest
            {
                VariableName = variableName,
                StringValue = value
            }, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Error setting system property {variableName}", variableName);
            return false;
        }
    }

    /// <summary>
    /// Music service authentication information.
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Token"></param>
    public record MusicServiceAuthInfo(string? Key, string? Token);
}
