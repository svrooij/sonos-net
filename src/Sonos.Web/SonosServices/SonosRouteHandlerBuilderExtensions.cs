namespace Sonos.Web.SonosServices;

internal static class SonosRouteHandlerBuilderExtensions
{
    internal static RouteHandlerBuilder WithSonosServiceDescription(this RouteHandlerBuilder builder, string serviceName, string action, string urlServiceName, string? description)
    {
        description ??= $"`{action}` action for `{serviceName}` service";
        description += $"\n\nSee [{action}](https://sonos.svrooij.io/services/{urlServiceName}#{action.ToLower()}) in {serviceName} for more details";
        return builder
            .WithDisplayName($"{serviceName} {action}")
            .WithName($"{serviceName}-{action}")
            .AddOpenApiOperationTransformer((operation, _, _) =>
            {
                operation.Summary = $"{action}";
                operation.Description = description;
                operation.Responses ??= new(); // Not sure if this is needed, since the `Responses` are required.
                operation.Responses["200"].Description = $"Success response for {serviceName}/{action}";
                return Task.CompletedTask;

            })
            .ProducesProblem(404)
            .ProducesProblem(409);
    }
}
