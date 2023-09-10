using Microsoft.EntityFrameworkCore;

namespace RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement
{
    public class UserApplicationAuth : BaseModel
    {
        public int UserId { get; set; }
        public int ApplicationDefinitionId { get; set; }

        public User User { get; set; }
        public ApplicationDefinition ApplicationDefinition { get; set; }

        public static void FluentInitAndSeed(ModelBuilder modelBuilder)
        {
            FluentInit<UserApplicationAuth>(modelBuilder);

            modelBuilder.Entity<UserApplicationAuth>(entity =>
            {
                entity.ToTable("UserApplicationAuth", "User");

                #region Relation List
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.ApplicationDefinition).WithMany().HasForeignKey(e => e.ApplicationDefinitionId);
                #endregion End Of Relation List

                #region Data
                entity.HasData(new UserApplicationAuth
                {
                    Id = 1,
                    UserId = 1,
                    ApplicationDefinitionId = 1,
                    IsDeleted = false,
                });

                entity.HasData(new UserApplicationAuth
                {
                    Id = 2,
                    UserId = 1,
                    ApplicationDefinitionId = 2,
                    IsDeleted = false,
                });

                entity.HasData(new UserApplicationAuth
                {
                    Id = 3,
                    UserId = 2,
                    ApplicationDefinitionId = 1,
                    IsDeleted = false,
                });
                #endregion End Of Data
            });
        }
    }
}
