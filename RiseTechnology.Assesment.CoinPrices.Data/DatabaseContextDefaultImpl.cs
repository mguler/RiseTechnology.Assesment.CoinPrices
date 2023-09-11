using Microsoft.EntityFrameworkCore;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Data
{
    public class DatabaseContextDefaultImpl : DbContext
    {
        public DatabaseContextDefaultImpl(DbContextOptions<DatabaseContextDefaultImpl> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region Crypto Management
            CoinPriceHistory.FluentInitAndSeed(modelBuilder);
            #endregion End Of Crypto Management

            #region User Management
            User.FluentInitAndSeed(modelBuilder);
            #endregion End Of User Management

        }
    }
}