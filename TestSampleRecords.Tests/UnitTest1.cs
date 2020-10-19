using System;
using Xunit;

namespace TestSampleRecords.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var a = 14;
            var b = 5;
            var c = a+b;

            Assert.Equal(19, c);

        }
    }
}
