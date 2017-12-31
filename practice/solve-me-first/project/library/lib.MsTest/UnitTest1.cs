using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lib.MsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SimpleAdd()
        {
            Assert.AreEqual(2, Solution.TestHarness(1,1));
            Assert.AreEqual(0, Solution.TestHarness(0,0));
            Assert.AreEqual(20, Solution.TestHarness(10,10));
            Assert.AreEqual(3, Solution.TestHarness(1,2));
            Assert.AreEqual(3, Solution.TestHarness(2,1));
        }
        [TestMethod]
        public void LargeAdd()
        {
            Assert.AreEqual(200000000, Solution.TestHarness(100000000,100000000));
            Assert.AreEqual(-1, Solution.TestHarness(-2147483648,2147483647));
            Assert.AreEqual(0, Solution.TestHarness(-2147483647,2147483647));
            Assert.AreEqual(2147483647, Solution.TestHarness(2147483646,1));
            Assert.AreEqual(-2147483648, Solution.TestHarness(-2147483647,-1));
            Assert.AreEqual(-2147483648, Solution.TestHarness(2147483647,1));
            Assert.AreEqual(2147483647, Solution.TestHarness(-2147483648,-1));
        }
        [TestMethod]
        public void NegativeAdd()
        {
            Assert.AreEqual(0, Solution.TestHarness(1,-1));
            Assert.AreEqual(-2, Solution.TestHarness(-1,-1));
            Assert.AreEqual(0, Solution.TestHarness(-0,0));
            Assert.AreEqual(0, Solution.TestHarness(-0,-0));
        }
    }
}
