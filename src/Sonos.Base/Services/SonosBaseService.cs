/*
 * Sonos-net
 *
 * Repository https://github.com/svrooij/sonos-net
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Sonos.Base.Services;

using Microsoft.Extensions.Logging;
using Sonos.Base.Soap;
using System.Net.Http;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;

public partial class SonosBaseService : IDisposable
{
    protected readonly string ControlPath;
    protected readonly string EventPath;
    protected readonly SonosService ServiceName;
    protected readonly Uri BaseUri;
    protected readonly HttpClient httpClient;
    protected readonly ILogger logger;
    protected readonly ISonosEventBus? eventBus;
    protected readonly string uuid;

    internal SonosBaseService(SonosService serviceName, string controlPath, string eventPath, SonosServiceOptions options)
    {
        this.ControlPath = controlPath;
        this.EventPath = eventPath;
        this.ServiceName = serviceName;
        this.BaseUri = options.DeviceUri;
        this.httpClient = options.ServiceProvider.GetHttpClient();
        this.logger = options.ServiceProvider.CreateLogger($"Sonos.Base.Services.{serviceName}") ?? (ILogger)NullLogger.Instance;
        this.uuid = options.Uuid;
        this.eventBus = options.ServiceProvider.GetSonosEventBus();
    }

    internal async Task<bool> ExecuteRequest<TPayload>(TPayload payload, CancellationToken cancellationToken, [CallerMemberName] string? caller = null) where TPayload : class
    {
        LogExecuteRequestStarted(uuid, ServiceName, caller);
        var request = SoapFactory.CreateRequest(BaseUri, ControlPath, payload, caller);
        var response = await httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponse(response, cancellationToken, caller);
        }
        return true;
    }

    internal async Task<TOut> ExecuteRequest<TPayload, TOut>(TPayload payload, CancellationToken cancellationToken, [CallerMemberName] string? caller = null) where TPayload : class where TOut : class
    {
        LogExecuteRequestStarted(uuid, ServiceName, caller);
        var request = SoapFactory.CreateRequest(BaseUri, ControlPath, payload, caller);
        var response = await httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponse(response, cancellationToken, caller);
        }
        return await ParseResponse<TOut>(response, cancellationToken, caller);
    }

    internal async Task HandleErrorResponse(HttpResponseMessage response, CancellationToken cancellationToken, string? caller = null)
    {
        LogHandleErrorResponseStarted(this.uuid, ServiceName, caller);

        // TODO HandleErrorResponse needs implementation
        // var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
        using var errorContent = await response.Content.ReadAsStreamAsync(cancellationToken);
        var error = SoapFactory.ParseFaultXml(errorContent);
        if (error is not null)
        {
            var code = error.UpnpErrorCode;
            string? message = (code is not null && ServiceErrors.ContainsKey((int)code)) ? ServiceErrors[(int)code].Message : null;
            var ex = new SonosServiceException(error.FaultCode, error.FaultString, code, message);
            LogHandleErrorResponse(ex, uuid, ServiceName, caller);
            throw ex;
        }
        throw new Exception();
    }

    internal async Task<TOut> ParseResponse<TOut>(HttpResponseMessage response, CancellationToken cancellationToken, string? caller = null) where TOut : class
    {
        LogParseResponseStarted(uuid, ServiceName, caller);
        using var xml = await response.Content.ReadAsStreamAsync(cancellationToken);
        return SoapFactory.ParseXml<TOut>(ServiceName.ToString(), xml);
    }

    public virtual void Dispose()
    {

    }

    internal virtual Dictionary<int, SonosUpnpError> ServiceErrors { get => SonosUpnpError.DefaultErrors; }

    [LoggerMessage(EventId = 100, Level = LogLevel.Debug, Message = "ExecuteRequest started {Uuid}/{Service} {Action}")]
    private partial void LogExecuteRequestStarted(string uuid, SonosService service, string? action);

    [LoggerMessage(EventId = 102, Level = LogLevel.Debug, Message = "ParseResponse started {Uuid}/{Service} {Action}")]
    private partial void LogParseResponseStarted(string uuid, SonosService service, string? action);

    [LoggerMessage(EventId = 103, Level = LogLevel.Debug, Message = "HandleErrorResponse started {Uuid}/{Service} {Action}")]
    private partial void LogHandleErrorResponseStarted(string uuid, SonosService service, string? action);

    [LoggerMessage(EventId = 104, Level = LogLevel.Warning, Message = "Sonos request failed {Uuid}/{Service} {Action}")]
    private partial void LogHandleErrorResponse(SonosServiceException e, string uuid, SonosService service, string? action);
}

public class SonosBaseService<TEvent> : SonosBaseService where TEvent : IServiceEvent
{
    internal SonosBaseService(SonosService serviceName, string controlPath, string eventPath, SonosServiceOptions options) : base(serviceName, controlPath, eventPath, options)
    {
    }

    public Task<bool> SubscribeForEventsAsync(CancellationToken cancellationToken = default)
    {
        return eventBus?.Subscribe(uuid, ServiceName, new Uri(BaseUri, EventPath), EmitEvent, cancellationToken) ?? Task.FromResult(false);
    }

    public Task<bool> RenewEventSubscriptionAsync(CancellationToken cancellationToken = default)
    {
        return eventBus?.RenewSubscription(uuid, ServiceName, cancellationToken) ?? Task.FromResult(false);
    }

    public Task<bool> CancelEventSubscriptionAsync(CancellationToken cancellationToken = default)
    {
        return eventBus?.Unsubscribe(uuid, ServiceName, cancellationToken) ?? Task.FromResult(false);
    }

    public override void Dispose()
    {
        if(eventBus is not null)
        {
            CancelEventSubscriptionAsync(CancellationToken.None).GetAwaiter().GetResult();
        }
            
        base.Dispose();

    }

    public event EventHandler<TEvent> OnEvent;

    private void EmitEvent(IServiceEvent e)
    {
        OnEvent?.Invoke(this, (TEvent)e);
    }
}