using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Business.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Configuration;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data;
using RiseTechnology.Assesment.CoinPrices.Data.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var corsPolicyName = "default cors policy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName,
                          policy =>
                          {
                                policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                          });
});
// Add services to the container.
builder.Services.AddControllers();

var tokenOptions = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<TokenOptions>(tokenOptions);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT");
var connectionName = environment == "docker" ? "CoinPricesDocker" : "CoinPrices";
var connectionString = builder.Configuration.GetConnectionString(connectionName);

builder.Services.AddScoped<DbContext, CoinPricesDatabaseContextDefaultImpl>(serviceProvider => {
    var optionsBuilder = new DbContextOptionsBuilder<CoinPricesDatabaseContextDefaultImpl>();
    optionsBuilder.UseSqlServer(connectionString);
    return new CoinPricesDatabaseContextDefaultImpl(optionsBuilder.Options);
});

builder.Services.AddScoped<IDataRepository, DataRepositoryDefaultImpl>();
builder.Services.AddScoped<ICoinManagementService, CoinManagementService>();
builder.Services.AddMappingService(options =>

#region Coin Management
    options.Add<CoinPriceHistoryToCoinPriceInfoDtoMapping>()
    .Add<CoinPriceInfoDtoToCoinPriceHistoryMapping>()
#endregion End Of Coin Management

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
                        OnTokenValidated = async context =>
                        {

                        },
                        OnAuthenticationFailed = async context =>
                        {

                        },
                        OnMessageReceived = async context =>
                        {

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

app.UseRouting();
app.UseCors(corsPolicyName);
app.UseAuthentication();
app.UseAuthorization();


#region Coin Management
app.MapControllerRoute(name: "get-prices", pattern: "get-prices-{priceInfoFilter:regex(today|lastMonth|lastYear)}", defaults: new { controller = "CoinManagement", action = "GetPrices" });
#endregion End Of Coin Management

app.Run();
