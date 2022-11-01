// See https://aka.ms/new-console-template for more information
using Sonos.Base;

Console.WriteLine("Hello, World!");

var sonos = new SonosDevice(new Uri("http://192.168.200.52:1400/"), null, null);
// await sonos.AVTransportService.Play(new Sonos.Base.Services.AVTransportService.PlayRequest(){Speed = "1"});

// var bass = await sonos.RenderingControlService.GetBass();
// var info = await sonos.DevicePropertiesService.GetZoneInfo();
// var zoneGroupState = await sonos.ZoneGroupTopologyService.GetZoneGroupState();
// foreach(var group in zoneGroupState.ParsedState.ZoneGroups) {
//   Console.WriteLine("Group: {0}", group.ZoneGroupMember.First().ZoneName);
// }

// var alarmState = await sonos.AlarmClockService.ListAlarms();
// foreach(var alarm in alarmState.Alarms) {
//   Console.WriteLine("Alarm ID: {0} time: {1}", alarm.ID, alarm.StartTime);
// }

var musicServices = await sonos.MusicServicesService.ListAvailableServices();
foreach (var service in musicServices.MusicServices)
{
    Console.WriteLine("Music service ID: {0} Name: {1}", service.Id, service.Name);
}
Console.WriteLine(musicServices.AvailableServiceTypeList);
// Console.WriteLine(musicServices.AvailableServiceDescriptorList);