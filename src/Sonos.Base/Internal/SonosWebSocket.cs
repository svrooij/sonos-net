using Sonos.Base.Services;
using System.Net.WebSockets;
using System.Text.Json;

namespace Sonos.Base.Internal
{
    internal class SonosWebSocket : IAsyncDisposable, IDisposable
    {
        private const string SubProtocol = "v1.api.smartspeaker.audio";
        // Have no idea if this should be public
        // https://github.com/bencevans/node-sonos/issues/530#issuecomment-1430039043
        private const string ApiKey = "123e4567-e89b-12d3-a456-426655440000";
        private readonly SonosServiceOptions options;
        private readonly ClientWebSocket webSocket;
        internal SonosWebSocket(SonosServiceOptions serviceOptions)
        {
            this.webSocket = new ClientWebSocket();
            this.options = serviceOptions;
#if NET6_0_OR_GREATER
            webSocket.Options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
#else
            throw new NotImplementedException("Sonos has a Self-Signed certificate for the websocket, and RemoteCertificateValidationCallback is unavailable on your platform");
#endif
            webSocket.Options.AddSubProtocol(SubProtocol);
            webSocket.Options.SetRequestHeader("X-Sonos-Api-Key", ApiKey);
        }

        internal Task ConnectAsync(CancellationToken cancellationToken)
        {
            if (webSocket.State != WebSocketState.Open)
            {
                return webSocket.ConnectAsync(options.GetWebsocketUri(), cancellationToken);
            }
            return Task.CompletedTask;
            
        }

        internal async Task QueueNoticiationAsync(string uuid, string uri, int volume, CancellationToken cancellationToken)
        {
            await ConnectAsync(cancellationToken);
            var message = new dynamic[] {
                new { @namespace = "audioClip:1", command = "loadAudioClip", playerId = uuid },
                new { name = "Sonos.NET Notification", appId = "io.svrooij.sonos-net", streamUrl = uri, volume = volume },
            };
            var data = JsonSerializer.SerializeToUtf8Bytes(message);
            var buffer = new ArraySegment<byte>(data);
            await webSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, cancellationToken);
        }

        public void Dispose()
        {
            webSocket.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (webSocket is not null && webSocket.State == WebSocketState.Open)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                webSocket.Dispose();
            }
            
        }
    }
}
