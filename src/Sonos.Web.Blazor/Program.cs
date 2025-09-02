using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Sonos.Web.Blazor;
using Sonos.Web.Blazor.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient to use the hosting server's base address
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddKiotaHandlers();

builder.Services.AddHttpClient<SonosWebClientFactory>((sp, client) =>
{
   client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddTransient(sp => sp.GetRequiredService<SonosWebClientFactory>().CreateClient());


await builder.Build().RunAsync();
