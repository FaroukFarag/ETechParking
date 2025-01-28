using ETechParking.Domain.Interfaces.Repositories.Locations;
using ETechParking.Domain.Models.Locations;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Repositories.Abstraction;

namespace ETechParking.Infrastructure.Data.Repositories.Locations;

public class LocationRepository(ETechParkingDbContext context) : BaseRepository<Location, int>(context), ILocationRepository
{
}
