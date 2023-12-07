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
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sonos.Base.Tests
{
    public class SonosDeviceTests
    {
        [Fact]
        public async Task Next_ExecutesCorrectCommand()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest(nameof(Services.SonosService.AVTransport), nameof(Services.AVTransportService.Next), "<InstanceID>0</InstanceID>");

            var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(mockedHandler.Object)));
            var result = await sonos.NextAsync();
            Assert.True(result);
        }

        [Fact]
        public async Task Pause_ExecutesCorrectCommand()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest(nameof(Services.SonosService.AVTransport), nameof(Services.AVTransportService.Pause), "<InstanceID>0</InstanceID>");

            var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(mockedHandler.Object)));
            var result = await sonos.PauseAsync();
            Assert.True(result);
        }

        [Fact]
        public async Task Play_ExecutesCorrectCommand()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest(nameof(Services.SonosService.AVTransport), nameof(Services.AVTransportService.Play), "<InstanceID>0</InstanceID><Speed>1</Speed>");

            var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(mockedHandler.Object)));
            var result = await sonos.PlayAsync();
            Assert.True(result);
        }

        [Fact]
        public async Task Previous_ExecutesCorrectCommand()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest(nameof(Services.SonosService.AVTransport), nameof(Services.AVTransportService.Previous), "<InstanceID>0</InstanceID>");

            var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(mockedHandler.Object)));
            var result = await sonos.PreviousAsync();
            Assert.True(result);
        }

        [Fact]
        public async Task Stop_ExecutesCorrectCommand()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest(nameof(Services.SonosService.AVTransport), nameof(Services.AVTransportService.Stop), "<InstanceID>0</InstanceID>");

            var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(mockedHandler.Object)));
            var result = await sonos.StopAsync();
            Assert.True(result);
        }


        [Theory]
        [InlineData(FakeData.S1DeviceDescription, "192.168.x.x - Sonos Play:5")]
        [InlineData(FakeData.S2DeviceDescription, "192.168.1.10 - Sonos One")]

        public async Task GetDeviceDescriptionAsync_ParsesDeviceDescription(string xml, string name)
        {
            var mockedHelper = new Mock<HttpClientHandler>();
            mockedHelper.MockDeviceDescription(deviceDescription: xml);
            var sonos = new SonosDevice(new SonosDeviceOptions(TestHelpers.DefaultUri, new StaticSonosServiceProvider(mockedHelper.Object)));
            var description = await sonos.GetDeviceDescriptionAsync();
            Assert.NotNull(description);
            Assert.Equal(name, description.device.friendlyName);
        }
    }
}