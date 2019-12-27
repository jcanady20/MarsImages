using System;
using Xunit;
using MarsImages.Internal.Data.Models;

namespace MarsImages.Tests.Internal.Data.Models
{
    public class ImageDateTests
    {
        public ImageDateTests()
        { }
        [Fact]
        public void Should_Set_Name_Property()
        {
            var expected = "ABC_123";
            var idt = new ImageDate()
            {
                Name = expected
            };
            Assert.Equal(expected, idt.Name);
        }
        [Fact]
        public void Should_Set_Name_Date()
        {
            var expected = new DateTime(2019, 01, 01);
            var idt = new ImageDate()
            {
                Date = expected
            };
            Assert.Equal(expected, idt.Date);
        }
        [Fact]
        public void Should_Set_Name_Status()
        {
            var expected = "ABC_282";
            var idt = new ImageDate()
            {
                Status = expected
            };
            Assert.Equal(expected, idt.Status);
        }
        [Fact]
        public void Should_Equal_Other()
        {
            var dt = new DateTime(2019, 01, 01);
            var idt1 = new ImageDate()
            {
                Date = dt
            };
            var idt2 = new ImageDate()
            {
                Date = dt
            };
            Assert.Equal(idt1, idt2);
        }
        [Fact]
        public void Should_Calculate_HashCode()
        {
            var dt = new DateTime(2019, 01, 01);
            var idt = new ImageDate()
            {
                Date = dt
            };
            var hash = dt.GetHashCode();
            Assert.Equal(hash, idt.GetHashCode());
        }
        [Fact]
        public void Should_Operator_Equal_Other()
        {
            var dt = new DateTime(2019, 01, 01);
            var idt1 = new ImageDate()
            {
                Date = dt
            };
            var idt2 = new ImageDate()
            {
                Date = dt
            };
            Assert.True(idt1 == idt2);
        }
        [Fact]
        public void Should_Operator_NOT_Equal_Other()
        {
            var idt1 = new ImageDate()
            {
                Date = new DateTime(2019, 01, 01)
            };
            var idt2 = new ImageDate()
            {
                Date = new DateTime(2019,01, 02)
            };
            Assert.True(idt1 != idt2);
        }
    }
}