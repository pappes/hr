using System;
using Xunit;

namespace lib.Xunit
{

    public class UnitTestTheories
    {
        [Theory]
        [InlineData(new object[] {0,3}, 1,1,1,2,2,2) ]
        [InlineData(new object[] {3,0}, 2,2,2,1,1,1) ]
        [InlineData(new object[] {0,0}, 1,1,1,1,1,1) ]
        [InlineData(new object[] {0,0}, 0,0,0,0,0,0) ]
        [InlineData(new object[] {3,0}, Int32.MaxValue,Int32.MaxValue,Int32.MaxValue,Int32.MinValue,Int32.MinValue,Int32.MinValue) ]
        public void TestTriplets(int[] result, int a, int b, int c, int d, int e, int f)
        {
            Assert.Equal(result, Solution.TestHarness(a, b,c,d,e,f));
        }
    }
}
