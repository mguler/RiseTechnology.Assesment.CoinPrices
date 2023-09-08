var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
