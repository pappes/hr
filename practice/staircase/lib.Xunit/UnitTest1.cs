using System;
using Xunit;

namespace lib.Xunit
{
    public class UnitTestTheories
    {
        [Theory]
        [InlineData(
@"#"
        , 1)]
        [InlineData(
@" #
##"
        , 2)]
        [InlineData(
@"     #
    ##
   ###
  ####
 #####
######"
        , 6)]
        public void TestPlusMinus(string result, int n)
        {
            Assert.Equal(result, Solution.TestHarness(n));
        }
    }
}
