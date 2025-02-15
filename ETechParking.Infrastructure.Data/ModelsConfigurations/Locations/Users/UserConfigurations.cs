using ETechParking.Domain.Models.Locations.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Users;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Location)
            .WithMany(l => l.Users)
            .HasForeignKey(u => u.LocationId)
            .IsRequired();

        builder.HasMany(u => u.Shifts)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .IsRequired();

        builder.HasMany(u => u.CreatedTickets)
            .WithOne(t => t.CreateUser)
            .HasForeignKey(t => t.CreateUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.ClosedTickets)
            .WithOne(t => t.CloseUser)
            .HasForeignKey(t => t.CloseUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
