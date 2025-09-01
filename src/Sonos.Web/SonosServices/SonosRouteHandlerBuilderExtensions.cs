namespace Sonos.Web.SonosServices;

internal static class SonosRouteHandlerBuilderExtensions
{
    internal static RouteHandlerBuilder WithSonosServiceDescription(this RouteHandlerBuilder builder, string serviceName, string action, string urlServiceName, string? description)
    {
        description ??= $"`{action}` action for `{serviceName}` service";
        description += $"\n\nSee [action description](https://sonos.svrooij.io/services/{urlServiceName}#{action.ToLower()}) for more details";
        return builder
            .WithDisplayName($"{serviceName} {action}")
            .WithName($"{serviceName}-{action}")
            .WithSummary($"{action}")
            .WithDescription(description)
            .WithOpenApi()
            .ProducesProblem(404)
            .ProducesProblem(422);
    }
}
