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
        [InlineData("YES", "0 3 4 2")]
        [InlineData("NO", "0 2 5 3")]

        public void TestProff(string expectedResult, string testData)
        {
            string actualResult = TestHelper.StreamDataToTestHarness(testData, Solution.TestHarness); 
            Assert.Equal(expectedResult, actualResult);
        }

    }
    public class UnitTestFacts : TestHelper
    {
        public UnitTestFacts (ITestOutputHelper output)
        : base(output) { }


        [Fact]
        public void TestEqEq()
        {            
            var k1 = new Kangaroo(3,4);
            var k2 = new Kangaroo(3,4);
            Assert.True(k1==k2);
            Assert.True(k1.Equals(k2));
            k1.Move();
            k2.Move();
            Assert.True(k1==k2);
            Assert.True(k1.Equals(k2));
        }

        [Fact]
        public void TestNotEq()
        {            
            var k1 = new Kangaroo(3,4);
            var k2 = new Kangaroo(2,4);
            Assert.True(k1!=k2);
            k1.Move();
            k2.Move();
            Assert.True(k1!=k2);
        }

        [Fact]
        public void TestLt()
        {            
            var k1 = new Kangaroo(1,4);
            var k2 = new Kangaroo(2,4);
            Assert.True(k1<k2);
            k1.Move();
            k2.Move();
            Assert.True(k1<k2);
        }

        [Fact]
        public void TestGt()
        {            
            var k1 = new Kangaroo(3,4);
            var k2 = new Kangaroo(2,4);
            Assert.True(k1>k2);
            k1.Move();
            k2.Move();
            Assert.True(k1>k2);
        }

        [Fact]
        public void TestMove()
        {            
            var k1 = new Kangaroo(3,4);
            var k2 = new Kangaroo(2,6);
            Assert.True(k1>k2);
            k1.Move();
            k2.Move();
            Assert.True(k1<k2);
        }
    }
}
  