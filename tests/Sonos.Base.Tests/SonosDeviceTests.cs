﻿/*
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
            mockedHandler.MockSonosRequest("AVTransport", nameof(Services.AVTransportService.Next), "<InstanceID>0</InstanceID>");

            var sonos = new SonosDevice(TestHelpers.DefaultUri, httpClient: new HttpClient(mockedHandler.Object));
            var result = await sonos.Next();
            Assert.True(result);
        }

        [Fact]
        public async Task Pause_ExecutesCorrectCommand()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest("AVTransport", nameof(Services.AVTransportService.Pause), "<InstanceID>0</InstanceID>");

            var sonos = new SonosDevice(TestHelpers.DefaultUri, httpClient: new HttpClient(mockedHandler.Object));
            var result = await sonos.Pause();
            Assert.True(result);
        }

        [Fact]
        public async Task Play_ExecutesCorrectCommand()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest("AVTransport", nameof(Services.AVTransportService.Play), "<InstanceID>0</InstanceID><Speed>1</Speed>");

            var sonos = new SonosDevice(TestHelpers.DefaultUri, httpClient: new HttpClient(mockedHandler.Object));
            var result = await sonos.Play();
            Assert.True(result);
        }

        [Fact]
        public async Task Previous_ExecutesCorrectCommand()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest("AVTransport", nameof(Services.AVTransportService.Previous), "<InstanceID>0</InstanceID>");

            var sonos = new SonosDevice(TestHelpers.DefaultUri, httpClient: new HttpClient(mockedHandler.Object));
            var result = await sonos.Previous();
            Assert.True(result);
        }

        [Fact]
        public async Task Stop_ExecutesCorrectCommand()
        {
            var mockedHandler = new Mock<HttpClientHandler>();
            mockedHandler.MockSonosRequest("AVTransport", nameof(Services.AVTransportService.Stop), "<InstanceID>0</InstanceID>");

            var sonos = new SonosDevice(TestHelpers.DefaultUri, httpClient: new HttpClient(mockedHandler.Object));
            var result = await sonos.Stop();
            Assert.True(result);
        }
    }
}