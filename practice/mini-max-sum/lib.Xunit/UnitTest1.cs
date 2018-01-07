using System;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestTheories
    {
        [Theory]
        [InlineData(new long[] {10,14}, new int[] {1, 2, 3, 4, 5})]
        [InlineData(new long[] {1,10}, new int[] {1, 2, 3, 4, -5})]
        [InlineData(new long[] {-14,-10}, new int[] {-1, -2, -3, -4, -5})]
        [InlineData(new long[] {4,4}, new int[] {1, 1, 1, 1, 1})]
        [InlineData(new long[] {-1,2}, new int[] {-2, -1, 1, 1, 1})]
        [InlineData(new long[] {0,0}, new int[] {0, 0, 0, 0, 0})]
        public void TestMiniMaxSum(long[] result, int[] input)
        {
            Assert.Equal(result, Solution.TestHarness(input));
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
            int maxArray = 5;
            int[] bigArray = createArray(maxArray, Int32.MaxValue);
            long[] result = new long[] {Convert.ToInt64(Int32.MaxValue)*4,Convert.ToInt64(Int32.MaxValue)*4};
            Assert.Equal(result, Solution.TestHarness(bigArray));
        }
    }
}
