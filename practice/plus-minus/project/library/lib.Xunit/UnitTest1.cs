using System;
using Xunit;

namespace lib.Xunit
{

    public class UnitTestTheories
    {
        [Theory]
        [InlineData(new double[] {3.0/6,2.0/6,1.0/6}, 6, new object[] {-4, 3, -9, 0, 4, 1}) ]
        [InlineData(new double[] {0.0/6,6.0/6,0.0/6}, 6, new object[] {-4, -3, -9, -1, -4, -1}) ]
        [InlineData(new double[] {6.0/6,0.0/6,0.0/6}, 6, new object[] {4, 3, 9, 1, 4, 1}) ]
        [InlineData(new double[] {0.0/6,0.0/6,6.0/6}, 6, new object[] {0, 0, 0, 0, 0, 0}) ]
        [InlineData(new double[] {0.0/1,1.0/1,0.0/1}, 1, new object[] {-4}) ]
        [InlineData(new double[] {1.0/1,0.0/1,0.0/1}, 1, new object[] {4}) ]
        [InlineData(new double[] {0.0/1,0.0/1,1.0/1}, 1, new object[] {0}) ]
        public void TestPlusMinus(double[] result, int n, int[] arr)
        {
            Assert.Equal(result, Solution.TestHarness(n, arr));
        }
    }
    public class UnitTestFacts
    {
        public int[] createArray(int size, int defaultValue)
        {
            int[] tempArray = new int[size];
            for (int i=0; i<size; i++) {
                tempArray[i]=defaultValue;
            }
            return tempArray;
        }
        [Fact]
        public void TestPlusMinus()
        {
            int maxArray = 10000;
            double pool = Convert.ToDouble(maxArray);
            double pos = Convert.ToDouble(maxArray);
            double neg = 0;
            double zer = 0;
            int[] bigArray = createArray(maxArray, Int32.MaxValue);
            Assert.Equal(new double[] {pos/pool,neg/pool,zer/pool}, Solution.TestHarness(maxArray, bigArray));
            bigArray[0] = 0;
            pos--;
            zer++;
            Assert.Equal(new double[] {pos/pool,neg/pool,zer/pool}, Solution.TestHarness(maxArray, bigArray));
            bigArray[1] = -1;
            pos--;
            neg++;
            Assert.Equal(new double[] {pos/pool,neg/pool,zer/pool}, Solution.TestHarness(maxArray, bigArray));
        }
    }
}
