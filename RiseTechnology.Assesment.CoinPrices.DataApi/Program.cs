using Microsoft.EntityFrameworkCore;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Business.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data;
using RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionStr = "Server=localhost;Database=CoinPrices;User Id=sa;Password=10105400;TrustServerCertificate=True";
builder.Services.AddScoped<DbContext, DatabaseContextDefaultImpl>(serviceProvider => {
    var optionsBuilder = new DbContextOptionsBuilder<DatabaseContextDefaultImpl>();
    optionsBuilder.UseSqlServer(connectionStr);
    optionsBuilder.EnableSensitiveDataLogging();
    return new DatabaseContextDefaultImpl(optionsBuilder.Options);
});

builder.Services.AddScoped<IDataRepository, DataRepositoryDefaultImpl>();
builder.Services.AddScoped<ICoinManagementService, CoinManagementService>();
builder.Services.AddMappingService(options =>
#region Coin Management
    options.Add<CoinPriceHistoryToCoinPriceInfoDtoMapping>()
    .Add<CoinPriceInfoDtoToCoinPriceHistoryMapping>()
#endregion End Of Coin Management
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();


#region Coin Management
app.MapControllerRoute(name: "get-prices", pattern: "get-prices-{priceInfoFilter:regex(today|lastMonth|lastYear)}", defaults: new { controller = "CoinManagement", action = "GetPrices" });
#endregion End Of Coin Management

app.Run();
