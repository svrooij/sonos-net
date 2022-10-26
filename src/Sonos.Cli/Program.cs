// See https://aka.ms/new-console-template for more information
using Sonos.Base;

Console.WriteLine("Hello, World!");

var sonos = new SonosDevice(new Uri("http://192.168.x.x:1400/"), null, null);
// await sonos.AVTransportService.Play(new Sonos.Base.Services.AVTransportService.PlayRequest(){Speed = "1"});

// var bass = await sonos.RenderingControlService.GetBass();
// var info = await sonos.DevicePropertiesService.GetZoneInfo();
var led = await sonos.DevicePropertiesService.GetLEDState();
Console.WriteLine();