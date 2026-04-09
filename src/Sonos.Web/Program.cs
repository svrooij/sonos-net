using System.Reflection;
using System.Text.Json.Serialization;


using Microsoft.AspNetCore.StaticFiles;

using Scalar.AspNetCore;
using Sonos.Base;
using Sonos.Web;
using Sonos.Web.Music;
using Sonos.Web.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(api =>
{
    api.ShouldInclude = (desc) => true;
    api.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_1;
    api.AddSchemaTransformer<Sonos.Web.OpenApi.SonosSchemaTransformer>();
    api.AddOperationTransformer<Sonos.Web.OpenApi.RemoveInstanceIdTransformer>();
    api.AddOperationTransformer<Sonos.Web.OpenApi.DocumentOperationTransformer>();
    api.AddOperationTransformer<Sonos.Web.OpenApi.BasePathOperationTransformer>();
    api.AddDocumentTransformer<Sonos.Web.OpenApi.BasePathDocumentTransformer>();
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.NumberHandling = JsonNumberHandling.Strict;
});


builder.Services.Configure<Sonos.Base.Events.Http.SonosEventReceiverOptions>(conf =>
{
    conf.Host = builder.Configuration.GetValue<string?>("SONOS_EVENT_HOST");
    conf.Port = builder.Configuration.GetValue<int?>("SONOS_EVENT_PORT") ?? 6329;
});

builder.Services.AddMemoryCache();
//builder.Services.AddHttpClient();

// If the crappy OpenAPI generator is not running
if (Assembly.GetEntryAssembly()?.GetName().Name != "GetDocument.Insider")
{
    // Make the ISonosEventBus available for injection
    builder.Services.AddSingleton<ISonosEventBus, Sonos.Base.Events.Http.SonosEventReceiver>();
    // Register the SonosEventReceiver as a hosted service
    builder.Services.AddHostedService(sp => (Sonos.Base.Events.Http.SonosEventReceiver)sp.GetRequiredService<ISonosEventBus>());

    builder.Services.AddHostedService<SonosWorker>();
}

builder.Services.AddHttpClient(SonosServiceProvider.HttpClientName, httpClient =>
{
    httpClient.Timeout = TimeSpan.FromSeconds(20);
});
builder.Services.AddSingleton<ISonosServiceProvider, SonosServiceProvider>();
builder.Services.AddSingleton<SonosManager>();
builder.Services.AddMusicClientSupport();

builder.Services.AddSignalR();


var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
app.MapOpenApi();
if (builder.Configuration.GetValue<bool>("DOTNET_RUNNING_IN_CONTAINER") == true)
{
    // Assume HTTPS termination is handled by the container orchestrator (e.g., Kubernetes, Docker Swarm)
    app.UseForwardedHeaders();

    // Fix the content type for .dat files, this is to serve the Blazor WebAssembly files from the wwwroot folder.
    var provider = new FileExtensionContentTypeProvider();
    provider.Mappings[".dat"] = "application/octet-stream";
    app.UseStaticFiles(new StaticFileOptions
    {
        RedirectToAppendTrailingSlash = true,
        ContentTypeProvider = provider,
        ServeUnknownFileTypes = false
    });
}
else
{
    //app.UseHttpsRedirection();
}



// Configure routing
app.UseRouting();

app.MapScalarApiReference(options =>
{
    //options.AddOAuth2Authentication("jwt", auth =>
    //{
    //    // Bind the ScalarOAuth section in the configuration to OAuth2AuthenticationOptions
    //    builder.Configuration.Bind("ScalarOAuth", auth);
    //    auth.Description = "Entra ID authentication using JWT Bearer tokens";
    //    auth.WithFlows(flows =>
    //    {
    //        flows.AuthorizationCode = new AuthorizationCodeFlow();
    //        builder.Configuration.Bind("ScalarOAuthCodeFlow", flows.AuthorizationCode);
    //        flows.AuthorizationCode.Pkce = Pkce.Sha256; // Set PKCE
    //    });

    //});
    //options.AddPreferredSecuritySchemes("jwt");
    //options.PersistentAuthentication = true;
    options.TagSorter = TagSorter.Alpha;
    options.Title = "Sonos-net API 📄 with Scalar";
    options.HideClientButton = true;
    options.HideModels = true;
    options.EnabledTargets = [ScalarTarget.PowerShell, ScalarTarget.CSharp, ScalarTarget.Http, ScalarTarget.Shell];
    // options.EnabledClients = [ScalarClient.HttpClient];
});

app.MapSonosApi();
app.MapMusicServices();

// Map the SignalR hub for player status updates
// be sure to subscribe for updates from the right player
app.MapHub<Sonos.Web.Hubs.PlayerStatusHub>("/api/ws/player");

// Fallback routing for Blazor WebAssembly
app.MapFallbackToFile("index.html");

app.Run();