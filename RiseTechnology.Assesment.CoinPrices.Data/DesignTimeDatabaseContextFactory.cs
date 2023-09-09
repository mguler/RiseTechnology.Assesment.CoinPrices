﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace RiseTechnology.Assesment.CoinPrices.Data
{
    public class DesignTimeDatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContextDefaultImpl>
    {
        public DatabaseContextDefaultImpl CreateDbContext(string[] args)
        {
            try
            {
                var connectionStr = "Server=localhost;Database=CoinPrices;User Id=sa;Password=murat123456!;TrustServerCertificate=True";
                var optionsBuilder = new DbContextOptionsBuilder<DatabaseContextDefaultImpl>();

                optionsBuilder.UseSqlServer(connectionStr);
                optionsBuilder.EnableSensitiveDataLogging();

                return new DatabaseContextDefaultImpl(optionsBuilder.Options);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
