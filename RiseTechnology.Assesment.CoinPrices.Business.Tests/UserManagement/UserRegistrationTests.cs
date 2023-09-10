using Microsoft.AspNetCore.Http;
using Moq;
using RiseTechnology.Assesment.CoinPrices.Business.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Auth;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;
using System.Collections.Specialized;

namespace RiseTechnology.Assesment.CoinPrices.Business.Tests.UserManagement
{
    [TestClass]
    public class UserRegistrationTests
    {
        [TestMethod("Should return error when given email address already registered")]
        public void ReturnErrorWhenGivenEmailAddressAlreadyResgitered()
        {
            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingServiceSetup = new Mock<IMappingServiceProvider>();
            var ruleCheckResultSetup = new Mock<IRuleServiceResult>();
            var ruleServiceSetup = new Mock<IRuleServiceProvider>();
            var jwtServiceSetup = new Mock<IJwtService>();


            dataRepositorySetup.Setup(m => m.Get<User>()).Returns(() => {
                return new List<User>()
                {
                    new User { Email = "user@localhost.com" }
                }.AsQueryable();
            });

            mappingServiceSetup.Setup(m => m.Map<User>(It.IsAny<RegisterDto>())).Returns<RegisterDto>((user) => {
                return new User { Email = user.Email };
            });

            ruleCheckResultSetup.SetupGet(m => m.IsSuccessful).Returns(true);
            ruleCheckResultSetup.SetupGet(m => m.Messages).Returns(new Dictionary<string, string>());
            var ruleCheckResult = ruleCheckResultSetup.Object;
            ruleServiceSetup.Setup(m => m.Apply(It.IsAny<string>(), It.IsAny<RegisterDto>())).Returns(() => ruleCheckResultSetup.Object);

            var registerDto = new RegisterDto { Email = "user@localhost.com" };
            var userManagementService = new UserManagementService(null, null, dataRepositorySetup.Object, mappingServiceSetup.Object, ruleServiceSetup.Object, null);
            var result = userManagementService.Register(registerDto);

            Assert.IsFalse(result.IsSuccessful);

        }

        [TestMethod("Should return error when exception occurs")]
        public void ReturnErrorWhenExceptionOccurs()
        {
            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingServiceSetup = new Mock<IMappingServiceProvider>();
            var ruleCheckResultSetup = new Mock<IRuleServiceResult>();
            var ruleServiceSetup = new Mock<IRuleServiceProvider>();
            var jwtServiceSetup = new Mock<IJwtService>();

            dataRepositorySetup.Setup(m => m.Get<User>()).Returns(() => {
                throw new Exception("Database error");
            });
            ruleCheckResultSetup.SetupGet(m => m.IsSuccessful).Returns(true);
            ruleCheckResultSetup.SetupGet(m => m.Messages).Returns(new Dictionary<string, string>());
            var ruleCheckResult = ruleCheckResultSetup.Object;
            ruleServiceSetup.Setup(m => m.Apply(It.IsAny<string>(), It.IsAny<RegisterDto>())).Returns(() => ruleCheckResultSetup.Object);

            var registerDto = new RegisterDto { Email = "user@localhost.com" };
            var userManagementService = new UserManagementService(null, null, dataRepositorySetup.Object, mappingServiceSetup.Object, ruleServiceSetup.Object, null);
            var result = userManagementService.Register(registerDto);

            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod("Should return successful")]
        public void ShouldReturnSuccessful()
        {
            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingServiceSetup = new Mock<IMappingServiceProvider>();
            var ruleCheckResultSetup = new Mock<IRuleServiceResult>();
            var ruleServiceSetup = new Mock<IRuleServiceProvider>();
            var jwtServiceSetup = new Mock<IJwtService>();

            dataRepositorySetup.Setup(m => m.Get<User>()).Returns(() =>  new List<User>().AsQueryable());

            ruleCheckResultSetup.SetupGet(m => m.IsSuccessful).Returns(true);
            ruleCheckResultSetup.SetupGet(m => m.Messages).Returns(new Dictionary<string, string>());
            var ruleCheckResult = ruleCheckResultSetup.Object;
            ruleServiceSetup.Setup(m => m.Apply(It.IsAny<string>(), It.IsAny<RegisterDto>())).Returns(() => ruleCheckResultSetup.Object);

            var registerDto = new RegisterDto { Email = "user@localhost.com" };
            var userManagementService = new UserManagementService(null, null, dataRepositorySetup.Object, mappingServiceSetup.Object, ruleServiceSetup.Object, null);
            var result = userManagementService.Register(registerDto);

            Assert.IsTrue(result.IsSuccessful);
        }
    }
}