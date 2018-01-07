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
    }/* *
    public class UnitTestFacts
    {
        public int[] createArray(int size, int defaultValue)
        {
            int[] mdArray = new int[size];
            for (int i=0; i<size; i++) {
                mdArray[i]=defaultValue;
            }
            return mdArray;
        }
        [Fact]
        public void TestDiaginalDiff1millionx1million()
        {
            int maxArray = 100000;  ///problem defines maximum 10^5 candles
            int[] bigArray = createArray(maxArray, Int32.MaxValue);
            Assert.Equal(maxArray, Solution.TestHarness(maxArray, bigArray));
            bigArray[0]=0;
            Assert.Equal(maxArray-1, Solution.TestHarness(maxArray, bigArray));
            bigArray = createArray(maxArray, Int32.MinValue);
            bigArray[0]=0;
            Assert.Equal(1, Solution.TestHarness(maxArray, bigArray));
        }
    }*/
}
