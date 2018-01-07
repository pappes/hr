using System;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestFacts
    {
        public int[][] createArray(int size, int defaultValue)
        {
            int[][] mdArray = new int[size][];
            for (int i=0; i<size; i++) {
                int[] odArray = new int[size];
                for (int j=0; j<size; j++) {
                    odArray[j]=defaultValue;
                }
                mdArray[i]=odArray;
            }
            return mdArray;
        }
        [Fact]
        public void TestDiaginalDiff3x3()
        {
            int arraySize = 3;
            int[][] matrix = createArray(arraySize, 0);
            Assert.Equal(0, Solution.TestHarness(arraySize, matrix));//matrix full of 0 has no difference on axis
            matrix[0][0] = 1;
            Assert.Equal(1, Solution.TestHarness(arraySize, matrix));//top left corner = 1 makes backslash 1 more than forward slash
            matrix[2][0] = 2;
            Assert.Equal(1, Solution.TestHarness(arraySize, matrix));//top right corner = 2 makes backslash 1 less than forward slash
            matrix[0][0] = 0;
            Assert.Equal(2, Solution.TestHarness(arraySize, matrix));//top left corner = 0 makes backslash 2 less than forward slash
        }
        [Fact]
        public void TestDiaginalDiff2x2()
        {
            int arraySize = 2;
            int[][] matrix = createArray(arraySize, 0);//top left corner = 1 makes backslash 1 more than forward slash
            matrix[0][0] = 1;
            Assert.Equal(1, Solution.TestHarness(arraySize, matrix));
        }
        [Fact]
        public void TestDiaginalDiff1x1()
        {
            int arraySize = 1;
            int[][] matrix = createArray(arraySize, 0);//diagonals are always equal with array size [1][1]
            matrix[0][0] = 1;
            Assert.Equal(0, Solution.TestHarness(arraySize, matrix));
        }
        [Fact]
        public void TestDiaginalDiff100x100()
        {
            int maxArray = 100;
            int[][] bigArray = createArray(maxArray, Int32.MaxValue);//large array with large values
            Assert.Equal(0, Solution.TestHarness(maxArray, bigArray));
            bigArray[0][0] = 0;
            Assert.Equal(Int32.MaxValue, Solution.TestHarness(maxArray, bigArray));//top left corner = 0 makes backslash (Int32.MaxValue) less than forward slash
        }
    }
}
