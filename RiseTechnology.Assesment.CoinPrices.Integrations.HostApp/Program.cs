using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping;
using RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data;
using RiseTechnology.Assesment.CoinPrices.Integrations.CoinDeskImpl;

IConfiguration configuration = new ConfigurationBuilder()
   .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
   .AddEnvironmentVariables()
   .AddCommandLine(args)
   .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        var connectionString = configuration.GetConnectionString("CoinPrices");
        services.AddScoped<DbContext, DatabaseContextDefaultImpl>(serviceProvider => {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContextDefaultImpl>();
            optionsBuilder.UseSqlServer(connectionString);
            return new DatabaseContextDefaultImpl(optionsBuilder.Options);
        });

        services.AddTransient<IDataRepository, DataRepositoryDefaultImpl>();
        services.AddHostedService<CoinDeskIntegrationService>();

        services.AddMappingService(options =>

        #region Coin Management
        options.Add<CoinPriceInfoDtoToCoinPriceHistoryMapping>()
        #endregion End Of Coin Management
);
    })
    .Build();

var dbContext = host.Services.GetService<DbContext>();
dbContext.Database.EnsureCreated();
await host.RunAsync();
