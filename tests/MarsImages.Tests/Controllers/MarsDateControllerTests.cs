using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using MarsImages.Internal.Data;
using MarsImages.Internal.Data.Models;
using MarsImages.Controllers.Api;

namespace MarsImages.Tests.Controllers
{
    public class MarsDateControllerTests
    {
        public MarsDateControllerTests()
        { }
        [Fact]
        public void Should_Return_NotFound_I()
        {
            var moq = new Mock<IMemoryCache>();
            moq.SetupGet(x => x.Dates).Returns((IReadOnlyList<ImageDate>)null);
            var controller = new MarsDateController(moq.Object);
            var result = controller.Get();
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void Should_Return_NotFound_II()
        {
            var moq = new Mock<IMemoryCache>();
            moq.SetupGet(x => x.Dates).Returns(new List<ImageDate>());
            var controller = new MarsDateController(moq.Object);
            var result = controller.Get();
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void Should_Return_OK()
        {
            var moq = new Mock<IMemoryCache>();
            moq.SetupGet(x => x.Dates).Returns(new List<ImageDate>() {
                new ImageDate()
            });
            var controller = new MarsImages.Controllers.Api.MarsDateController(moq.Object);
            var result = controller.Get();
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}