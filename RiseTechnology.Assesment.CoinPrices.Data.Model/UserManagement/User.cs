using Microsoft.EntityFrameworkCore;
using RiseTechnology.Assesment.CoinPrices.Data.Model;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement.Converters;

namespace RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement
{
    public class User : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<UserApplicationAuth> UserApplicationAuth { get; set; }

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

                #region Data
                entity.HasData(new User
                {
                    Id = 1,
                    Email = "sysadmin@localhost",
                    Firstname = "System",
                    Lastname = "Admin",
                    IsActive = true,
                    Password = "A1973t!",
                    IsDeleted = false,
                });

                entity.HasData(new User
                {
                    Id = 2,
                    Email = "testuser1@localhost",
                    Firstname = "Test",
                    Lastname = "User",
                    IsActive = true,
                    Password = "B1973t!",
                    IsDeleted = false,
                });

                entity.HasData(new User
                {
                    Id = 3,
                    Email = "testuser2@localhost",
                    Firstname = "Test",
                    Lastname = "User",
                    IsActive = true,
                    Password = "C1973t!",
                    IsDeleted = false,
                });
                #endregion End Of Data
            });
        }
    }
}
