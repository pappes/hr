using System;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestFacts
    {
        [Fact]
        public void SimpleAdd()
        {
            Assert.Equal(2, Solution.TestHarness(1,1));
            Assert.Equal(0, Solution.TestHarness(0,0));
            Assert.Equal(20, Solution.TestHarness(10,10));
            Assert.Equal(3, Solution.TestHarness(1,2));
            Assert.Equal(3, Solution.TestHarness(2,1));
        }
        [Fact]
        public void LargeAdd()
        {
            Assert.Equal(200000000, Solution.TestHarness(100000000,100000000));
            Assert.Equal(-1, Solution.TestHarness(-2147483648,2147483647));
            Assert.Equal(0, Solution.TestHarness(-2147483647,2147483647));
            Assert.Equal(2147483647, Solution.TestHarness(2147483646,1));
            Assert.Equal(-2147483648, Solution.TestHarness(-2147483647,-1));
            Assert.Equal(-2147483648, Solution.TestHarness(2147483647,1));
            Assert.Equal(2147483647, Solution.TestHarness(-2147483648,-1));
        }
        [Fact]
        public void NegativeAdd()
        {
            Assert.Equal(0, Solution.TestHarness(1,-1));
            Assert.Equal(-2, Solution.TestHarness(-1,-1));
            Assert.Equal(0, Solution.TestHarness(-0,0));
            Assert.Equal(0, Solution.TestHarness(-0,-0));
        }
    }
    public class UnitTestTheories
    {
        [Theory]
        [InlineData(2,1,1) ]//simmple add
        [InlineData(0,0,0) ]
        [InlineData(20,10,10) ]
        [InlineData(3,1,2) ]
        [InlineData(3,2,1) ]
        [InlineData(200000000,100000000,100000000) ] //large add
        [InlineData(-1,-2147483648,2147483647) ]
        [InlineData(0,-2147483647,2147483647) ]
        [InlineData(2147483647,2147483646,1) ]
        [InlineData(-2147483648,-2147483647,-1) ]
        [InlineData(-2147483648,2147483647,1) ]//roll over lower bound
        [InlineData(2147483647,-2147483648,-1) ]//roll over upper bound
        public void TestAdd(int result, int a, int b)
        {
            Assert.Equal(result, Solution.TestHarness(a, b));
        }
    }
}
