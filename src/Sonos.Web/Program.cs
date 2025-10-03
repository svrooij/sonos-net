using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Scalar.AspNetCore;

using Sonos.Base;
using Sonos.Base.Music;
using Sonos.Web;
using Sonos.Web.Music;
using Sonos.Web.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(api =>
{
    api.ShouldInclude = (desc) => true;
    api.AddOperationTransformer(new Sonos.Web.OpenApi.RemoveInstanceIdTransformer());
    api.AddOperationTransformer(new Sonos.Web.OpenApi.DocumentOperationTransformer());
    api.AddOperationTransformer(new Sonos.Web.OpenApi.BasePathOperationTransformer());
    api.AddDocumentTransformer(new Sonos.Web.OpenApi.BasePathDocumentTransformer());
});

builder.Services.Configure<Sonos.Base.Events.Http.SonosEventReceiverOptions>(conf =>
{
    conf.Host = builder.Configuration.GetValue<string?>("SONOS_EVENT_HOST");
    conf.Port = builder.Configuration.GetValue<int?>("SONOS_EVENT_PORT") ?? 6329;
});

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

// Make the ISonosEventBus available for injection
builder.Services.AddSingleton<ISonosEventBus, Sonos.Base.Events.Http.SonosEventReceiver>();
// Register the SonosEventReceiver as a hosted service
builder.Services.AddHostedService(sp => (Sonos.Base.Events.Http.SonosEventReceiver)sp.GetRequiredService<ISonosEventBus>());

builder.Services.AddHttpClient(SonosServiceProvider.HttpClientName, httpClient =>
{
    httpClient.Timeout = TimeSpan.FromSeconds(20);
});
builder.Services.AddSingleton<ISonosServiceProvider, SonosServiceProvider>();
builder.Services.AddSingleton<SonosManager>();
builder.Services.AddMusicClientSupport();

builder.Services.AddSignalR();

builder.Services.AddHostedService<SonosWorker>();

var app = builder.Build();

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
}
else
{
    app.UseHttpsRedirection();
}


// Enable serving static files for Blazor WebAssembly
app.UseBlazorFrameworkFiles();
app.UseStaticFiles(new StaticFileOptions
{
    RedirectToAppendTrailingSlash = true,
});

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