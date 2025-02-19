using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Enums.Locations.Tickets;
using ETechParking.Domain.Lookups.Locations.Fares;
using ETechParking.Domain.Lookups.Locations.Tickets;
using ETechParking.Domain.Models.Locations;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Locations.Roles;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Users;
using ETechParking.Infrastructure.Data.ModelsConfigurations.Locations;
using ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Fares;
using ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Roles;
using ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Shifts;
using ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Tickets;
using ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Users;
using ETechParking.Infrastructure.Data.ModelsConfigurations.Lookups.Abstraction;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETechParking.Infrastructure.Data.Context;

public class ETechParkingDbContext(DbContextOptions options) : IdentityDbContext<User, Role, int>(options)
{
    public DbSet<Location> Locations { get; set; }
    public DbSet<Fare> Fares { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<FareTypeLookup> FareTypeLookups { get; set; }
    public DbSet<TransactionTypeLookup> TransactionTypeLookups { get; set; }
    public DbSet<ClientTypeLookup> ClientTypeLookups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new LocationConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new RoleConfigurations());
        modelBuilder.ApplyConfiguration(new FareConfigurations());
        modelBuilder.ApplyConfiguration(new TicketConfigurations());
        modelBuilder.ApplyConfiguration(new ShiftConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new BaseLookupConfiguration<FareTypeLookup, FareType>("FareTypes"));
        modelBuilder.ApplyConfiguration(new BaseLookupConfiguration<TransactionTypeLookup, TransactionType>("TransactionTypes"));
        modelBuilder.ApplyConfiguration(new BaseLookupConfiguration<ClientTypeLookup, ClientType>("ClientTypes"));
    }
}