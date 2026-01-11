
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Sonos.Web.OpenApi;

internal class SonosSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (schema.Title == "speakerId")
        {
            schema.Description = "UUID of the Sonos speaker";
            schema.Pattern = "^RINCON_[A-F0-9]{12}01400$";
            schema.Example = "RINCON_38420BB9D72E01400";

        }
        return Task.CompletedTask;
    }
}
