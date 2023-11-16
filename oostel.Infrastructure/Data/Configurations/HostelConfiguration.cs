using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

            builder.Entity<HostelLikes>()
            .HasOne(hl => hl.LikedHostel)
            .WithMany()
            .HasForeignKey(hl => hl.LikedHostelId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Hostel>()
                .Property(h => h.LastModifiedDate)
                .IsConcurrencyToken();

            builder.Entity<Hostel>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.HostelFacilities)
                    .HasConversion(
                        from => string.Join(";", from),
                        to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        new ValueComparer<List<string>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()
                    )
                );
            });

            builder.Entity<Hostel>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.RulesAndRegulation)
                    .HasConversion(
                        from => string.Join(";", from),
                        to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        new ValueComparer<List<string>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()
                    )
                );
            });

            builder.Entity<Room>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.RoomFacilities)
                    .HasConversion(
                        from => string.Join(";", from),
                        to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        new ValueComparer<List<string>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()
                    )
                );
            });

            builder.Entity<Room>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.RoomPictures)
                    .HasConversion(
                        from => string.Join(";", from),
                        to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        new ValueComparer<List<string>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()
                    )
                );
            });
        }
    }
}
