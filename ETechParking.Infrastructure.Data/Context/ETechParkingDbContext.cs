using ETechParking.Domain.Models.Locations;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Locations.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETechParking.Infrastructure.Data.Context;

public class ETechParkingDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<Location> Locations { get; set; }
    public DbSet<Fare> Fares { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfiguration(new LocationConfigurations());
        //modelBuilder.ApplyConfiguration(new FareConfigurations());
        //modelBuilder.ApplyConfiguration(new UserConfigurations());
    }
}