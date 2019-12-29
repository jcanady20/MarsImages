using System;
using Xunit;
using Moq;
using MarsImages.Internal.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace MarsImages.Tests.Internal.Services
{
    public class MarsIMageMetaDataServiceTests
    {
        private readonly Mock<ILogger<MarsImageMetaDataService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IMarsImageMetaDataHttpClient> _mockHttpClient;
        public MarsIMageMetaDataServiceTests()
        {
            _mockLogger = new Mock<ILogger<MarsImageMetaDataService>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.SetupGet(m => m[It.IsAny<string>()]).Returns("ABYOLSOLSSlll1111");
            _mockHttpClient = new Mock<IMarsImageMetaDataHttpClient>();
            _mockHttpClient.SetupGet(m => m.BaseAddress).Returns(new Uri("http://localhost:222"));

        }
        [Fact]
        public async Task Should_Call_HttpClient()
        {
            _mockHttpClient.Setup(m => m.GetRoverMetaDataByDateAsync(It.IsAny<System.Uri>())).ReturnsAsync(new MarsImages.Internal.Data.Models.ImageResponse());

            var srv = new MarsImageMetaDataService(_mockLogger.Object, _mockConfiguration.Object, _mockHttpClient.Object);
            var result = await srv.GetMetaDataByDateAsync(DateTime.Now);
            _mockHttpClient.Verify(m => m.GetRoverMetaDataByDateAsync(It.IsAny<System.Uri>()), Times.Once);
        }
        [Fact]
        public async Task Should_Log_Exception()
        {
            _mockHttpClient.Setup(m => m.GetRoverMetaDataByDateAsync(It.IsAny<System.Uri>())).ThrowsAsync(new Exception());
            var srv = new MarsImageMetaDataService(_mockLogger.Object, _mockConfiguration.Object, _mockHttpClient.Object);
            var result = await srv.GetMetaDataByDateAsync(DateTime.Now);
            //_mockLogger.Verify(l => l.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), null, It.IsAny<Func<object, Exception, string>>()), Times.Once);
        }
    }
}