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
using System.Xml.Serialization;

namespace Sonos.Base.Services;

public partial class AlarmClockService
{
    public partial class ListAlarmsResponse
    {
        private AlarmCollection _alarms;

        [XmlIgnore]
        public Alarm[] Alarms
        {
            get
            {
                if (_alarms == null)
                {
                    _alarms = SoapFactory.ParseEmbeddedXml<AlarmCollection>(this.CurrentAlarmList);
                }
                return _alarms.Alarms;
            }
        }
    }

    public async Task<bool> PatchAlarmAsync(PatchAlarmRequest request, CancellationToken cancellationToken = default)
    {
        var alarm = (await this.ListAlarmsAsync(cancellationToken)).Alarms?.FirstOrDefault(a => a.ID == request.ID);
        if (alarm is null)
        {
            throw new SonosException("Alarm not found");
        }

        var updateRequest = new UpdateAlarmRequest
        {
            ID = request.ID,
            StartLocalTime = request.StartLocalTime ?? alarm.StartTime.ToString("HH:mm:ss"),
            Duration = request.Duration ?? alarm.Duration.ToString("HH:mm:ss"),
            Recurrence = request.Recurrence ?? alarm.Recurrence,
            Enabled = request.Enabled ?? alarm.Enabled == 1,
            RoomUUID = request.RoomUUID ?? alarm.RoomUUID,
            ProgramURI = request.ProgramURI ?? alarm.ProgramURI,
            ProgramMetaData = request.ProgramMetaData ?? alarm.ProgramMetaData,
            PlayMode = request.PlayMode ?? alarm.PlayMode,
            Volume = request.Volume ?? alarm.Volume,
            IncludeLinkedZones = request.IncludeLinkedZones ?? alarm.IncludeLinkedZones == 1
        };

        return await this.UpdateAlarmAsync(updateRequest, cancellationToken);

    }

    public class PatchAlarmRequest
    {
        /// <summary>
        /// The ID of the alarm see ListAlarms
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The start time as `hh:mm:ss`
        /// </summary>
        public string? StartLocalTime { get; set; }

        /// <summary>
        /// The duration as `hh:mm:ss`
        /// </summary>
        public string? Duration { get; set; }

        /// <summary>
        /// Repeat this alarm on
        /// </summary>
        public string? Recurrence { get; set; }

        /// <summary>
        /// Alarm enabled after creation
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// The UUID of the speaker you want this alarm for
        /// </summary>
        public string? RoomUUID { get; set; }

        /// <summary>
        /// The sound uri
        /// </summary>
        public string? ProgramURI { get; set; }

        /// <summary>
        /// The sound metadata, can be empty string
        /// </summary>
        public string? ProgramMetaData { get; set; }

        /// <summary>
        /// Alarm play mode
        /// </summary>
        public string? PlayMode { get; set; }

        /// <summary>
        /// Volume between 0 and 100
        /// </summary>
        public int? Volume { get; set; }

        /// <summary>
        /// Should grouped players also play the alarm?
        /// </summary>
        public bool? IncludeLinkedZones { get; set; }
    }
}