using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Services
{
    public partial class SystemPropertiesService
    {
        public async Task<string> GetString(string variableName, CancellationToken cancellationToken = default)
        {
            var result = await GetString(new GetStringRequest { VariableName = variableName }, cancellationToken);
            return result.StringValue;
        }

        public Task<bool> SetString(string variableName, string value, CancellationToken cancellationToken = default) => SetString(new SetStringRequest { VariableName = variableName, StringValue = value }, cancellationToken);
    }
}
