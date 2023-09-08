using RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping;
using RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement;
using RiseTechnology.Assesment.CryptoTrader.Mapping.MappingConfigurations.CryptoManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMappingService(options =>

#region Crypto Management
    options.Add<CoinPriceHistoryToCoinPriceInfoDtoMapping>()
    .Add<CoinPriceInfoDtoToCoinPriceHistoryMapping>()
#endregion End Of Crypto Management

#region User Management
    .Add<RegisterDtoToUserMapping>()
    .Add<UserToUsertDtoMapping>()
#endregion End Of User Management
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}");

#region User Management
app.MapControllerRoute(name: "user-registration", pattern: "register", defaults: new { controller = "User", action = "Register" });
app.MapControllerRoute(name: "login", pattern: "login", defaults: new { controller = "User", action = "Login" });
app.MapControllerRoute(name: "logout", pattern: "logout", defaults: new { controller = "User", action = "Logout" });
#endregion End Of User Management

#region Coin Management
app.MapControllerRoute(name: "coin-price-showcase", pattern: "showcase", defaults: new { controller = "CoinManagement", action = "Showcase" });
#endregion End Of Coin Management


app.Run();
