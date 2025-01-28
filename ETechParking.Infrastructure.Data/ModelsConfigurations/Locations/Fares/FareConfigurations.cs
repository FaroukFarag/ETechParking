using ETechParking.Domain.Models.Locations.Fares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Fares;

public class FareConfigurations : IEntityTypeConfiguration<Fare>
{
    public void Configure(EntityTypeBuilder<Fare> builder)
    {
        builder.Property(f => f.Amount)
            .IsRequired();

        builder.Property(f => f.FareType)
            .IsRequired();

        builder.HasOne(f => f.Location)
            .WithMany(l => l.Fares)
            .HasForeignKey(f => f.LocationId)
            .IsRequired();
    }
}
