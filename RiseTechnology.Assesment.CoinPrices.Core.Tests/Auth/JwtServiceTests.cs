
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Auth;

namespace RiseTechnology.Assesment.CoinPrices.Core.Tests.Rules
{
    [TestClass]
    public class JwtServiceTests
    {
        [TestMethod("Should generate a token")]
        public void ShouldGenerateAToken()
        {
            var jwtTokenService = new JwtServiceDefaultImpl();
            var result = jwtTokenService.GenerateToken("topsecretpasswordforthetest", "issuer", "audience", 10, new Dictionary<string, string>());
            Assert.IsNotNull(result);
        }
    }
}