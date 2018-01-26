using System;
using Xunit;
using Xunit.Abstractions;

namespace lib.Xunit
{
    public class UnitTestTheories : TestHelper
    {
        public UnitTestTheories (ITestOutputHelper output)
        : base(output) { }


        [Theory]
        [InlineData("NO", "1\r\n1 1\r\n0 0")]
        [InlineData("NO", "1\r\n1 1\r\n0")]

        public void TestProff(string expectedResult, string testData)
        {
            string actualResult = TestHelper.StreamDataToTestHarness(testData, Solution.TestHarness); 
            Assert.Equal(expectedResult, actualResult);
        }

    }
}
