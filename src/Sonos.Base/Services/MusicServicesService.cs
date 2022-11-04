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

using Sonos.Base.Soap;

namespace Sonos.Base.Services;

public partial class MusicServicesService
{
    public partial class ListAvailableServicesResponse
    {
        private MusicServiceCollection _musicServiceCollection;

        public MusicService[] MusicServices
        {
            get
            {
                if (_musicServiceCollection is null)
                {
                    _musicServiceCollection = SoapFactory.ParseEmbeddedXml<MusicServiceCollection>(this.AvailableServiceDescriptorList);
                }
                return _musicServiceCollection.Services;
            }
        }
    }
}
