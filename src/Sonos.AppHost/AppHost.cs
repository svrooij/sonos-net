using Microsoft.Extensions.Configuration;

using Scalar.Aspire;

var builderOptions = new DistributedApplicationOptions
{
    AllowUnsecuredTransport = true,
    Args = args,
    TrustDeveloperCertificate = true,
    DeveloperCertificateDefaultHttpsTerminationEnabled = true,


};
var builder = DistributedApplication.CreateBuilder(builderOptions);

var api = builder
    .AddProject<Projects.Sonos_Web>("sonos-web", "http")
    .AsHttp2Service()
    .WithHttpHealthCheck("/health")
    
    .WithUrl("/scalar", "docs");

var frontend = builder.AddProject<Projects.Sonos_Web_Blazor>("sonos-web-blazor", "http")
    .WithExplicitStart();

// READ otlp endpoint from configuration and replace localhost with host.docker.internal for container scenarios
// ASPIRE_DASHBOARD_OTLP_HTTP_ENDPOINT_URL

//var otelEndpoint = builder.AddExternalService("otlp", builder.Configuration.GetValue<string>("ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL")!.Replace("localhost", "host.docker.internal"));

var otlpKey = builder.Configuration.GetValue<string>("Dashboard__Otlp__SecondaryApiKey") ?? "a20eb898-7826-433b-8df8-e8034e35a9cb";
var gateway = builder.AddYarp("gateway")
                     .WithConfiguration(yarp =>
                     {
                         // Configure routes programmatically
                         yarp.AddRoute("/api/{**catch-all}", api);
                         //yarp.AddRoute("/otlp/{**catch-all}", otelEndpoint)
                         //   .WithTransformPathRemovePrefix("/otlp");
                            //.WithTransformRequestHeader("x-otlp-api-key", otlpKey, append: true);
                         yarp.AddRoute("/getaa", api);
                         yarp.AddRoute("/scalar/{**catch-all}", api);
                         yarp.AddRoute("/openapi/{**catch-all}", api);
                         yarp.AddRoute(frontend);

                     })
                     .WaitFor(api);


var scalar = builder.AddScalarApiReference()
    .WithExplicitStart();

scalar.WithApiReference(api);

builder.Build().Run();
