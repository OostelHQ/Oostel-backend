using Microsoft.EntityFrameworkCore;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;

namespace Oostel.Infrastructure.Data.Configurations
{
    public static class UserProfileConfiguration
    {
        public static void ConfigureUserProfile(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Landlord>().HasKey(u => u.Id);

            modelBuilder.Entity<Landlord>()
                .HasMany(h => h.Hostels)
                .WithOne(u => u.Landlord)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

           /* modelBuilder.Entity<Landlord>()
            .HasOne(u => u.ReferralAgentInfo)
            .WithOne(r => r.Landlord).HasForeignKey<ReferralAgentInfo>(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}
