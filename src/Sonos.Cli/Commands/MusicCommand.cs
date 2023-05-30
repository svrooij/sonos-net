using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sonos.Base.Music;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;

namespace Sonos.Cli.Commands;

public class MusicCommand
{
    //public enum MusicCommandEnum
    //{
    //    Position = 1,
    //    Transport = 2,

    //    //Volume = 3,
    //    Media = 4,
    //    MusicClient = 5,
    //}

    public static Command GetCommand()
    {
        var listCommand = new Command("list", "List all music services") { new Option<bool?>("--json", "Output list as json")};
        listCommand.Handler = CommandHandler.Create<MusicCommandOptions, IHost, InvocationContext>(RunList);

        var loginCommand = new Command("login", "Login to music service") { new Argument<int>("serviceId", "ID of the service you wish to login to")};
        loginCommand.Handler = CommandHandler.Create<MusicCommandOptions, IHost, InvocationContext>(RunLogin);

        var browseCommand = new Command("browse", "Browse a music service") {
            new Argument<int>("serviceId", "ID of the service you wish to login to"),
            new Argument<string>("input",() => "root", "What do you want to browse"),
            new Option<bool?>("--json", "Output result as json")
        };
        browseCommand.Handler = CommandHandler.Create<MusicCommandOptions, IHost, InvocationContext>(RunBrowse);

        var command = new Command("music", "Talk to music services")
        {
            listCommand,
            loginCommand,
            browseCommand,
        };
        
        //command.Handler = CommandHandler.Create<MusicCommandOptions, IHost, InvocationContext>(Run);
        return command;
    }

    private static async Task Run(MusicCommandOptions options, IHost host, InvocationContext ctx)
    {
        //var logger = host.Services.GetRequiredService<ILogger<InfoCommand>>();
        //logger.LogDebug("Execute info command {host} {info}", options.Host, options.Action);
        //var sonos = host.CreateSonosDeviceWithOptions(options);
        //var token = ctx.GetCancellationToken();
        //switch (options.Info)
        //{
        //    case SonosInfo.Position:
        //        CommandHelpers.WriteJson(await sonos.AVTransportService.GetPositionInfo(token));
        //        break;

        //    case SonosInfo.Transport:
        //        CommandHelpers.WriteJson(await sonos.AVTransportService.GetTransportInfo(token));
        //        break;
        //    //case SonosInfo.Volume:
        //    //CommandHelpers.WriteJson(await sonos.RenderingControlService.GetVolume());
        //    //break;
        //    case SonosInfo.Media:
        //        CommandHelpers.WriteJson(await sonos.AVTransportService.GetMediaInfo(token));
        //        break;
        //    case SonosInfo.MusicClient:
        //        var serial = await sonos.SystemPropertiesService.GetString("R_TrialZPSerial", token);
        //        var householdId = (await sonos.DevicePropertiesService.GetHouseholdID(token)).CurrentHouseholdID;
        //        CommandHelpers.WriteJson(new { serial, householdId });
        //        break;
        //}
    }

    private static async Task RunList(MusicCommandOptions options, IHost host, InvocationContext ctx)
    {
        var logger = host.Services.GetRequiredService<ILogger<MusicCommand>>();
        logger.LogDebug("Music service list {host}", options.Host);
        var sonos = host.CreateSonosDeviceWithOptions(options);
        var token = ctx.GetCancellationToken();

        var services = await sonos.MusicServicesService.ListAvailableServices(token);

        if (options.Json == true)
        {
            CommandHelpers.WriteJson(services.MusicServices);
        } else
        {
            Console.WriteLine("ID\tName\t\t\t\tAuth");
            foreach (var service in services.MusicServices.OrderBy(m => m.Name))
            {
                Console.WriteLine("{0}\t{1}\t\t\t{2}", service.Id, service.Name, service.Policy.Auth);
            }
        }
        
    }

    private static async Task RunLogin(MusicCommandOptions options, IHost host, InvocationContext ctx)
    {
        var logger = host.Services.GetRequiredService<ILogger<MusicCommand>>();
        logger.LogDebug("Music service login {host} {serviceId}", options.Host, options.ServiceId);
        var sonos = host.CreateSonosDeviceWithOptions(options);
        var token = ctx.GetCancellationToken();

        var musicClient = await sonos.GetMusicClientAsync(options.ServiceId ?? -1, cancellationToken: token);

        string? linkCode = null;

        if (musicClient.AuthenticationType == AuthenticationType.AppLink)
        {
            var appLinkResult = await musicClient.GetAppLinkAsync(token);
            linkCode = appLinkResult.AuthorizeAccount.DeviceLink.LinkCode;
            Console.WriteLine("Go to {0}", appLinkResult.AuthorizeAccount.DeviceLink.RegistrationUrl);
            if(appLinkResult.AuthorizeAccount.DeviceLink.ShowLinkCode == true)
            {
                Console.WriteLine("And enter code: {0}", appLinkResult.AuthorizeAccount.DeviceLink.LinkCode);
            }
        }

        // TODO Implement others

        if (linkCode != null)
        {
            
            var result = false;
            int count = 0;
            while(!result && count < 11)
            {
                await Task.Delay(30000, token);
                result = await musicClient.FinishLoginAsync(linkCode, token);
                count++;
            }
        }
    }

    private static async Task RunBrowse(MusicCommandOptions options, IHost host, InvocationContext ctx)
    {
        var logger = host.Services.GetRequiredService<ILogger<MusicCommand>>();
        logger.LogDebug("Music service login {host} {serviceId}", options.Host, options.ServiceId);
        var sonos = host.CreateSonosDeviceWithOptions(options);
        var token = ctx.GetCancellationToken();

        var musicClient = await sonos.GetMusicClientAsync(options.ServiceId ?? -1, cancellationToken: token);

        var result = await musicClient.GetMetadataAsync(new Base.Music.Models.GetMetadataRequest { Id = options.Input }, token);

        if (result != null)
        {
            if (options.Json == true)
            {
                CommandHelpers.WriteJson(result.MediaCollection);
                CommandHelpers.WriteJson(result.MediaMetadata);
            } else
            {
                
                if (result.MediaCollection != null)
                {
                    Console.WriteLine("Id\tItemType\tName");
                    foreach (var media in result.MediaCollection)
                    {
                        Console.WriteLine("{0}\t\t{1}\t\t{2}", media.Id, media.ItemType, media.Title);
                    }
                } else
                {
                    Console.WriteLine("Id\tItemType\tName");
                    foreach (var media in result.MediaMetadata)
                    {
                        Console.WriteLine("{0}\t\t{1}\t\t{2}", media.Id, media.ItemType, media.Title);
                    }
                }
            }
        }
    }


    public class MusicCommandOptions : BaseOptions
    {
        //public MusicCommandEnum Action { get; set; }

        public int? ServiceId { get; set; }
        public bool? Json { get; set; }
        public string? Input { get; set; }
    }
}