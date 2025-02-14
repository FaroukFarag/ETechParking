using ETechParking.Domain.Models.Locations.Shifts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Shifts;

internal class ShiftConfigurations : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.Property(t => t.StartDateTime)
            .IsRequired();

        builder.Property(f => f.TotalCash)
            .HasPrecision(18, 6);

        builder.Property(f => f.TotalCredit)
            .HasPrecision(18, 6);

        builder.HasOne(s => s.Location)
            .WithMany(l => l.Shifts)
            .HasForeignKey(s => s.LocationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.User)
            .WithMany(u => u.Shifts)
            .HasForeignKey(s => s.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Tickets)
            .WithOne(t => t.Shift)
            .HasForeignKey(s => s.ShiftId)
            .IsRequired();
    }
}
