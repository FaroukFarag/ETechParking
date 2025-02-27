using ETechParking.Domain.Models.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETechParking.Infrastructure.Data.ModelsConfigurations.Locations;

public class LocationConfigurations : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.Property(l => l.Country)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(l => l.City)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(l => l.Longitude)
            .HasPrecision(18, 6)
            .IsRequired();

        builder.Property(l => l.Latitude)
            .HasPrecision(18, 6)
            .IsRequired();

        builder.HasMany(l => l.Users)
            .WithOne(u => u.Location)
            .HasForeignKey(u => u.LocationId)
            .IsRequired();

        builder.HasMany(l => l.Fares)
            .WithOne(f => f.Location)
            .HasForeignKey(f => f.LocationId)
            .IsRequired();

        builder.HasMany(l => l.Tickets)
            .WithOne(t => t.Location)
            .HasForeignKey(t => t.LocationId)
            .IsRequired();

        builder.HasMany(l => l.Shifts)
            .WithOne(s => s.Location)
            .HasForeignKey(s => s.LocationId)
            .IsRequired();
    }
}
