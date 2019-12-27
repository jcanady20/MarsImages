using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MarsImages.Internal.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MarsImages.Tests.Internal.Middleware
{
    public class ErrorHandlingTests
    {
        private readonly Mock<ILogger<ErrorHandling>> _mockLogger;
        private readonly HttpContext _httpContext;
        public ErrorHandlingTests()
        {
            _mockLogger = new Mock<ILogger<ErrorHandling>>();
            _httpContext = new DefaultHttpContext();
        }

        [Fact]
        public async Task Should_Call_Next()
        {
            var nextCalled = false;
            RequestDelegate next = (HttpContext context) => {
                nextCalled = true;
                return Task.FromResult(0);
            };
            var handler = new ErrorHandling(_mockLogger.Object, next);
            await handler.Invoke(_httpContext);
            Assert.True(nextCalled);
        }
    }
}