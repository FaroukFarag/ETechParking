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

        builder.Property(l => l.Country)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(l => l.City)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(l => l.Longitude)
            .IsRequired();

        builder.Property(l => l.Latitude)
            .IsRequired();
    }
}
