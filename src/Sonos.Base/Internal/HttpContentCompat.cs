#if NETSTANDARD2_0
namespace System.Net.Http;
internal static class HttpContentExtensions {
    // This way you don't have to change the code running in NETSTANDARD2.0
    internal static Task<Stream> ReadAsStreamAsync(this HttpContent input, CancellationToken cancellationToken) {
        return input.ReadAsStreamAsync();
    }
}
#endif