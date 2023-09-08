using Microsoft.EntityFrameworkCore;
using RiseTechnology.Assesment.CoinPrices.Data.Model;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement.Converters;

namespace RiseTechnology.Assesment.CryptoTrader.Data.Model.UserManagement
{
    public class User : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public static void FluentInitAndSeed(ModelBuilder modelBuilder)
        {
            FluentInit<User>(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "UserManagement");
                #region Property List
                entity.Property(e => e.Firstname).IsRequired().HasMaxLength(16);
                entity.Property(e => e.Lastname).IsRequired().HasMaxLength(16);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(32);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(128).HasConversion<PasswordConverterSHA256Impl>();
                entity.Property(e => e.IsActive).IsRequired();
                #endregion
            });
        }
    }
}
