using System;
using System.IO;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestTheories
    {
        [Theory]
        [InlineData("", 1, new string[] {"", "", ""})]
        public void TestProff(string expectedResult, int testCount, string[] testData)
        {
            Stream inputData;
            Stream outputData;
            //Solution.TestHarness((new StreamReader(inputData), new StreamWriter(outputData));
            string actualResult= "no";
            Assert.Equal(expectedResult, actualResult);
        }
    }
    /*public class UnitTestFacts
    {
        public int[][] createArray(int x, int[] classSizes, int defaultValue)
        {
            int[][] mdArray = new int[x][];
            for (int i=0; i<x; i++) {
                int[] odArray = new int[y];
                for (int j=0; j<y; j++) {
                    odArray[j]=defaultValue;
                }
                mdArray[i]=odArray;
            }
            return mdArray;
        }
        [Fact]
        public void TestDiaginalDiff2x2()
        {
            int arraySize = 2;
            int[][] rules = createArray(arraySize, 2, 0);//top left corner = 1 makes backslash 1 more than forward slash
            int[][] arrivals = createArray(arraySize, 0);//top left corner = 1 makes backslash 1 more than forward slash
            //matrix[0][0] = 1;
            Assert.Equal(new string[] {"",""}, Solution.TestHarness(2, rules, arrivals));
        }
    }*/
}
