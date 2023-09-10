using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Business.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Auth;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Auth;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Configuration;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules;
using RiseTechnology.Assesment.CoinPrices.Data;
using RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Rules.Configurations.UserManagement;
using RiseTechnology.Assesment.CryptoTrader.Mapping.MappingConfigurations.CryptoManagement;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var tokenOptions = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<TokenOptions>(tokenOptions);

var connectionString = builder.Configuration.GetConnectionString("CoinPrices");
builder.Services.AddScoped<DbContext, DatabaseContextDefaultImpl>(serviceProvider => {
    var optionsBuilder = new DbContextOptionsBuilder<DatabaseContextDefaultImpl>();
    optionsBuilder.UseSqlServer(connectionString);
    return new DatabaseContextDefaultImpl(optionsBuilder.Options);
});

builder.Services.AddScoped<IDataRepository, DataRepositoryDefaultImpl>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IJwtService, JwtServiceDefaultImpl>();
builder.Services.AddMappingService(options =>

#region Coin Management
    options.Add<CoinPriceHistoryToCoinPriceInfoDtoMapping>()
    .Add<CoinPriceInfoDtoToCoinPriceHistoryMapping>()
#endregion End Of Coin Management

#region User Management
    .Add<RegisterDtoToUserMapping>()
    .Add<UserToUsertDtoMapping>()
    .Add<UserToDictionaryMapping>()
#endregion End Of User Management
);

builder.Services.AddRuleService(options =>
#region User Management    
    options.Add<LoginRuleConfiguration>()
    .Add<RegisterRuleConfiguration>()
#endregion End Of User Management

);

var jwtOptions = tokenOptions.Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = async context => {
                            context.Token = context.Request.Cookies["Jwt"];
                        }
                    };

                    options.SaveToken = true;
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        context.HttpContext.Response.Redirect("/login");
    }
});
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CoinManagement}/{action=Showcase}");

#region User Management
app.MapControllerRoute(name: "user-registration", pattern: "register", defaults: new { controller = "User", action = "Register" });
app.MapControllerRoute(name: "login", pattern: "login", defaults: new { controller = "User", action = "Login" });
app.MapControllerRoute(name: "logout", pattern: "logout", defaults: new { controller = "User", action = "Logout" });
#endregion End Of User Management

#region Coin Management
app.MapControllerRoute(name: "coin-price-showcase", pattern: "showcase", defaults: new { controller = "CoinManagement", action = "Showcase" });
#endregion End Of Coin Management

app.Run();
