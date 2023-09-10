using Microsoft.EntityFrameworkCore;

namespace RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement
{
    public class ApplicationDefinition:BaseModel
    {
        public string Name { get; set; }
        public static void FluentInitAndSeed(ModelBuilder modelBuilder)
        {
            FluentInit<ApplicationDefinition>(modelBuilder);

            modelBuilder.Entity<ApplicationDefinition>(entity =>
            {
                entity.ToTable("ApplicationDefinition", "User");

                #region Property List
                entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
                #endregion End Of Property List

                #region Data
                entity.HasData(new ApplicationDefinition
                {
                    Id = 1,
                    Name = "Showcase Application",
                    IsDeleted = false,
                });

                entity.HasData(new ApplicationDefinition
                {
                    Id = 2,
                    Name = "Coin Prices Data Api",
                    IsDeleted = false,
                });
                #endregion End Of Data
            });
        }
    }
}
