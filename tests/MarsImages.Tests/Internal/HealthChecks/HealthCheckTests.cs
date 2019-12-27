using System;
using Xunit;
using MarsImages.Internal.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;

namespace MarsImages.Tests.Internal.HealthChecks
{
    public class HealthCheckTests
    {
        public HealthCheckTests()
        { }
        [Fact]
        public async Task Should_Return_Healthy()
        {
            var chk = new HealthCheck();
            var  result = await chk.CheckHealthAsync(null);
            Assert.NotNull(result);
            Assert.IsType<HealthCheckResult>(result);
            Assert.Equal(HealthStatus.Healthy, result.Status);
        }
    }
}