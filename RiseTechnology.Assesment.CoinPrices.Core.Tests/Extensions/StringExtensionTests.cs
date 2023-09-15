
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Extensions;

namespace RiseTechnology.Assesment.CoinPrices.Core.Tests.Rules
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod("Should return a string that matches the pattern")]
        public void ShouldReturnAString()
        {
            var expected = "expexted result";
            var result = StringExtensions.Match("-expexted result- ", "[a-z ]+");
            Assert.AreEqual(expected, result);
        }
        [TestMethod("Should return a string array that contains matched results to the pattern")]
        public void ShouldReturnAStringArray()
        {
            var expected = "expexted result";
            var result = StringExtensions.Matches("-expexted result- ", "[a-z ]+");
            Assert.IsTrue(result.Contains(expected));
        }
        [TestMethod("Should return true")]
        public void ShouldReturnTrue()
        {
            var expected = "expexted result";
            var result = StringExtensions.IsMatch("-expexted result- ", "[a-z ]+");
            Assert.IsTrue(result);
        }
    }
}