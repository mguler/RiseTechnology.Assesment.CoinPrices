using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using RiseTechnology.Assesment.CoinPrices.Business.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Auth;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Configuration;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.Tests.UserManagement
{
    [TestClass]
    public class UserLoginTests
    {
        [TestMethod("Should return false when user does not exists with the given username")]
        public void ReturnFalseWhenUserDoesNotExistsWithTheGivenUsername()
        {
            var dataRepositorySetup = new Mock<IDataRepository>();
            var ruleCheckResultSetup = new Mock<IRuleServiceResult>();
            var ruleServiceSetup = new Mock<IRuleServiceProvider>();
            var jwtServiceSetup = new Mock<IJwtService>();

            dataRepositorySetup.Setup(m => m.Get<User>()).Returns(() => {
                return new List<User>()
                {
                    new User { Email = "user@localhost.com" , Password = "secretpassword" }
                }.AsQueryable();
            });

            ruleCheckResultSetup.SetupGet(m => m.IsSuccessful).Returns(true);
            ruleCheckResultSetup.SetupGet(m => m.Messages).Returns(new Dictionary<string, string>());
            var ruleCheckResult = ruleCheckResultSetup.Object;
            ruleServiceSetup.Setup(m => m.Apply(It.IsAny<string>(), It.IsAny<LoginDto>())).Returns(() => ruleCheckResultSetup.Object);

            var loginDto = new LoginDto { Username = "doesnotexists@localhost.com", Password = "secretpassword" };
            var userManagementService = new UserManagementService(null, null, dataRepositorySetup.Object, null, ruleServiceSetup.Object, null);
            var result = userManagementService.Login(loginDto);

            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod("Should return false when user does not exists with the given password")]
        public void ReturnFalseWhenUserDoesNotExistsWithTheGivenPassword()
        {
            var dataRepositorySetup = new Mock<IDataRepository>();
            var ruleCheckResultSetup = new Mock<IRuleServiceResult>();
            var ruleServiceSetup = new Mock<IRuleServiceProvider>();
            var jwtServiceSetup = new Mock<IJwtService>();

            dataRepositorySetup.Setup(m => m.Get<User>()).Returns(() => {
                return new List<User>()
                {
                    new User { Email = "user@localhost.com" , Password = "secretpassword" }
                }.AsQueryable();
            });

            ruleCheckResultSetup.SetupGet(m => m.IsSuccessful).Returns(true);
            ruleCheckResultSetup.SetupGet(m => m.Messages).Returns(new Dictionary<string, string>());
            ruleServiceSetup.Setup(m => m.Apply(It.IsAny<string>(), It.IsAny<LoginDto>())).Returns(() => ruleCheckResultSetup.Object);

            var loginDto = new LoginDto { Username = "user@localhost.com", Password = "thepasswordthatdoesntmatch" };
            var userManagementService = new UserManagementService(null, null, dataRepositorySetup.Object, null, ruleServiceSetup.Object, null);
            var result = userManagementService.Login(loginDto);

            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod("Should return false if the user is not activated")]
        public void ReturnFalseIfTheUserIsNotActivated()
        {
            var dataRepositorySetup = new Mock<IDataRepository>();
            var ruleCheckResultSetup = new Mock<IRuleServiceResult>();
            var ruleServiceSetup = new Mock<IRuleServiceProvider>();
            var jwtServiceSetup = new Mock<IJwtService>();

            dataRepositorySetup.Setup(m => m.Get<User>()).Returns(() => {
                return new List<User>()
                {
                    new User { Email = "user@localhost.com" , Password = "secretpassword" , IsActive = false }
                }.AsQueryable();
            });

            ruleCheckResultSetup.SetupGet(m => m.IsSuccessful).Returns(true);
            ruleCheckResultSetup.SetupGet(m => m.Messages).Returns(new Dictionary<string, string>());
            ruleServiceSetup.Setup(m => m.Apply(It.IsAny<string>(), It.IsAny<LoginDto>())).Returns(() => ruleCheckResultSetup.Object);

            var loginDto = new LoginDto { Username = "user@localhost.com", Password = "secretpassword" };
            var userManagementService = new UserManagementService(null, null, dataRepositorySetup.Object, null, ruleServiceSetup.Object, null);
            var result = userManagementService.Login(loginDto);

            Assert.IsFalse(result.IsSuccessful);
        }
        [TestMethod("Should return successful")]
        public void ReturnSuccesfull()
        {
            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingServiceSetup = new Mock<IMappingServiceProvider>();
            var ruleCheckResultSetup = new Mock<IRuleServiceResult>();
            var ruleServiceSetup = new Mock<IRuleServiceProvider>();
            var jwtServiceSetup = new Mock<IJwtService>();
            var tokenOptionsSetup = new Mock<IOptions<TokenOptions>>();
            var httpContextAccessorSetup = new Mock<IHttpContextAccessor>();
            var httpContextSetup =  new Mock<HttpContext>();
            var httpResponseSetup = new Mock<HttpResponse>();
            var httpResponseCookies = new Mock<IResponseCookies>();

            dataRepositorySetup.Setup(m => m.Get<User>()).Returns(() => {
                return new List<User>()
                {
                    new User { Email = "user@localhost.com" , Password = "secretpassword" , IsActive = true }
                }.AsQueryable();
            });

            ruleCheckResultSetup.SetupGet(m => m.IsSuccessful).Returns(true);
            ruleCheckResultSetup.SetupGet(m => m.Messages).Returns(new Dictionary<string, string>());
            ruleServiceSetup.Setup(m => m.Apply(It.IsAny<string>(), It.IsAny<LoginDto>())).Returns(() => ruleCheckResultSetup.Object);
            jwtServiceSetup.Setup(m => m.GenerateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<Dictionary<string, string>>()))
            .Returns(() => "");
            tokenOptionsSetup.Setup(m => m.Value).Returns(() => new TokenOptions());

            httpResponseSetup.SetupGet(m => m.Cookies).Returns(() => httpResponseCookies.Object);
            httpContextSetup.SetupGet(m => m.Response).Returns(() => httpResponseSetup.Object);
            httpContextAccessorSetup.SetupGet(m => m.HttpContext).Returns(() => httpContextSetup.Object);


            var loginDto = new LoginDto { Username = "user@localhost.com", Password = "secretpassword" };
            var userManagementService = new UserManagementService(tokenOptionsSetup.Object, httpContextAccessorSetup.Object, dataRepositorySetup.Object, mappingServiceSetup.Object, ruleServiceSetup.Object,jwtServiceSetup.Object);
            var result = userManagementService.Login(loginDto);

            Assert.IsTrue(result.IsSuccessful);
        }
    }
}