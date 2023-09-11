using Microsoft.EntityFrameworkCore;

namespace RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement
{
    public class CoinPriceHistory : BaseModel
    {
        public string Symbol { get;set; }
        public decimal Price { get; set; }
        public long Timestamp { get; set; }
        
        public static void FluentInitAndSeed(ModelBuilder modelBuilder)
        {
            FluentInit<CoinPriceHistory>(modelBuilder);

            modelBuilder.Entity<CoinPriceHistory>(entity =>
            {
                entity.ToTable("CoinPriceHistory", "CoinManagement");

                #region Property List
                entity.Property(e => e.Symbol).IsRequired().HasMaxLength(16);
                entity.Property(e => e.Price).IsRequired().HasPrecision(18, 8);
                entity.Property(e => e.Timestamp).IsRequired();
                #endregion

            });
        }
    }
}
