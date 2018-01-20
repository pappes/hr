using System;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestTheories
    {
        [Theory]
        [InlineData(new int[] {1}, 1, new int[] {0})]
        [InlineData(new int[] {2}, 1, new int[] {1})]
        [InlineData(new int[] {3}, 1, new int[] {2})]
        [InlineData(new int[] {6}, 1, new int[] {3})]
        [InlineData(new int[] {7}, 1, new int[] {4})]
        [InlineData(new int[] {2147483647}, 1, new int[] {60})]
        [InlineData(new int[] {1, 2, 7}, 3, new int[] {0, 1, 4})]
        [InlineData(new int[] {1, 2}, 2, new int[] {0, 1})]
        [InlineData(new int[] {7, 6}, 2, new int[] {4, 3})]
        public void TestTree(int[] result, int size, int[] cases)
        {
            Assert.Equal(result, Solution.TestHarness(size, cases));
        }
    }
}