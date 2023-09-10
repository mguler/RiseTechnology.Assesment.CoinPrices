using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Integrations.BinanceImpl;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping;
using RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data;

IConfiguration configuration = new ConfigurationBuilder()
   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
   .AddEnvironmentVariables()
   .AddCommandLine(args)
   .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT");
        var connectionName = environment == "docker" ? "CoinPricesDocker" : "CoinPrices";
        var connectionString = configuration.GetConnectionString(connectionName);

        services.AddScoped<DbContext, DatabaseContextDefaultImpl>(serviceProvider => {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContextDefaultImpl>();
            optionsBuilder.UseSqlServer(connectionString);
            return new DatabaseContextDefaultImpl(optionsBuilder.Options);
        });

        services.AddTransient<IDataRepository, DataRepositoryDefaultImpl>();
        services.AddHostedService<BinanceIntegrationService>();

        services.AddMappingService(options =>

        #region Coin Management
        options.Add<CoinPriceInfoDtoToCoinPriceHistoryMapping>()
        #endregion End Of Coin Management
);
    })
    .Build();

await host.RunAsync();
