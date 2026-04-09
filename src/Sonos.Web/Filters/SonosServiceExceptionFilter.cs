using Sonos.Base.Services;

namespace Sonos.Web.Filters;

/// <summary>
/// Provides an endpoint filter that handles exceptions of type SonosServiceException and returns a standardized service
/// exception result.
/// </summary>
/// <remarks>Use this filter to ensure consistent error handling across endpoints by converting
/// SonosServiceException instances into a uniform error response. This helps maintain predictable error formats for
/// clients consuming the API.</remarks>
public class SonosServiceExceptionFilter : IEndpointFilter
{
    /// <inheritdoc />
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);

        }
        catch (SonosServiceException ex)
        {
            return Sonos.Web.SonosServices.SonosResults.ServiceExceptionResult(ex);
        }
    }
}

internal static class SonosServiceExceptionFilterExtensions
{
    /// <summary>
    /// Adds the <see cref="SonosServiceExceptionFilter"/> to the endpoint filter pipeline.
    /// </summary>
    /// <param name="builder">The <see cref="RouteHandlerBuilder"/> to which the filter should be added.</param>
    /// <returns>The modified <see cref="RouteHandlerBuilder"/> with the filter added.</returns>
    public static RouteHandlerBuilder AddSonosServiceExceptionFilter(this RouteHandlerBuilder builder) =>
        builder
            .AddEndpointFilter<SonosServiceExceptionFilter>()
            .ProducesProblem(409);
    
}
