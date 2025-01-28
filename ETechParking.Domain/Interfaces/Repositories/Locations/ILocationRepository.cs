using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Locations;

namespace ETechParking.Domain.Interfaces.Repositories.Locations;

public interface ILocationRepository : IBaseRepository<Location, int>
{
}
