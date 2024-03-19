using Microsoft.EntityFrameworkCore;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using Oostel.Domain.UserWallet;

namespace Oostel.Infrastructure.Data.Configurations
{
    public static class UserProfileConfiguration
    {
        public static void ConfigureUserProfile(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Landlord>().HasKey(u => u.Id);
            modelBuilder.Entity<Agent>().HasKey(u => u.Id);
            modelBuilder.Entity<Student>().HasKey(u => u.Id);

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


            modelBuilder.Entity<Landlord>()
                .HasOne(x => x.Wallet)
                .WithOne(s => s.Landlord)
                .HasForeignKey<Wallet>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
             .HasOne(x => x.Wallet)
             .WithOne(s => s.Student)
             .HasForeignKey<Wallet>(w => w.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Agent>()
                .HasOne(x => x.Wallet)
                .WithOne(s => s.Agent)
                .HasForeignKey<Wallet>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //  modelBuilder.Entity<StudentLikes>().HasKey(k => new { k.SourceUserId, k.LikedStudentId });

            /* modelBuilder.Entity<StudentLikes>()
                 .HasOne(s => s.LikedStudent)
                 .WithMany()
                 .HasForeignKey(s => s.LikedStudentId)
                 .OnDelete(DeleteBehavior.Restrict);*/

            modelBuilder.Entity<StudentLikes>()
            .HasOne(sl => sl.LikedStudent)
            .WithMany(s => s.LikedUsers)
            .HasForeignKey(sl => sl.LikedStudentId)
            .OnDelete(DeleteBehavior.Restrict);

            /*modelBuilder.Entity<StudentLikes>()
                .HasOne(s => s.LikedStudent)
                .WithMany(x => x.LikedByUsers)
               // .HasForeignKey(s => s.LikedStudentId)
*/
            modelBuilder.Entity<LandlordAgent>()
                .HasOne(u => u.Agent)
                .WithMany(a => a.LandlordAgents)
                .HasForeignKey(u => u.AgentId);

        }
    }
}
