using System;
using System.IO;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestTheories
    {
        [Theory]
        [InlineData("yes", "1\r\n1 1\r\n0")]


        public void TestProff(string expectedResult, string testData)
        {
            //build streams to simulate stdin and stdout
            //that allows the test to control and monitor the data
            string actualResult; 
            using (Stream inputData = GenerateStreamFromString(testData))
            {   
                using (Stream outputDataStream = new MemoryStream())
                {
                    StreamWriter captureOutputData = new StreamWriter(outputDataStream);  
                    StreamReader sendInputData     = new StreamReader(inputData);   
                    Solution.TestHarness(sendInputData, captureOutputData);
                    actualResult = ReadFromStreamWriter(captureOutputData, outputDataStream);
                }
            }
            Assert.Equal(expectedResult, actualResult);
        }

        //helper method from https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
        static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        
        static string ReadFromStreamWriter(StreamWriter writer, Stream stream)
        {
            writer.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
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
