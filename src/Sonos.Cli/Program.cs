// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sonos.Base;
using Sonos.Base.Events.Http;
using Sonos.Cli.Commands;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;

namespace Sonos.Cli;

public class Program
{
    private static async Task Main(string[] args) => await BuildCommandLine()
        .UseHost(_ => Host.CreateDefaultBuilder(),
            host =>
            {
                host.ConfigureServices((context, services) =>
                {
                    services.AddHttpClient();
                    services.AddTransient<ISonosServiceProvider, SonosServiceProvider>();
                    if (args.Contains("events"))
                    {
                        services.Configure<SonosEventReceiverOptions>(context.Configuration.GetSection("SONOSEVENTS"));
                        services.AddSingleton<ISonosEventBus, SonosEventReceiver>();
                        
                        services.AddHostedService(factory => (SonosEventReceiver)factory.GetRequiredService<ISonosEventBus>());
                    }
                });

            })
        .UseDefaults()
        .Build()
        .InvokeAsync(args);

    private static CommandLineBuilder BuildCommandLine()
    {
        var root = new RootCommand("Sonos command line controller"){
              new Argument<string>("host"),
                ControlCommand.GetCommand(),
                InfoCommand.GetCommand(),
                VolumeCommand.GetCommand(),
                ZonesCommand.GetCommand(),
                AlarmsCommand.GetCommand(),
                EventsCommand.GetCommand(),
            };
        //root.Handler = CommandHandler.Create<IHost>(Run);
        return new CommandLineBuilder(root);
    }
}