using Scalar.AspNetCore;

using Sonos.Base;
using Sonos.Web;
using Sonos.Web.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(api =>
{
    api.ShouldInclude = (desc) => true;
    api.AddOperationTransformer<Sonos.Web.OpenApi.RemoveInstanceIdTransformer>();
    api.AddOperationTransformer<Sonos.Web.OpenApi.DocumentOperationTransformer>();
});

builder.Services.Configure<Sonos.Base.Events.Http.SonosEventReceiverOptions>(conf =>
{
    conf.Host = builder.Configuration.GetValue<string?>("SONOS_EVENT_HOST");
});

// Make the ISonosEventBus available for injection
builder.Services.AddSingleton<ISonosEventBus, Sonos.Base.Events.Http.SonosEventReceiver>();
// Register the SonosEventReceiver as a hosted service
builder.Services.AddHostedService(sp => (Sonos.Base.Events.Http.SonosEventReceiver)sp.GetRequiredService<ISonosEventBus>());

builder.Services.AddSingleton<ISonosServiceProvider, SonosServiceProvider>();
builder.Services.AddSingleton<SonosManager>();

builder.Services.AddHostedService<SonosWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
app.MapOpenApi();

app.UseHttpsRedirection();

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

app.Run();