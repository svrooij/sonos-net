/*
 * Sonos-net
 *
 * Repository https://github.com/svrooij/sonos-net
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Microsoft.Extensions.DependencyInjection;
using Moq;
using Sonos.Base.Tests;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sonos.Base.Services.Tests;

public class AlarmClockServiceTests
{
    private const string ListAlarmsResponseXml = @"<CurrentAlarmList>&lt;Alarms&gt;&lt;Alarm ID=&quot;1&quot; StartTime=&quot;07:30:00&quot; Duration=&quot;00:15:00&quot; Recurrence=&quot;WEEKDAYS&quot; Enabled=&quot;1&quot; RoomUUID=&quot;RINCON_AAAAAAAAAAAA01400&quot; ProgramURI=&quot;x-rincon-cpcontainer:1006206cspotify%3aplaylist%3a37i9dQZF1DX4WYpdgoIcn6?sid=9&amp;amp;flags=8300&amp;amp;sn=2&quot; ProgramMetaData=&quot;&amp;lt;DIDL-Lite xmlns:dc=&amp;quot;http://purl.org/dc/elements/1.1/&amp;quot; xmlns:upnp=&amp;quot;urn:schemas-upnp-org:metadata-1-0/upnp/&amp;quot; xmlns:r=&amp;quot;urn:schemas-rinconnetworks-com:metadata-1-0/&amp;quot; xmlns=&amp;quot;urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/&amp;quot;&amp;gt;&amp;lt;item id=&amp;quot;1006206cspotify%3aplaylist%3a37i9dQZF1DX4WYpdgoIcn6&amp;quot; parentID=&amp;quot;10fe2664playlists&amp;quot; restricted=&amp;quot;true&amp;quot;&amp;gt;&amp;lt;dc:title&amp;gt;Chill Hits&amp;lt;/dc:title&amp;gt;&amp;lt;upnp:class&amp;gt;object.container.playlistContainer&amp;lt;/upnp:class&amp;gt;&amp;lt;desc id=&amp;quot;cdudn&amp;quot; nameSpace=&amp;quot;urn:schemas-rinconnetworks-com:metadata-1-0/&amp;quot;&amp;gt;SA_RINCON2311_X_#Svc2311-0-Token&amp;lt;/desc&amp;gt;&amp;lt;/item&amp;gt;&amp;lt;/DIDL-Lite&amp;gt;&quot; PlayMode=&quot;SHUFFLE&quot; Volume=&quot;8&quot; IncludeLinkedZones=&quot;0&quot;/&gt;&lt;Alarm ID=&quot;4&quot; StartTime=&quot;07:00:00&quot; Duration=&quot;02:00:00&quot; Recurrence=&quot;ONCE&quot; Enabled=&quot;0&quot; RoomUUID=&quot;RINCON_AAAAAAAAAAAA01400&quot; ProgramURI=&quot;x-rincon-cpcontainer:1006206cspotify%3aplaylist%3a37i9dQZEVXcSKsmeEHkPRe?sid=9&amp;amp;flags=8300&amp;amp;sn=2&quot; ProgramMetaData=&quot;&amp;lt;DIDL-Lite xmlns:dc=&amp;quot;http://purl.org/dc/elements/1.1/&amp;quot; xmlns:upnp=&amp;quot;urn:schemas-upnp-org:metadata-1-0/upnp/&amp;quot; xmlns:r=&amp;quot;urn:schemas-rinconnetworks-com:metadata-1-0/&amp;quot; xmlns=&amp;quot;urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/&amp;quot;&amp;gt;&amp;lt;item id=&amp;quot;1006206cspotify%3aplaylist%3a37i9dQZEVXcSKsmeEHkPRe&amp;quot; parentID=&amp;quot;00080024spotify%3aview%3asection0JQ5DAnM3wGh0gz1MXnu80&amp;quot; restricted=&amp;quot;true&amp;quot;&amp;gt;&amp;lt;dc:title&amp;gt;Discover Weekly&amp;lt;/dc:title&amp;gt;&amp;lt;upnp:class&amp;gt;object.container.playlistContainer&amp;lt;/upnp:class&amp;gt;&amp;lt;desc id=&amp;quot;cdudn&amp;quot; nameSpace=&amp;quot;urn:schemas-rinconnetworks-com:metadata-1-0/&amp;quot;&amp;gt;SA_RINCON2311_X_#Svc2311-0-Token&amp;lt;/desc&amp;gt;&amp;lt;/item&amp;gt;&amp;lt;/DIDL-Lite&amp;gt;&quot; PlayMode=&quot;SHUFFLE&quot; Volume=&quot;8&quot; IncludeLinkedZones=&quot;0&quot;/&gt;&lt;Alarm ID=&quot;55&quot; StartTime=&quot;09:00:00&quot; Duration=&quot;02:00:00&quot; Recurrence=&quot;ONCE&quot; Enabled=&quot;0&quot; RoomUUID=&quot;RINCON_AAAAAAAAAAAA01400&quot; ProgramURI=&quot;x-sonosapi-stream:station%3aqmusic-nonstop?sid=300&amp;amp;flags=8232&amp;amp;sn=3&quot; ProgramMetaData=&quot;&amp;lt;DIDL-Lite xmlns:dc=&amp;quot;http://purl.org/dc/elements/1.1/&amp;quot; xmlns:upnp=&amp;quot;urn:schemas-upnp-org:metadata-1-0/upnp/&amp;quot; xmlns:r=&amp;quot;urn:schemas-rinconnetworks-com:metadata-1-0/&amp;quot; xmlns=&amp;quot;urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/&amp;quot;&amp;gt;&amp;lt;item id=&amp;quot;10092028station%3aqmusic-nonstop&amp;quot; parentID=&amp;quot;10fe2064stationCollection%3aqmusic-non-stop&amp;quot; restricted=&amp;quot;true&amp;quot;&amp;gt;&amp;lt;dc:title&amp;gt;Non-stop&amp;lt;/dc:title&amp;gt;&amp;lt;upnp:class&amp;gt;object.item.audioItem.audioBroadcast.#station&amp;lt;/upnp:class&amp;gt;&amp;lt;desc id=&amp;quot;cdudn&amp;quot; nameSpace=&amp;quot;urn:schemas-rinconnetworks-com:metadata-1-0/&amp;quot;&amp;gt;SA_RINCON76807_&amp;lt;/desc&amp;gt;&amp;lt;/item&amp;gt;&amp;lt;/DIDL-Lite&amp;gt;&quot; PlayMode=&quot;SHUFFLE&quot; Volume=&quot;7&quot; IncludeLinkedZones=&quot;0&quot;/&gt;&lt;/Alarms&gt;</CurrentAlarmList><CurrentAlarmListVersion>RINCON_AAAAAAAAAAAA01400:70</CurrentAlarmListVersion>";

    [Fact]
    public async Task CancelEventSubscriptionAsync_CallsEventBus()
    {
        var uuid = Guid.NewGuid().ToString();
        var mockedEventBus = new Mock<ISonosEventBus>();
        mockedEventBus
            .Setup(_ => _.Unsubscribe(It.Is<string>(v => v.Equals(uuid)), It.Is<SonosService>(v => v == SonosService.AlarmClock), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(null, mockedEventBus.Object), uuid));

        var result = await sonos.AlarmClockService.CancelEventSubscriptionAsync();

        Assert.True(result);
        mockedEventBus.VerifyAll();
    }

    [Fact]
    public async Task CancelEventSubscriptionAsync_ReturnsFalse_WithoutEventBus()
    {
        var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider()));

        var result = await sonos.AlarmClockService.CancelEventSubscriptionAsync();

        Assert.False(result);
    }

    [Fact]
    public void Dispose_CallsEventBus()
    {
        var uuid = Guid.NewGuid().ToString();
        var mockedEventBus = new Mock<ISonosEventBus>();
        mockedEventBus
            .Setup(_ => _.Unsubscribe(It.Is<string>(v => v.Equals(uuid)), It.Is<SonosService>(v => v == SonosService.AlarmClock), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(null, mockedEventBus.Object), uuid));

        sonos.AlarmClockService.Dispose();
        mockedEventBus.VerifyAll();
    }

    [Fact]
    public async Task ListAlarms_ParsesAlarmList()
    {
        var mockedHandler = new Mock<HttpClientHandler>();
        mockedHandler.MockSonosRequest(nameof(Services.SonosService.AlarmClock), nameof(Services.AlarmClockService.ListAlarms), responseBody: ListAlarmsResponseXml);
        var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(mockedHandler.Object)));


        var alarmsResponse = await sonos.AlarmClockService.ListAlarms();
        Assert.NotNull(alarmsResponse?.Alarms);
        var firstAlarm = alarmsResponse?.Alarms[0];
        Assert.NotNull(firstAlarm);
        Assert.Equal("RINCON_AAAAAAAAAAAA01400", firstAlarm?.RoomUUID);
        Assert.Equal("WEEKDAYS", firstAlarm?.Recurrence);
        Assert.Equal("x-rincon-cpcontainer:1006206cspotify%3aplaylist%3a37i9dQZF1DX4WYpdgoIcn6?sid=9&flags=8300&sn=2", firstAlarm?.ProgramURI);
        Assert.Equal("SA_RINCON2311_X_#Svc2311-0-Token", firstAlarm?.ProgramMetaDataObject?.Items?[0].Desc?.Value);
    }
    [Fact]
    public async Task RenewEventSubscriptionAsync_CallsEventBus()
    {
        var uuid = Guid.NewGuid().ToString();
        var mockedEventBus = new Mock<ISonosEventBus>();
        mockedEventBus
            .Setup(_ => _.RenewSubscription(It.Is<string>(v => v.Equals(uuid)), It.Is<SonosService>(v => v == SonosService.AlarmClock), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(null, mockedEventBus.Object), uuid));

        var result = await sonos.AlarmClockService.RenewEventSubscriptionAsync();

        Assert.True(result);
        mockedEventBus.VerifyAll();
    }

    [Fact]
    public async Task RenewEventSubscriptionAsync_ReturnsFalse_WithoutEventBus()
    {
        var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider()));

        var result = await sonos.AlarmClockService.RenewEventSubscriptionAsync();

        Assert.False(result);
    }

    [Fact]
    public async Task SubscribeForEventsAsync_CallsEventBus()
    {
        var uuid = Guid.NewGuid().ToString();
        var mockedEventBus = new Mock<ISonosEventBus>();
        mockedEventBus
            .Setup(_ => _.Subscribe(It.Is<string>(v => v.Equals(uuid)), It.Is<SonosService>(v => v == SonosService.AlarmClock), It.Is<Uri>(v => v.ToString() == "http://localhost/AlarmClock/Event"), It.IsAny<Action<IServiceEvent>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(null, mockedEventBus.Object), uuid));

        var result = await sonos.AlarmClockService.SubscribeForEventsAsync();

        Assert.True(result);
        mockedEventBus.VerifyAll();
    }

    [Fact]
    public async Task SubscribeForEventsAsync_ReturnsFalse_WithoutEventBus()
    {
        var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider()));

        var result = await sonos.AlarmClockService.SubscribeForEventsAsync();

        Assert.False(result);
    }
}