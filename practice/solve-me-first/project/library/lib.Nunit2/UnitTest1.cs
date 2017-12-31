using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimpleAdd()
        {
            Assert.That(2, Is.EqualTo(Solution.TestHarness(1,1)));
            Assert.That(0, Is.EqualTo(Solution.TestHarness(0,0)));
            Assert.That(20, Is.EqualTo(Solution.TestHarness(10,10)));
            Assert.That(3, Is.EqualTo(Solution.TestHarness(1,2)));
            Assert.That(3, Is.EqualTo(Solution.TestHarness(2,1)));
        }
        [Test]
        public void LargeAdd()
        {
            Assert.That(200000000, Is.EqualTo(Solution.TestHarness(100000000,100000000)));
            Assert.That(-1, Is.EqualTo(Solution.TestHarness(-2147483648,2147483647)));
            Assert.That(0, Is.EqualTo(Solution.TestHarness(-2147483647,2147483647)));
            Assert.That(2147483647, Is.EqualTo(Solution.TestHarness(2147483646,1)));
            Assert.That(-2147483648, Is.EqualTo(Solution.TestHarness(-2147483647,-1)));
            Assert.That(-2147483648, Is.EqualTo(Solution.TestHarness(2147483647,1)));
            Assert.That(2147483647, Is.EqualTo(Solution.TestHarness(-2147483648,-1)));
        }
        [Test]
        public void NegativeAdd()
        {
            Assert.That(0, Is.EqualTo(Solution.TestHarness(1,-1)));
            Assert.That(-2, Is.EqualTo(Solution.TestHarness(-1,-1)));
            Assert.That(0, Is.EqualTo(Solution.TestHarness(-0,0)));
            Assert.That(0, Is.EqualTo(Solution.TestHarness(-0,-0)));
        }
    }
}