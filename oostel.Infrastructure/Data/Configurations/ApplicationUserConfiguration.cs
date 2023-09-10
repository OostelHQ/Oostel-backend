using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.Data.Configurations
{
    public static class ApplicationUserConfiguration
    {
        public static void ConfigureApplicationUser(this ModelBuilder builder)
        {
            // Identity 
            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins").HasKey(k => k.UserId);
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(k => k.Id);
            builder.Entity<ApplicationUser>().HasIndex(b => b.Email).IsUnique();
            builder.Entity<ApplicationUser>().HasIndex(b => b.PhoneNumber).IsUnique();
            builder.Entity<ApplicationRole>().ToTable("AspNetRoles").HasKey(k => k.Id);
            builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles").HasKey(k => k.RoleId);
            builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaim").HasKey(k => k.Id);
            builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens").HasKey(k => k.UserId);
            builder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(i => new { i.UserId, i.RoleId });
            });

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.UserProfile)
                .WithOne(a => a.User)
                .HasForeignKey<UserProfile>(k => k.Id);

            builder.Entity<ApplicationRole>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        }
    }
}
