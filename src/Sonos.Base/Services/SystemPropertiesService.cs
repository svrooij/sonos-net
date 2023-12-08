using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonos.Base.Services
{
    public partial class SystemPropertiesService
    {
        public async Task<string> GetStringAsync(string variableName, CancellationToken cancellationToken = default)
        {
            var result = await GetStringAsync(new GetStringRequest { VariableName = variableName }, cancellationToken);
            return result.StringValue;
        }

        public Task<bool> SetStringAsync(string variableName, string value, CancellationToken cancellationToken = default) => SetStringAsync(new SetStringRequest { VariableName = variableName, StringValue = value }, cancellationToken);
    }
}
