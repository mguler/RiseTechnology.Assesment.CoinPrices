using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace RiseTechnology.Assesment.CoinPrices.Data.CoinManagement
{
    public class CoinPricesDesignTimeDatabaseContextFactory : IDesignTimeDbContextFactory<CoinPricesDatabaseContextDefaultImpl>
    {
        public CoinPricesDatabaseContextDefaultImpl CreateDbContext(string[] args)
        {
            try
            {
                var connectionStr = "Server=localhost;Database=RiseTechnologyCoinPrices;User Id=sa;Password=10105400;TrustServerCertificate=True";
                var optionsBuilder = new DbContextOptionsBuilder<CoinPricesDatabaseContextDefaultImpl>();

                optionsBuilder.UseSqlServer(connectionStr);
                optionsBuilder.EnableSensitiveDataLogging();

                return new CoinPricesDatabaseContextDefaultImpl(optionsBuilder.Options);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
