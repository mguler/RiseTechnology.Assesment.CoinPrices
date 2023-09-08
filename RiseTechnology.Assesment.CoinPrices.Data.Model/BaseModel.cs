using Microsoft.EntityFrameworkCore;

namespace RiseTechnology.Assesment.CoinPrices.Data.Model
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public static void FluentInit<T>(ModelBuilder modelBuilder) where T : BaseModel
        {
            modelBuilder.Entity<T>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id);
                entity.Property(e => e.IsDeleted).IsRequired();

                //Filter deleted records
                entity.HasQueryFilter(model => !model.IsDeleted);
            });
        }
    }
}