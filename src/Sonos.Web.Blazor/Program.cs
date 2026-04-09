using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Sonos.Web.Blazor;
using Sonos.Web.Blazor.Client;
using Sonos.Web.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient to use the hosting server's base address
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddKiotaHandlers();

builder.Services.AddHttpClient<SonosWebClientFactory>((sp, client) =>
{
   client.BaseAddress = new Uri(new Uri(builder.HostEnvironment.BaseAddress), "/api/");
});



builder.Services.AddTransient(sp => sp.GetRequiredService<SonosWebClientFactory>().CreateClient());
builder.Services.AddSingleton<PlayerService>();


var host = builder.Build();
var playerService = host.Services.GetRequiredService<PlayerService>();
await playerService.Initialize();
await playerService.InitializeSignalR(new Uri(new Uri(builder.HostEnvironment.BaseAddress), "/api/ws/player"));
await host.RunAsync();
