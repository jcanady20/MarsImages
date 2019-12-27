using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using MarsImages.Internal.Data;
using MarsImages.Internal.Data.Models;
using MarsImages.Controllers.Api;

namespace MarsImages.Tests.Controllers
{
    public class MarsImageControllerTests
    {
        private readonly static int _imageId = 766333;
        public MarsImageControllerTests()
        { }
        [Fact]
        public void Should_Return_NotFound_I()
        {
            var moq = new Mock<IMemoryCache>();
            moq.SetupGet(x => x.Dates).Returns((IReadOnlyList<ImageDate>)null);
            var controller = new MarsImageController(moq.Object);
            var result = controller.Get(_imageId);
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void Should_Return_NotFound_II()
        {
            var moq = new Mock<IMemoryCache>();
            moq.SetupGet(x => x.Dates).Returns(new List<ImageDate>());
            var controller = new MarsImageController(moq.Object);
            var result = controller.Get(_imageId);
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void Should_Return_NotFound_III()
        {
            var moq = new Mock<IMemoryCache>();
            moq.SetupGet(x => x.Dates).Returns(new List<ImageDate>() {
                new ImageDate()
            });
            var controller = new MarsImages.Controllers.Api.MarsImageController(moq.Object);
            var result = controller.Get(_imageId);
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void Should_Return_NotFound_IV()
        {
            var moq = new Mock<IMemoryCache>();
            moq.SetupGet(x => x.Dates).Returns(new List<ImageDate>() {
                new ImageDate()
                {
                    Photos = new List<Photo>()
                    {
                        new Photo()
                        {
                            Id = _imageId
                        }
                    }
                }
            });
            var controller = new MarsImages.Controllers.Api.MarsImageController(moq.Object);
            var result = controller.Get(_imageId);
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}