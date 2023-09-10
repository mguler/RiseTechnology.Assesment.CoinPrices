using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace RiseTechnology.Assesment.CoinPrices.Data.CoinManagement
{
    public class UserDatabaseDesignTimeDatabaseContextFactory : IDesignTimeDbContextFactory<UserDatabaseContextDefaultImpl>
    {
        public UserDatabaseContextDefaultImpl CreateDbContext(string[] args)
        {
            try
            {
                var connectionStr = "Server=localhost;Database=RiseTechnologyUserDatabase;User Id=sa;Password=10105400;TrustServerCertificate=True";
                var optionsBuilder = new DbContextOptionsBuilder<UserDatabaseContextDefaultImpl>();

                optionsBuilder.UseSqlServer(connectionStr);
                optionsBuilder.EnableSensitiveDataLogging();

                return new UserDatabaseContextDefaultImpl(optionsBuilder.Options);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
