using System;
using Xunit;
using MarsImages.Internal.Extensions;

namespace MarsImages.Tests.Internal.Extensions
{
    public class StringExtensionTests
    {
        public StringExtensionTests()
        { }
        [Fact]
        public void Should_Return_Date_I()
        {
            var dt = "2019-01-01".ToDateTime();
            Assert.NotNull(dt);
            Assert.True(dt?.Year == 2019);
        }
        [Fact]
        public void Should_Return_Date_II()
        {
            var dt = "02/27/17".ToDateTime();
            Assert.NotNull(dt);
            Assert.True(dt?.Year == 2017);
            Assert.True(dt?.Month == 2);
            Assert.True(dt?.Day == 27);
        }
        [Fact]
        public void Should_Return_Date_III()
        {
            var dt = "June 2, 2018".ToDateTime();
            Assert.NotNull(dt);
            Assert.True(dt?.Year == 2018);
            Assert.True(dt?.Month == 6);
            Assert.True(dt?.Day == 2);
        }
        [Fact]
        public void Should_Return_Date_IV()
        {
            var dt = "Jul-13-2016".ToDateTime();
            Assert.NotNull(dt);
            Assert.True(dt?.Year == 2016);
            Assert.True(dt?.Month == 7);
            Assert.True(dt?.Day == 13);
        }
        [Fact]
        public void Should_Return_Null()
        {
            var dt = "April 31, 2018".ToDateTime();
            Assert.Null(dt);
        }
    }
}