using System;
using Xunit;

namespace MarsImages.Tests.Internal.Data
{
    public class MemoryCacheTests
    {
        public MemoryCacheTests()
        { }
        [Fact]
        public void Should_Add_Object()
        {
            var memCache = new MarsImages.Internal.Data.MemoryCache();
            var imageDate = new MarsImages.Internal.Data.Models.ImageDate();
            imageDate.Date = new DateTime(2019, 01, 01);
            memCache.Add(imageDate);
            Assert.NotEmpty(memCache.Dates);
            var obj = memCache.Dates[0];
            Assert.Equal(imageDate, obj);
        }
        [Fact]
        public void Should_Initialize_Dates()
        {
            var memCache = new MarsImages.Internal.Data.MemoryCache();
            Assert.NotNull(memCache.Dates);
        }
        [Fact]
        public void Should_Reject_Object()
        {
            var memCache = new MarsImages.Internal.Data.MemoryCache();
            var imageDateI = new MarsImages.Internal.Data.Models.ImageDate();
            var imageDateII = new MarsImages.Internal.Data.Models.ImageDate();
            imageDateI.Date = new DateTime(2019, 01, 01);
            imageDateII.Date = new DateTime(2019, 01, 01);
            memCache.Add(imageDateI);
            memCache.Add(imageDateII);
            Assert.NotEmpty(memCache.Dates);
            Assert.True(memCache.Dates.Count == 1);
            
        }

    }
}