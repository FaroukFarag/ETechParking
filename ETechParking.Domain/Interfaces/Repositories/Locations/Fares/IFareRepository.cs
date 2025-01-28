using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Locations.Fares;

namespace ETechParking.Domain.Interfaces.Repositories.Locations.Fares;

public interface IFareRepository : IBaseRepository<Fare, int>
{
}
