// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine.Builder;
using System.CommandLine;
using System.CommandLine.Hosting;
using System.Threading.Tasks;
using System.CommandLine.Parsing;
using Sonos.Cli.Commands;

namespace Sonos.Cli;

public class Program
{
    static async Task Main(string[] args) => await BuildCommandLine()
        .UseHost(_ => Host.CreateDefaultBuilder(),
            host =>
            {
                host.ConfigureServices(services =>
                {
                    services.AddHttpClient();
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
                AlarmsCommand.GetCommand(),
            };
        //root.Handler = CommandHandler.Create<IHost>(Run);
        return new CommandLineBuilder(root);
    }
}
