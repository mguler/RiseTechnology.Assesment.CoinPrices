using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Rules.Configurations.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Rules.Tests.UserManagement
{
    [TestClass()]
    public class UserLoginTests
    {
        [TestMethod("Should return error/false when username doesn't match the email pattern")]
        public void ShouldReturnErrorWhenUsernameDoesntMatchTheEmailPattern()
        {
            var rule = default(Delegate);
            var mock = new Mock<IRuleServiceProvider>();
            mock.Setup(m => m.Register(It.IsAny<string>(), It.IsAny<Func<LoginDto, IRuleServiceResult>>())).Callback<string, Delegate>((name, ruleMethod) => {
                rule = ruleMethod;
            });

            var ruleEngine = mock.Object;
            var loginRule = new LoginRuleConfiguration();
            loginRule.Configure(ruleEngine);

            var loginDto = new LoginDto
            {
                Password = "Abc123#$",
            };
            var result = rule.DynamicInvoke(loginDto) as IRuleServiceResult;
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod("Should return error/false when password doesn't match the policy")]
        public void ShouldReturnErrorWhenPasswordDoesntMatchThePolicy()
        {
            var rule = default(Delegate);
            var mock = new Mock<IRuleServiceProvider>();
            mock.Setup(m => m.Register(It.IsAny<string>(), It.IsAny<Func<LoginDto, IRuleServiceResult>>())).Callback<string, Delegate>((name, ruleMethod) => {
                rule = ruleMethod;
            });

            var ruleEngine = mock.Object;
            var loginRule = new LoginRuleConfiguration();
            loginRule.Configure(ruleEngine);

            var loginDto = new LoginDto
            {
                Password = "Abc123#$",
            };
            var result = rule.DynamicInvoke(loginDto) as IRuleServiceResult;
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod("Should return successful")]
        public void ShouldReturnSuccessful()
        {
            var rule = default(Delegate);
            var mock = new Mock<IRuleServiceProvider>();
            mock.Setup(m => m.Register(It.IsAny<string>(), It.IsAny<Func<LoginDto, IRuleServiceResult>>())).Callback<string, Delegate>((name, ruleMethod) => {
                rule = ruleMethod;
            });

            var ruleEngine = mock.Object;
            var loginRule = new LoginRuleConfiguration();
            loginRule.Configure(ruleEngine);

            var loginDto = new LoginDto
            {
                Username = "user@localhost.com",
                Password = "Abc123#$",
            };

            var result = rule.DynamicInvoke(loginDto) as IRuleServiceResult;
            Assert.IsTrue(result.IsSuccessful);
        }

    }
}