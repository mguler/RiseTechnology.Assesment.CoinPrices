using Moq;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Business.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Business.CoinManagement.CoinManagementServiceParameticImpl;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.Tests.UserManagement
{
    [TestClass]
    public class GetPriceInfoTests
    {
        [TestMethod("Should invoke the inner service")]
        public void ShouldInvokeTheInnerService()
        {
            var invoked = false;
            var innerServiceSetup = new Mock<ICoinManagementService>();
            var dictionary = new Dictionary<string, ICoinManagementService>(); 

            innerServiceSetup.Setup(m => m.GetPriceInfo(It.IsAny<PriceInfoFilter>())).Callback<PriceInfoFilter>(filter =>
            {
                invoked = true;
            });

            dictionary.Add("Month", innerServiceSetup.Object);
            var service = new CoinManagementService(dictionary);

            service.GetPriceInfo(PriceInfoFilter.Month);
            Assert.IsTrue(invoked);
        }

        [TestMethod("Should return last 24 hour data")]
        public void ShouldReturnLast24HourData()
        {
            var today = DateTimeOffset.Now.ToUnixTimeSeconds();
            var yesterday = DateTimeOffset.Now.AddDays(-2).ToUnixTimeSeconds();
            var moreThanMonth = DateTimeOffset.Now.AddDays(-60).ToUnixTimeSeconds();
            var moreThanYear = DateTimeOffset.Now.AddDays(-366).ToUnixTimeSeconds();

            var data = new List<CoinPriceHistory>
            {
                new CoinPriceHistory { Timestamp =  today },
                new CoinPriceHistory { Timestamp =  yesterday},
                new CoinPriceHistory { Timestamp =  moreThanMonth},
                new CoinPriceHistory { Timestamp =  moreThanYear}
            };

            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingProviderSetup = new Mock<IMappingServiceProvider>();

            dataRepositorySetup.Setup(m => m.Get<CoinPriceHistory>()).Returns(() =>
            {
                return data.AsQueryable();
            });

            mappingProviderSetup.Setup(m => m.Map<List<CoinPriceInfoDto>>(It.IsAny<List<CoinPriceHistory>>())).Returns<List<CoinPriceHistory>>((data) =>
            data.Select(item => new CoinPriceInfoDto
            {
                Price = item.Price,
                Symbol = item.Symbol,
                Timestamp = item.Timestamp
            }).ToList());

            var coinManagementService = new CoinManagementServiceDailyImpl(dataRepositorySetup.Object, mappingProviderSetup.Object);
            var result = coinManagementService.GetPriceInfo(PriceInfoFilter.Today);

            Assert.IsTrue(result.Prices.All(item => item.Timestamp >= DateTimeOffset.Now.AddDays(-1).ToUnixTimeSeconds()));
        }

        [TestMethod("Should return last 30 days data")]
        public void ShouldReturnLast30DaysData()
        {
            var yesterday = DateTimeOffset.Now.AddDays(-2).ToUnixTimeSeconds();
            var moreThanMonth = DateTimeOffset.Now.AddDays(-60).ToUnixTimeSeconds();
            var moreThanYear = DateTimeOffset.Now.AddDays(-366).ToUnixTimeSeconds();

            var data = new List<CoinPriceHistory>
            {
                new CoinPriceHistory { Timestamp =  yesterday},
                new CoinPriceHistory { Timestamp =  moreThanMonth},
                new CoinPriceHistory { Timestamp =  moreThanYear}
            };

            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingProviderSetup = new Mock<IMappingServiceProvider>();

            dataRepositorySetup.Setup(m => m.Get<CoinPriceHistory>()).Returns(() =>
            {
                return data.AsQueryable();
            });

            mappingProviderSetup.Setup(m => m.Map<List<CoinPriceInfoDto>>(It.IsAny<List<CoinPriceHistory>>())).Returns<List<CoinPriceHistory>>((data) =>
            data.Select(item => new CoinPriceInfoDto
            {
                Price = item.Price,
                Symbol = item.Symbol,
                Timestamp = item.Timestamp
            }).ToList());

            var coinManagementService = new CoinManagementServiceMonthlyImpl(dataRepositorySetup.Object, mappingProviderSetup.Object);
            var result = coinManagementService.GetPriceInfo(PriceInfoFilter.Month);

            Assert.IsTrue(result.Prices.All(item => item.Timestamp >= DateTimeOffset.Now.AddDays(-30).ToUnixTimeSeconds()));
        }

        [TestMethod("Should return last 365 days data")]
        public void ShouldReturnLast365DaysData()
        {
            var moreThanMonth = DateTimeOffset.Now.AddDays(-60).ToUnixTimeSeconds();
            var moreThanYear = DateTimeOffset.Now.AddDays(-366).ToUnixTimeSeconds();

            var data = new List<CoinPriceHistory>
            {
                new CoinPriceHistory { Timestamp =  moreThanMonth},
                new CoinPriceHistory { Timestamp =  moreThanYear}
            };

            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingProviderSetup = new Mock<IMappingServiceProvider>();

            dataRepositorySetup.Setup(m => m.Get<CoinPriceHistory>()).Returns(() =>
            {
                return data.AsQueryable();
            });

            mappingProviderSetup.Setup(m => m.Map<List<CoinPriceInfoDto>>(It.IsAny<List<CoinPriceHistory>>())).Returns<List<CoinPriceHistory>>((data) =>
            data.Select(item => new CoinPriceInfoDto
            {
                Price = item.Price,
                Symbol = item.Symbol,
                Timestamp = item.Timestamp
            }).ToList());

            var coinManagementService = new CoinManagementServiceAnnualImpl(dataRepositorySetup.Object, mappingProviderSetup.Object);
            var result = coinManagementService.GetPriceInfo(PriceInfoFilter.Month);

            Assert.IsTrue(result.Prices.All(item => item.Timestamp >= DateTimeOffset.Now.AddDays(-365).ToUnixTimeSeconds()));
        }
    }
}