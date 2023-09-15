using RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules;

namespace RiseTechnology.Assesment.CoinPrices.Core.Tests.Rules
{
    [TestClass]
    public class RuleServiceProviderTests
    {
        [TestMethod("Should throw an exception when registering a rule with duplicate key")]
        public void ShouldThrowExceptionWhenRegisteringARuleWithDuplicateKey()
        {
            var ruleServiceProvider = new RuleServiceProvider();
            ruleServiceProvider.Register<object>("testrule", arg => null);

            Assert.ThrowsException<Exception>(() => ruleServiceProvider.Register<object>("testrule", arg => null));
        }
        [TestMethod("Should throw an exception when the expected rule is not registered")]
        public void ShouldThrowExceptionWhenTheExpectedRuleIsNotRegistered()
        {
            var ruleServiceProvider = new RuleServiceProvider();
            Assert.ThrowsException<Exception>(() => ruleServiceProvider.Apply<object>("testrule", null));
        }
        [TestMethod("Should return successful result")]
        public void ShouldReturnSuccessfulResult()
        {
            var ruleServiceProvider = new RuleServiceProvider();
            ruleServiceProvider.Register<object>("testrule", arg => new RuleServiceResult());

            var result = ruleServiceProvider.Apply<object>("testrule", null);
            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod("Should return false when result contains message")]
        public void ShouldReturnFalseWhenResultContainsMessage()
        {
            var result = new RuleServiceResult();
            result.Messages.Add("test", "testmessage");
            Assert.IsFalse(result.IsSuccessful);
        }
    }
}