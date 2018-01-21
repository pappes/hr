using System;
using System.IO;
using System.Text;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestTheories
    {
        [Theory]
        [InlineData("NO", "1\r\n1 1\r\n0 0")]
        [InlineData("NO", "1\r\n1 1\r\n0")]
        [InlineData("NO", "1\r\n10 5\r\n-4 -3 -2 -1 0 1 2 3 4 5 6")]
        [InlineData("NO", "1\r\n1 1\r\n0")]
        [InlineData("YES", "1\r\n10 5\r\n-3 -2 -1 0 1 2 3 4 5 6")]
        [InlineData("YES", "1\r\n1 1\r\n1")]
        [InlineData("?", "1\r\n10 5\r\n-2 -1 0 1")]
        [InlineData(
@"YES
NO", 
@"2
4 3
-1 -3 4 2
4 2
0 -1 2 1")]

        public void TestProff(string expectedResult, string testData)
        {
            string actualResult = TestHelper.StreamDataToTestHarness(testData); 
            Assert.Equal(expectedResult, actualResult);
        }

    }

    public class UnitTestFacts
    {
        [Fact]
        public void TestMaxClassOK()
        {
            string expectedResult = ""; 
            char[] charsToTrim = {'\r', '\n', ' '};
            int maxTests = 10;
            int maxClass = 1000;
            int threshold = maxClass;
            StringBuilder testData = new StringBuilder("", maxTests*maxClass*2+maxTests*15);
            testData.Append( $"{maxTests}\r\n");
            for (int i=0; i< maxTests; i++){
                testData.Append($"{maxClass} {threshold}\r\n");
                for (int j=0; j<maxClass; j++){
                    testData.Append( j<maxClass-1 ? "0 " : "0" );
                }
                testData.AppendLine();
                expectedResult = expectedResult + "NO\r\n";
            }
            string actualResult = TestHelper.StreamDataToTestHarness(testData.ToString());
            Assert.Equal(expectedResult.TrimEnd(charsToTrim), actualResult.TrimEnd(charsToTrim));
        }
        public void TestMaxClassFail()
        {
            string expectedResult = ""; 
            char[] charsToTrim = {'\r', '\n', ' '};
            int maxTests = 10;
            int maxClass = 1000;
            int threshold = maxClass;
            StringBuilder testData = new StringBuilder("", maxTests*maxClass*2+maxTests*15);
            testData.Append( $"{maxTests}\r\n");
            for (int i=0; i< maxTests; i++){
                testData.Append($"{maxClass} {threshold}\r\n");
                for (int j=0; j<maxClass; j++){
                    testData.Append( j<maxClass-1 ? "0 " : "1" );
                }
                testData.AppendLine();
                expectedResult = expectedResult + "YES\r\n";
            }
            string actualResult = TestHelper.StreamDataToTestHarness(testData.ToString());
            Assert.Equal(expectedResult.TrimEnd(charsToTrim), actualResult.TrimEnd(charsToTrim));
        }
        public void TestSuperLargeClass()
        {
            string expectedResult = ""; 
            char[] charsToTrim = {'\r', '\n', ' '};
            int maxTests = 100;
            int maxClass = 10000;
            int threshold = maxClass;
            StringBuilder testData = new StringBuilder("", maxTests*maxClass*2+maxTests*15);
            testData.Append( $"{maxTests}\r\n");
            for (int i=0; i< maxTests; i++){
                testData.Append($"{maxClass} {threshold}\r\n");
                for (int j=0; j<maxClass; j++){
                    testData.Append( j<maxClass-1 ? "0 " : "1" );
                }
                testData.AppendLine();
                expectedResult = expectedResult + "YES\r\n";
            }
            string actualResult = TestHelper.StreamDataToTestHarness(testData.ToString());
            Assert.Equal(expectedResult.TrimEnd(charsToTrim), actualResult.TrimEnd(charsToTrim));
        }
    }
    public class TestHelper
    {
        public static String StreamDataToTestHarness(string testData)
        {
            //build streams to simulate stdin and stdout
            //that allows the test to control and monitor the data
            string actualResult; 
            using (Stream inputData = TestHelper.GenerateStreamFromString(testData))
            {   
                using (Stream outputDataStream = new MemoryStream())
                {
                    StreamWriter captureOutputData = new StreamWriter(outputDataStream);  
                    StreamReader sendInputData     = new StreamReader(inputData);   
                    Solution.TestHarness(sendInputData, captureOutputData);
                    actualResult = TestHelper.ReadFromStreamWriter(captureOutputData, outputDataStream);
                }
            }
            char[] charsToTrim = {'\r', '\n', ' '};
            return actualResult.TrimEnd(charsToTrim);
        }
        //helper method from https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        
        public static string ReadFromStreamWriter(StreamWriter writer, Stream stream)
        {
            writer.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
