using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Auth;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Configuration;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules;
using RiseTechnology.Assesment.CoinPrices.Data;
using RiseTechnology.Assesment.CoinPrices.Data.CoinManagement;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

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


#region Coin Management
app.MapControllerRoute(name: "coin-price-showcase", pattern: "showcase", defaults: new { controller = "CoinManagement", action = "Showcase" });
#endregion End Of Coin Management

app.Run();
