using System;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestTheories
    {
        [Theory]
        [InlineData("19:05:45", "07:05:45PM")]
        [InlineData("07:05:45", "07:05:45AM")]
        [InlineData("12:05:45", "12:05:45PM")]
        [InlineData("00:05:45", "12:05:45AM")]
        [InlineData("12:00:00", "12:00:00PM")]
        [InlineData("00:00:00", "12:00:00AM")]
        public void TestConversion(string result, string inp)
        {
            Assert.Equal(result, Solution.TestHarness(inp));
        }
    }
}
