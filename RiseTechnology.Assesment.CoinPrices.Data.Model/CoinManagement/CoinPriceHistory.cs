using Microsoft.EntityFrameworkCore;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement.Converters;

namespace RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement
{
    public class CoinPriceHistory : BaseModel
    {
        public string Symbol { get;set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        
        public static void FluentInitAndSeed(ModelBuilder modelBuilder)
        {
            FluentInit<CoinPriceHistory>(modelBuilder);

            modelBuilder.Entity<CoinPriceHistory>(entity =>
            {
                entity.ToTable("CoinPriceHistory", "CoinManagement");

                #region Property List
                entity.Property(e => e.Symbol).IsRequired().HasMaxLength(16);
                entity.Property(e => e.Price).IsRequired().HasPrecision(18, 8);
                entity.Property(e => e.Timestamp).IsRequired().HasConversion<UnixTimestampConverter>();
                #endregion
            });
        }
    }
}
