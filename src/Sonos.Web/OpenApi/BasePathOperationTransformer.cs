
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Sonos.Web.OpenApi;

/// <summary>
/// This transformer removes the "api/" prefix from the relative paths in the OpenAPI operations.
/// </summary>
internal class BasePathOperationTransformer : IOpenApiOperationTransformer
{
    private const string RelativePathPrefixPath = "api/";
    public async Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        if(context.Description.RelativePath?.StartsWith(RelativePathPrefixPath) == true)
        {
            context.Description.RelativePath = context.Description.RelativePath[4..];
        }
    }
}

/// <summary>
/// This transformer adds a base path of "/api/" to the OpenAPI document's server URLs.
/// Making for better client generation.
/// </summary>
internal class BasePathDocumentTransformer : IOpenApiDocumentTransformer
{
    private const string BasePath = "/api";
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        if(document.Servers is not null && document.Servers.Count > 0)
        {
            foreach(var server in document.Servers)
            {
                // Add /api/ base path if not already present
                if (!server.Url.EndsWith(BasePath))
                {
                    server.Url = server.Url.TrimEnd('/') + BasePath;
                }
            }
        } else
        {
            document.Servers = new List<OpenApiServer>
            {
                new OpenApiServer { Url = "/api", Description = "Current server" }
            };
        }

        // Trim /api from the start of each path if it is present
        var updatedPaths = new OpenApiPaths();
        foreach (var path in document.Paths)
        {
            var updatedPathKey = path.Key.StartsWith(BasePath) ? path.Key[BasePath.Length..] : path.Key;
            updatedPaths[updatedPathKey] = path.Value;

        }
        document.Paths = updatedPaths;
        return Task.CompletedTask;
    }
}
