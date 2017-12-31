using System;
using Xunit;

namespace lib.Xunit
{
    public static class testConstraints 
    {
        public const long bigNumber = 10000000000;
        //                                   12345678901

    }
    public class UnitTestTheories
    {
        [Theory]
        [InlineData(31, 6, new long[] {1,2,3,4,10,11}) ]//simple sum
        [InlineData(3, 2, new long[] {1,2}) ]
        [InlineData(1, 1, new long[] {1}) ]
        [InlineData(testConstraints.bigNumber, 2, new long[] {testConstraints.bigNumber,0}) ]//limit testing
        [InlineData(0, 2, new long[] {testConstraints.bigNumber,-testConstraints.bigNumber}) ]
        [InlineData(500, 3, new long[] {testConstraints.bigNumber,500, -testConstraints.bigNumber}) ]
        public void TestAdd(long result, int a, long[] b)
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
            int maxArray = 10;
            long[] bigArray = new long[maxArray];
            for (int i=0; i<maxArray; i++) {
                bigArray[i]=testConstraints.bigNumber;
            }
            Assert.Equal(testConstraints.bigNumber*maxArray, Solution.TestHarness(maxArray, bigArray));
        }
    }
}
