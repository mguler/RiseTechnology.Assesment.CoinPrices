using Moq;
using RiseTechnology.Assesment.CoinPrices.Business.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.Tests.UserManagement
{
    [TestClass]
    public class GetPriceInfoTests
    {
        [TestMethod("Should return last 24 hour data")]
        public void ShouldReturnLast24HourData()
        {
            var today = DateTime.Now;
            var yesterday = today.AddDays(-2);
            var moreThanMonth = today.AddDays(-60);
            var moreThanYear = today.AddDays(-366);

            var data = new List<CoinPriceHistory>
            {
                new CoinPriceHistory {  Timestamp =  today },
                new CoinPriceHistory { Timestamp =  yesterday},
                new CoinPriceHistory { Timestamp =  moreThanMonth},
                new CoinPriceHistory { Timestamp =  moreThanYear}
            };

            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingProviderSetup = new Mock<IMappingServiceProvider>();

            dataRepositorySetup.Setup(m => m.Get<CoinPriceHistory>()).Returns(() => {
                return data.AsQueryable();
            });

            mappingProviderSetup.Setup(m => m.Map<List<CoinPriceInfoDto>>(It.IsAny<List<CoinPriceHistory>>())).Returns<List<CoinPriceHistory>>((data) =>
            data.Select(item => new CoinPriceInfoDto
            {
                Price = item.Price,
                Symbol = item.Symbol,
                Date = item.Timestamp
            }).ToList());

            var coinManagementService = new CoinManagementService(dataRepositorySetup.Object, mappingProviderSetup.Object);
            var result = coinManagementService.GetPriceInfo(PriceInfoFilter.Today);

            Assert.IsTrue(result.All(item => item.Date >= today.AddDays(-1)));
        }

        [TestMethod("Should return last 30 days data")]
        public void ShouldReturnLast30DaysData()
        {
            var today = DateTime.Now;
            var yesterday = today.AddDays(-2);
            var moreThanMonth = today.AddDays(-60);
            var moreThanYear = today.AddDays(-366);

            var data = new List<CoinPriceHistory>
            {
                new CoinPriceHistory { Timestamp =  yesterday},
                new CoinPriceHistory { Timestamp =  moreThanMonth},
                new CoinPriceHistory { Timestamp =  moreThanYear}
            };

            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingProviderSetup = new Mock<IMappingServiceProvider>();

            dataRepositorySetup.Setup(m => m.Get<CoinPriceHistory>()).Returns(() => {
                return data.AsQueryable();
            });

            mappingProviderSetup.Setup(m => m.Map<List<CoinPriceInfoDto>>(It.IsAny<List<CoinPriceHistory>>())).Returns<List<CoinPriceHistory>>((data) =>
            data.Select(item => new CoinPriceInfoDto
            {
                Price = item.Price,
                Symbol = item.Symbol,
                Date = item.Timestamp
            }).ToList());

            var coinManagementService = new CoinManagementService(dataRepositorySetup.Object, mappingProviderSetup.Object);
            var result = coinManagementService.GetPriceInfo(PriceInfoFilter.Month);

            var v = result.All(item => item.Date >= today.AddDays(-30));
            Assert.IsTrue(result.All(item => item.Date >= today.AddDays(-30)));
        }

        [TestMethod("Should return last 365 days data")]
        public void ShouldReturnLast365DaysData()
        {
            var today = DateTime.Now;
            var moreThanMonth = today.AddDays(-60);
            var moreThanYear = today.AddDays(-366);

            var data = new List<CoinPriceHistory>
            {
                new CoinPriceHistory { Timestamp =  moreThanMonth},
                new CoinPriceHistory { Timestamp =  moreThanYear}
            };

            var dataRepositorySetup = new Mock<IDataRepository>();
            var mappingProviderSetup = new Mock<IMappingServiceProvider>();

            dataRepositorySetup.Setup(m => m.Get<CoinPriceHistory>()).Returns(() => {
                return data.AsQueryable();
            });

            mappingProviderSetup.Setup(m => m.Map<List<CoinPriceInfoDto>>(It.IsAny<List<CoinPriceHistory>>())).Returns<List<CoinPriceHistory>>((data) =>
            data.Select(item => new CoinPriceInfoDto
            {
                Price = item.Price,
                Symbol = item.Symbol,
                Date = item.Timestamp
            }).ToList());

            var coinManagementService = new CoinManagementService(dataRepositorySetup.Object, mappingProviderSetup.Object);
            var result = coinManagementService.GetPriceInfo(PriceInfoFilter.Month);

            Assert.IsTrue(result.All(item => item.Date >= today.AddDays(-365)));
        }
    }
}