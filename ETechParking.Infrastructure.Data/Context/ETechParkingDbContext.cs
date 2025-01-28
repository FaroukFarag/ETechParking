using ETechParking.Domain.Models.Locations;
using ETechParking.Domain.Models.Locations.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETechParking.Infrastructure.Data.Context;

public class ETechParkingDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}