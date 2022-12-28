namespace Sonos.Base.Events.Http.Tests;

public class SonosEventReceiverTests
{
    [Fact]
    public async Task RenewSubscription_ReturnsFalse_WhenNotSubscribed()
    {
        var uuid = Guid.NewGuid().ToString();
        var service = Services.SonosService.AlarmClock;
        var mockedHandler = new Mock<HttpClientHandler>();
        var httpClient = new HttpClient(mockedHandler.Object);

        var eventReceiver = new SonosEventReceiver(httpClient);

        var result = await eventReceiver.RenewSubscription(uuid, service);

        Assert.False(result, "RenewSubscription did not return false");

        mockedHandler.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task RenewSubscription_ReturnsTrue_WhenPreviouslySubscribed()
    {
        var uuid = Guid.NewGuid().ToString();
        var service = Services.SonosService.AlarmClock;
        var sid = Guid.NewGuid().ToString();
        var eventUri = new Uri("http://localhost:8888/eventmock/");
        var mockedHandler = new Mock<HttpClientHandler>();
        mockedHandler.MockEventRequest(eventUri, "SUBSCRIBE", new Dictionary<string, string> { { "callback", $"<http://sonosevents.local:6329/event/{uuid}/{service}>" }, { "NT", "upnp:event" }, { "Timeout", "Second-1800" } }, new Dictionary<string, string> { { "sid", sid } });
        mockedHandler.MockEventRequest(eventUri, "SUBSCRIBE", new Dictionary<string, string> { { "SID", sid }, { "Timeout", "Second-1800" } });

        var httpClient = new HttpClient(mockedHandler.Object);

        var eventReceiver = new SonosEventReceiver(httpClient);

        var result = await eventReceiver.Subscribe(uuid, service, eventUri, (e) => { });

        Assert.True(result, "Subscribe did not return true");

        var result2 = await eventReceiver.RenewSubscription(uuid, service);

        Assert.True(result2, "RenewSubscription did not return true");


        mockedHandler.VerifyAll();
    }

    [Fact]
    public async Task Subscribe_SendsExpectedHttpCall()
    {
        var uuid = Guid.NewGuid().ToString();
        var service = Services.SonosService.AlarmClock;
        var sid = Guid.NewGuid().ToString();
        var eventUri = new Uri("http://localhost:8888/eventmock/");
        var mockedHandler = new Mock<HttpClientHandler>();
        mockedHandler.MockEventRequest(eventUri, "SUBSCRIBE", new Dictionary<string, string> { { "callback", $"<http://sonosevents.local:6329/event/{uuid}/{service}>" }, { "NT", "upnp:event" }, { "Timeout", "Second-1800" } }, new Dictionary<string, string> { { "sid", sid } });

        var httpClient = new HttpClient(mockedHandler.Object);

        var eventReceiver = new SonosEventReceiver(httpClient);

        var result = await eventReceiver.Subscribe(uuid, service, eventUri, (e) => { });

        Assert.True(result);
        mockedHandler.VerifyAll();
    }
    [Fact]
    public async Task Unsubscribe_ReturnsFalse_WhenNotSubscribed()
    {
        var uuid = Guid.NewGuid().ToString();
        var service = Services.SonosService.AlarmClock;
        var mockedHandler = new Mock<HttpClientHandler>();
        var httpClient = new HttpClient(mockedHandler.Object);

        var eventReceiver = new SonosEventReceiver(httpClient);

        var result = await eventReceiver.Unsubscribe(uuid, service);

        Assert.False(result, "RenewSubscription did not return false");

        mockedHandler.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Unsubscribe_ReturnsTrue_WhenPreviouslySubscribed()
    {
        var uuid = Guid.NewGuid().ToString();
        var service = Services.SonosService.AlarmClock;
        var sid = Guid.NewGuid().ToString();
        var eventUri = new Uri("http://localhost:8888/eventmock/");

        var mockedHandler = new Mock<HttpClientHandler>();
        mockedHandler.MockEventRequest(eventUri, "SUBSCRIBE", new Dictionary<string, string> { { "callback", $"<http://sonosevents.local:6329/event/{uuid}/{service}>" }, { "NT", "upnp:event" }, { "Timeout", "Second-1800" } }, new Dictionary<string, string> { { "sid", sid } });
        mockedHandler.MockEventRequest(eventUri, "UNSUBSCRIBE", new Dictionary<string, string> { { "SID", sid }});
        var httpClient = new HttpClient(mockedHandler.Object);

        var eventReceiver = new SonosEventReceiver(httpClient);

        var result = await eventReceiver.Subscribe(uuid, service, eventUri, (e) => { });
        Assert.True(result, "Subscribe did not return true");

        var result2 = await eventReceiver.Unsubscribe(uuid, service);
        Assert.True(result2, "Unsubscribe did not return true");

        mockedHandler.VerifyAll();
    }
}