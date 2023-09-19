using Microsoft.EntityFrameworkCore;
using Oostel.Domain.Hostel.Entities;
using System.Reflection.Emit;

namespace Oostel.Infrastructure.Data.Configurations
{
    public static class HostelConfiguration
    {
        public static void ConfigureHostelSystem(this ModelBuilder builder)
        {
            builder.Entity<Hostel>().HasKey(u => u.Id);

            builder.Entity<Hostel>()
                .HasMany(r => r.Rooms)
                .WithOne(h => h.Hostel)
                .HasForeignKey(h => h.HostelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Hostel>()
                .Property(h => h.LastModifiedDate)
                .IsConcurrencyToken();
        }
    }
}
