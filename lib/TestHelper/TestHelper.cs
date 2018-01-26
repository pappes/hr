using System;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

    public class TestHelper 
    {
        private readonly ITestOutputHelper xUnitConsole;

        public TestHelper(ITestOutputHelper output)
        {
            this.xUnitConsole = output;
        }
        public void XUnitWriteLine(string testData)
        {        
            xUnitConsole.WriteLine(testData);
        }
        public static String StreamDataToTestHarness(string testData, Action<StreamReader,StreamWriter> testHarness)
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
                    testHarness(sendInputData, captureOutputData);
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
