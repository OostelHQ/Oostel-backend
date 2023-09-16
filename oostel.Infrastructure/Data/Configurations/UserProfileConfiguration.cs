using Microsoft.EntityFrameworkCore;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.Data.Configurations
{
    public static class UserProfileConfiguration
    {
        public static void ConfigureUserProfile(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().HasKey(u => u.Id);

            modelBuilder.Entity<UserProfile>()
                .HasMany(h => h.Hostels)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
