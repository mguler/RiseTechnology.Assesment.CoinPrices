using RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping;

namespace RiseTechnology.Assesment.CoinPrices.Core.Tests.Rules
{
    [TestClass]
    public class MappingServiceProviderTests
    {
        [TestMethod("Should throw an exception when registering a mapping with the types that are already registered")]
        public void ShouldThrowExceptionWhenRegisteringTheTypesThatAreAlreadyRegistered()
        {
            var mappingServiceProvider = new MappingServiceProvider();
            mappingServiceProvider.Register<object, object>((source) => null);

            Assert.ThrowsException<Exception>(() => mappingServiceProvider.Register<object, object>((source) => null));
        }
        [TestMethod("Should throw an exception when the expected mapping is not registered")]
        public void ShouldThrowExceptionWhenTheExpectedMappingIsNotRegistered()
        {
            var mappingServiceProvider = new MappingServiceProvider();
            Assert.ThrowsException<Exception>(() => mappingServiceProvider.Map<object>(new object()));
        }
        [TestMethod("Should return successful result")]
        public void ShouldReturnSuccessfulResult()
        {
            var mappingServiceProvider = new MappingServiceProvider();
            mappingServiceProvider.Register<object, object>((source) => new object());
            var result = mappingServiceProvider.Map<object>(new object());
            Assert.IsNotNull(result);
        }
    }
}