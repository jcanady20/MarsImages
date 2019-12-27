using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MarsImages.Internal.HealthChecks
{
    public class HealthCheck : IHealthCheck
    {
        public HealthCheck()
        { }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return HealthCheckResult.Healthy();
        }
    }
}