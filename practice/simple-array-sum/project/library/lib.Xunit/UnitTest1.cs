using System;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestTheories
    {
        [Theory]
        [InlineData(31, 6, new object[] {1,2,3,4,10,11}) ]//simple sum
        [InlineData(3, 2, new object[] {1,2}) ]
        [InlineData(1, 1, new object[] {1}) ]
        [InlineData(2147483647, 2, new object[] {2147483647,0}) ]//limit testing
        [InlineData(0, 2, new object[] {2147483647,-2147483647}) ]
        [InlineData(500, 3, new object[] {2147483647,500, -2147483647}) ]
        public void TestAdd(int result, int a, int[] b)
        {
            Assert.Equal(result, Solution.TestHarness(a, b));
        }
    }
    public class UnitTestFacts
    {
        [Fact]
        public void TestBigArray()
        {
            //Int32.Maxvalue throws System.OutOfMemoryException when creating the array
            //-2000000 was determined to work via experimentation
            //and scaled up to -5000000  for headroom
            int bigNumber = Int32.MaxValue-2000000;
            int[] bigArray = new int[bigNumber];
            for (int i=0; i<bigNumber; i++) {
                bigArray[i]=1;
            }
            Assert.Equal(bigNumber, Solution.TestHarness(bigNumber, bigArray));
        }
    }
}
