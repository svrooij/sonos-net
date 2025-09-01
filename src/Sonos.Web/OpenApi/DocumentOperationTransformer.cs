
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Sonos.Web.OpenApi;

internal class DocumentOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        bool hasSpeakerIdParameter = false;
        if (operation.Parameters is not null)
        {
            var speakerIdParam = operation.Parameters.FirstOrDefault(p => p.Name == "speakerId");
            if (speakerIdParam is not null)
            {
                hasSpeakerIdParameter = true;
                speakerIdParam.Description = "UUID of the Sonos speaker";
                speakerIdParam.Required = true;
                speakerIdParam.Schema = new OpenApiSchema
                {
                    Type = "string",
                    Pattern = "^RINCON_[A-F0-9]{12}01400$",
                    Example = new OpenApiString("RINCON_38420BB9D72E01400")
                };
            }
        }

        if (operation.Responses is not null )
        {
            var notFoundResponse = operation.Responses.FirstOrDefault(r => r.Key == "404");
            if (notFoundResponse.Value is not null)
            {
                notFoundResponse.Value.Description = hasSpeakerIdParameter ? "Speaker ID incorrect" : "Not found";
            }

            var unprocessableEntityResponse = operation.Responses.FirstOrDefault(r => r.Key == "422");
            if (unprocessableEntityResponse.Value is not null)
            {
                unprocessableEntityResponse.Value.Description = "Sonos service error";
            }
        }

        return Task.CompletedTask;
    }
}
