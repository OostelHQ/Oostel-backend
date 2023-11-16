using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserMessage;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using Oostel.Domain.UserWallet;
using System.Reflection.Emit;

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
                .HasOne(u => u.Landlord)
                .WithOne(a => a.User)
                .HasForeignKey<Landlord>(k => k.Id);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Agent)
                .WithOne(a => a.User)
                .HasForeignKey<Agent>(p => p.Id);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Student)
                .WithOne(a => a.User)
                .HasForeignKey<Student>(k => k.Id);

            builder.Entity<ApplicationRole>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Entity<ApplicationUser>()
                .HasOne(x => x.Wallets)
                .WithOne(s => s.User)
                .HasForeignKey<Wallet>(w => w.Id)
                .OnDelete(DeleteBehavior.Cascade);

             builder.Entity<Domain.UserMessage.Message>()
               .HasOne(u => u.Recipient)
               .WithMany(m => m.MessagesReceived)
               .OnDelete(DeleteBehavior.Restrict);

             builder.Entity<Domain.UserMessage.Message>()
                 .HasOne(u => u.Sender)
                 .WithMany(m => m.MessagesSent)
                 .OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}
