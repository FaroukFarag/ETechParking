using ETechParking.Domain.Interfaces.Repositories.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Repositories.Abstraction;

namespace ETechParking.Infrastructure.Data.Repositories.Locations.Shifts;

public class ShiftRepository(ETechParkingDbContext context) : BaseRepository<Shift, int>(context), IShiftRepository
{
}
