
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Sonos.Web.OpenApi;

internal class RemoveInstanceIdTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        if (operation.RequestBody?.Content == null)
        {
            return Task.CompletedTask;
        }

        
        foreach (var mediaType in operation.RequestBody.Content.Values)
        {
            if (mediaType.Schema?.Properties != null)
            {
                // Remove InstanceID property (case-insensitive)
                mediaType.Schema.Properties.Remove("instanceID");

                // Remove from required properties if present
                if (mediaType.Schema.Required != null)
                {
                    mediaType.Schema.Required.Remove("instanceID");
                }
            }
        }
        

        return Task.CompletedTask;

    }
}
