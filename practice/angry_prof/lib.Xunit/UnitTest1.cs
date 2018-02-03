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
        [InlineData("NO", "1\r\n10 5\r\n-2 -1 0 1")]
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
            TestLargeClass();
        }
        public void TestMaxClassFail()
        {
            TestLargeClass(lastStudentTime: 1);
        }
        public void TestSuperLargeClass()
        {
            TestLargeClass(classSize: 10000);
        }
        public void TestLargeClass(int classSize = 1000, int lastStudentTime = default(int))
        {
            //construct a test case dynamically by building up the input and output strings
            var combinedResults = ""; 
            char[] charsToTrim = {'\r', '\n', ' '};
            var maxTests = 10;
            var threshold = classSize;
            var expectedResult = lastStudentTime >0 ? "YES": "NO";
            //start the string builder big enough to hold the final sring size
            StringBuilder testData = new StringBuilder("", maxTests*classSize*2+maxTests*15);
            testData.Append( $"{maxTests}\r\n");
    //XUnitWriteLine($"maxTests:{maxTests} classSize:{classSize} threshold:{threshold} testData:{testData}");
            for (var i=0; i< maxTests; i++){
                testData.Append($"{classSize} {threshold}\r\n");
                for (var j=0; j<classSize; j++){
                    testData.Append( j<classSize-1 ? "0 " : lastStudentTime.ToString() );
                }
                testData.AppendLine();
                combinedResults = combinedResults + $"{expectedResult}\r\n";
            }
            string actualResult = TestHelper.StreamDataToTestHarness(testData.ToString(), Solution.Services.Solution.TestHarness);
            Assert.Equal(combinedResults.TrimEnd(charsToTrim), actualResult.TrimEnd(charsToTrim));
        }
    }
    public class UnitTestMethods
    {
        [Fact]
        public void TestLectureTheatre()
        {
            var testClass = new LectureTheatre();
            testClass.InitialiseStatistics(1,2);
            Assert.Equal(testClass.ClassSize, 1);
            Assert.Equal(testClass.CancellationThreshold, 2);
            Assert.Equal(testClass.OnTimeStudents, default(int));
            Assert.Equal(testClass.LateStudents, default(int));
            testClass.UpdateStatistics(0);
            Assert.Equal(testClass.ClassSize, 1);
            Assert.Equal(testClass.CancellationThreshold, 2);
            Assert.Equal(testClass.OnTimeStudents, 1);
            Assert.Equal(testClass.LateStudents, default(int));
            testClass.UpdateStatistics(-1);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(2, testClass.OnTimeStudents);
            Assert.Equal(default(int), testClass.LateStudents);
            testClass.UpdateStatistics(int.MinValue);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(3, testClass.OnTimeStudents);
            Assert.Equal(default(int), testClass.LateStudents);
            testClass.UpdateStatistics(1);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(3, testClass.OnTimeStudents);
            Assert.Equal(1, testClass.LateStudents);
            testClass.UpdateStatistics(int.MaxValue);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(3, testClass.OnTimeStudents);
            Assert.Equal(2, testClass.LateStudents);
        }
        public void TestUnsubscriberLambda() 
        {
            var canary=1;
            using (var x = new UnsubscriberLambda(() => canary=2)) 
            {
                Assert.Equal(1, canary);
            }
            Assert.Equal(2, canary);

        }
    }

//TODO: unit test individual methods
}
