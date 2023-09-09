﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Integrations.BinanceImpl;
using RiseTechnology.Assesment.CoinPrices.Integrations.HostApp;
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
        var connectionString = configuration.GetConnectionString("CoinPrices");

        services.AddScoped<DbContext, DatabaseContextDefaultImpl>(serviceProvider => {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContextDefaultImpl>();
            optionsBuilder.UseSqlServer(connectionString);
            return new DatabaseContextDefaultImpl(optionsBuilder.Options);
        });

        services.AddTransient<IDataRepository, DataRepositoryDefaultImpl>();
        services.AddTransient<QuartzNetDefaultDataIntegrationJobImpl<BinanceDataIntegrationProvider>>();

        //Schedule task for every 1 minute
        services.AddQuartz( quartz => {
            quartz.ScheduleJob<QuartzNetDefaultDataIntegrationJobImpl<BinanceDataIntegrationProvider>>(trigger => trigger
                .StartNow().WithSimpleSchedule(s=>s.WithIntervalInSeconds(3)));
                //.WithSimpleSchedule(;
                //.WithCronSchedule("0 0/1 * 1/1 * ? *"));
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.AddMappingService(options =>

        #region Coin Management
        options.Add<CoinPriceInfoDtoToCoinPriceHistoryMapping>()
        #endregion End Of Coin Management
);
    })
    .Build();

await host.RunAsync();
