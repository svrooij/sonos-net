using Microsoft.Extensions.Hosting;
using Sonos.Base.Events;
using Sonos.Base.Services;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Sonos.Cli.Commands.ControlCommand;

namespace Sonos.Cli.Commands;

public class EventsCommand
{
    public static Command GetCommand()
    {
        var command = new Command("events", "Listen to sonos events")
        {
            new Argument<SonosService>("service")
        };

        command.Handler = CommandHandler.Create<EventCommandOptions, IHost, InvocationContext>(Run);
        return command;
    }

    private static async Task Run(EventCommandOptions options, IHost host, InvocationContext ctx)
    {
        using var sonos = host.CreateSonosDeviceWithOptions(options);

        try
        {
            var token = ctx.GetCancellationToken();

            switch (options.Service)
            {
                case SonosService.AlarmClock:
                    sonos.AlarmClockService.OnEvent += SomeService_OnEvent;
                    await sonos.AlarmClockService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.AudioIn:
                    sonos.AudioInService.OnEvent += SomeService_OnEvent;
                    await sonos.AudioInService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.AVTransport:
                    sonos.AVTransportService.OnEvent += SomeService_OnEvent;
                    await sonos.AVTransportService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.ConnectionManager:
                    sonos.ConnectionManagerService.OnEvent += SomeService_OnEvent;
                    await sonos.ConnectionManagerService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.ContentDirectory:
                    sonos.ContentDirectoryService.OnEvent += SomeService_OnEvent;
                    await sonos.ContentDirectoryService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.DeviceProperties:
                    sonos.DevicePropertiesService.OnEvent += SomeService_OnEvent;
                    await sonos.DevicePropertiesService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.GroupManagement:
                    sonos.GroupManagementService.OnEvent += SomeService_OnEvent;
                    await sonos.GroupManagementService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.GroupRenderingControl:
                    sonos.GroupRenderingControlService.OnEvent += SomeService_OnEvent;
                    await sonos.GroupRenderingControlService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.HTControl:
                    sonos.HTControlService.OnEvent += SomeService_OnEvent;
                    await sonos.HTControlService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.MusicServices:
                    sonos.MusicServicesService.OnEvent += SomeService_OnEvent;
                    await sonos.MusicServicesService.SubscribeForEventsAsync(token);
                    break;
                //case SonosService.QPlay:
                //    break;
                case SonosService.Queue:
                    sonos.QueueService.OnEvent += SomeService_OnEvent;
                    await sonos.QueueService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.RenderingControl:
                    sonos.RenderingControlService.OnEvent += SomeService_OnEvent;
                    await sonos.RenderingControlService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.SystemProperties:
                    sonos.SystemPropertiesService.OnEvent += SomeService_OnEvent;
                    await sonos.SystemPropertiesService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.VirtualLineIn:
                    sonos.VirtualLineInService.OnEvent += SomeService_OnEvent;
                    await sonos.VirtualLineInService.SubscribeForEventsAsync(token);
                    break;
                case SonosService.ZoneGroupTopology:
                    sonos.ZoneGroupTopologyService.OnEvent += SomeService_OnEvent;
                    await sonos.ZoneGroupTopologyService.SubscribeForEventsAsync(token);
                    break;
            }

            await Task.Delay(-1, token);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public class EventCommandOptions : BaseOptions
    {
        public SonosService Service { get; set; }
    }

    private static void SomeService_OnEvent(object? sender, dynamic e)
    {
        CommandHelpers.WriteJson(e, true);
    }

}
