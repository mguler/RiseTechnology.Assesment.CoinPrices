using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Rules.Configurations.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Rules.Tests.UserManagement
{
    [TestClass()]
    public class UserRegistrationTests
    {
        [TestMethod("Should return email error")]
        public void ShouldReturnEmailError()
        {
            var rule = default(Delegate);
            var mock = new Mock<IRuleServiceProvider>();
            mock.Setup(m => m.Register(It.IsAny<string>(), It.IsAny<Func<RegisterDto, IRuleServiceResult>>())).Callback<string, Delegate>((name, ruleMethod) => {
                rule = ruleMethod;
            });

            var ruleEngine = mock.Object;
            var registerRule = new RegisterRuleConfiguration();
            registerRule.Configure(ruleEngine);

            var registerDto = new RegisterDto();
            var result = rule.DynamicInvoke(registerDto) as IRuleServiceResult;
            Assert.IsTrue(result.Messages.ContainsKey("Email"));
        }

        [TestMethod("Should return firstname error")]
        public void ShouldReturnFirstnameError()
        {
            var rule = default(Delegate);
            var mock = new Mock<IRuleServiceProvider>();
            mock.Setup(m => m.Register(It.IsAny<string>(), It.IsAny<Func<RegisterDto, IRuleServiceResult>>())).Callback<string, Delegate>((name, ruleMethod) => {
                rule = ruleMethod;
            });

            var ruleEngine = mock.Object;
            var registerRule = new RegisterRuleConfiguration();
            registerRule.Configure(ruleEngine);

            var registerDto = new RegisterDto();
            var result = rule.DynamicInvoke(registerDto) as IRuleServiceResult;
            Assert.IsTrue(result.Messages.ContainsKey("Firstname"));
        }
        [TestMethod("Should return lastname error")]
        public void ShouldReturnLastnameError()
        {
            var rule = default(Delegate);
            var mock = new Mock<IRuleServiceProvider>();
            mock.Setup(m => m.Register(It.IsAny<string>(), It.IsAny<Func<RegisterDto, IRuleServiceResult>>())).Callback<string, Delegate>((name, ruleMethod) => {
                rule = ruleMethod;
            });

            var ruleEngine = mock.Object;
            var registerRule = new RegisterRuleConfiguration();
            registerRule.Configure(ruleEngine);

            var registerDto = new RegisterDto();
            var result = rule.DynamicInvoke(registerDto) as IRuleServiceResult;
            Assert.IsTrue(result.Messages.ContainsKey("Lastname"));
        }

        [TestMethod("Should return password error")]
        public void ShouldReturnPasswordError()
        {
            var rule = default(Delegate);
            var mock = new Mock<IRuleServiceProvider>();
            mock.Setup(m => m.Register(It.IsAny<string>(), It.IsAny<Func<RegisterDto, IRuleServiceResult>>())).Callback<string, Delegate>((name, ruleMethod) => {
                rule = ruleMethod;
            });

            var ruleEngine = mock.Object;
            var registerRule = new RegisterRuleConfiguration();
            registerRule.Configure(ruleEngine);

            var registerDto = new RegisterDto();
            var result = rule.DynamicInvoke(registerDto) as IRuleServiceResult;
            Assert.IsTrue(result.Messages.ContainsKey("Password"));
        }

        [TestMethod("Should return re-password error")]
        public void ShouldReturnRePasswordError()
        {
            var rule = default(Delegate);
            var mock = new Mock<IRuleServiceProvider>();
            mock.Setup(m => m.Register(It.IsAny<string>(), It.IsAny<Func<RegisterDto, IRuleServiceResult>>())).Callback<string, Delegate>((name, ruleMethod) => {
                rule = ruleMethod;
            });

            var ruleEngine = mock.Object;
            var registerRule = new RegisterRuleConfiguration();
            registerRule.Configure(ruleEngine);

            var registerDto = new RegisterDto { Password= "Abc123#$" };
            var result = rule.DynamicInvoke(registerDto) as IRuleServiceResult;
            Assert.IsTrue(result.Messages.ContainsKey("RePassword"));
        }

        [TestMethod("Should return successful")]
        public void ShouldReturnSuccessful()
        {
            var rule = default(Delegate);
            var mock = new Mock<IRuleServiceProvider>();
            mock.Setup(m => m.Register(It.IsAny<string>(), It.IsAny<Func<RegisterDto, IRuleServiceResult>>())).Callback<string, Delegate>((name, ruleMethod) => {
                rule = ruleMethod;
            });

            var ruleEngine = mock.Object;
            var registerRule = new RegisterRuleConfiguration();
            registerRule.Configure(ruleEngine);

            var registerDto = new RegisterDto
            {
                Email = "user@localhost.com",
                Firstname = "Test",
                Lastname = "User",
                Password = "Abc123#$",
                RePassword = "Abc123#$"
            };

            var result = rule.DynamicInvoke(registerDto) as IRuleServiceResult;
            Assert.IsTrue(result.IsSuccessful);
        }

    }
}