using System;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Solution.Services;

namespace lib.Xunit
{
    public class UnitTestTheories : TestHelper
    {
        public UnitTestTheories (ITestOutputHelper output)
        : base(output) { }


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
            string actualResult = TestHelper.StreamDataToTestHarness(testData, Solution.Services.Solution.TestHarness); 
            Assert.Equal(expectedResult, actualResult);
        }

    }

    public class UnitTestFacts : TestHelper
    {
        public UnitTestFacts (ITestOutputHelper output)
        : base(output) { }

        [Fact]
        public void TestMaxClassOK()
        {
            var expectedResult = ""; 
            char[] charsToTrim = {'\r', '\n', ' '};
            var maxTests = 10;
            var maxClass = 1000;
            var threshold = maxClass;
            StringBuilder testData = new StringBuilder("", maxTests*maxClass*2+maxTests*15);
    //XUnitWriteLine($"maxTests:{maxTests} maxClass:{maxClass} threshold:{threshold} testData:{testData}");
            testData.Append( $"{maxTests}\r\n");
            for (var i=0; i< maxTests; i++){
                testData.Append($"{maxClass} {threshold}\r\n");
                for (var j=0; j<maxClass; j++){
                    testData.Append( j<maxClass-1 ? "0 " : "0" );
                }
                testData.AppendLine();
                expectedResult = expectedResult + "NO\r\n";
            }
            string actualResult = TestHelper.StreamDataToTestHarness(testData.ToString(), Solution.Services.Solution.TestHarness);
            Assert.Equal(expectedResult.TrimEnd(charsToTrim), actualResult.TrimEnd(charsToTrim));
        }
        public void TestMaxClassFail()
        {
            var expectedResult = ""; 
            char[] charsToTrim = {'\r', '\n', ' '};
            var maxTests = 10;
            var maxClass = 1000;
            var threshold = maxClass;
            StringBuilder testData = new StringBuilder("", maxTests*maxClass*2+maxTests*15);
            testData.Append( $"{maxTests}\r\n");
            for (var i=0; i< maxTests; i++){
                testData.Append($"{maxClass} {threshold}\r\n");
                for (var j=0; j<maxClass; j++){
                    testData.Append( j<maxClass-1 ? "0 " : "1" );
                }
                testData.AppendLine();
                expectedResult = expectedResult + "YES\r\n";
            }
            string actualResult = TestHelper.StreamDataToTestHarness(testData.ToString(), Solution.Services.Solution.TestHarness);
            Assert.Equal(expectedResult.TrimEnd(charsToTrim), actualResult.TrimEnd(charsToTrim));
        }
        public void TestSuperLargeClass()
        {
            var expectedResult = ""; 
            char[] charsToTrim = {'\r', '\n', ' '};
            var maxTests = 100;
            var maxClass = 10000;
            var threshold = maxClass;
            StringBuilder testData = new StringBuilder("", maxTests*maxClass*2+maxTests*15);
            testData.Append( $"{maxTests}\r\n");
            for (var i=0; i< maxTests; i++){
                testData.Append($"{maxClass} {threshold}\r\n");
                for (var j=0; j<maxClass; j++){
                    testData.Append( j<maxClass-1 ? "0 " : "1" );
                }
                testData.AppendLine();
                expectedResult = expectedResult + "YES\r\n";
            }
            string actualResult = TestHelper.StreamDataToTestHarness(testData.ToString(), Solution.Services.Solution.TestHarness);
            Assert.Equal(expectedResult.TrimEnd(charsToTrim), actualResult.TrimEnd(charsToTrim));
        }
    }


}
