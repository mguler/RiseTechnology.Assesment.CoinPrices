using Microsoft.EntityFrameworkCore;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Data.CoinManagement
{
    public class UserDatabaseContextDefaultImpl : DbContext
    {
        public UserDatabaseContextDefaultImpl(DbContextOptions<UserDatabaseContextDefaultImpl> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region User Management
            User.FluentInitAndSeed(modelBuilder);
            UserApplicationAuth.FluentInitAndSeed(modelBuilder);
            ApplicationDefinition.FluentInitAndSeed(modelBuilder);
            #endregion End Of User Management

        }
    }
}