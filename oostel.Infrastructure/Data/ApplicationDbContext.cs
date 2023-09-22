using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Infrastructure.Data.Configurations;

namespace Oostel.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureApplicationUser();
            builder.ConfigureUserProfile();
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UserOTP> UserOTPs { get; set; }
        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
