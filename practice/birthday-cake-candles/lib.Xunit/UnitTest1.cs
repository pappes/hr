using System;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestTheories
    {
        [Theory]
        [InlineData(1, 1, new int[] {1})]
        [InlineData(3, 3, new int[] {1,1,1})]
        [InlineData(1, 3, new int[] {1,2,3})]
        [InlineData(2, 3, new int[] {1,3,3})]
        public void TestCandles(int result, int n, int[] ar)
        {
            Assert.Equal(result, Solution.TestHarness(n, ar));
        }
    }
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
    }
}
