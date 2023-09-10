using Microsoft.EntityFrameworkCore;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Data.CoinManagement
{
    public class CoinPricesDatabaseContextDefaultImpl : DbContext
    {
        public CoinPricesDatabaseContextDefaultImpl(DbContextOptions<CoinPricesDatabaseContextDefaultImpl> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region Crypto Management
            CoinPriceHistory.FluentInitAndSeed(modelBuilder);
            #endregion End Of Crypto Management
        }
    }
}