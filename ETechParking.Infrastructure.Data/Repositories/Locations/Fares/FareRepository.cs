using ETechParking.Domain.Interfaces.Repositories.Locations.Fares;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Repositories.Abstraction;

namespace ETechParking.Infrastructure.Data.Repositories.Locations.Fares;

public class FareRepository(ETechParkingDbContext context) : BaseRepository<Fare, int>(context), IFareRepository
{
}
