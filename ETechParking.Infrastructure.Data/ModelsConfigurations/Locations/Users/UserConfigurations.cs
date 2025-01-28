using ETechParking.Domain.Models.Locations.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Users;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(f => f.Location)
            .WithMany(l => l.Users)
            .HasForeignKey(f => f.LocationId)
            .IsRequired();
    }
}
