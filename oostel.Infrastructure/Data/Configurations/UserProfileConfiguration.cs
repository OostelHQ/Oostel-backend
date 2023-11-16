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
            modelBuilder.Entity<Agent>().HasKey(u => u.Id);

            modelBuilder.Entity<Landlord>()
                .HasMany(h => h.Hostels)
                .WithOne(u => u.Landlord)
                .HasForeignKey(u => u.LandlordId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LandlordAgent>(x => x.HasKey(aa => new { aa.LandlordId, aa.AgentId }));

            modelBuilder.Entity<LandlordAgent>()
                .HasOne(u => u.Landlord)
                .WithMany(a => a.LandlordAgents)
                .HasForeignKey(u => u.LandlordId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LandlordAgent>()
                .HasOne(u => u.Agent)
                .WithMany(a => a.LandlordAgents)
                .HasForeignKey(u => u.AgentId);

        }
    }
}
