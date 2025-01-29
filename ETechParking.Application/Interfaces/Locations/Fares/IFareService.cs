using ETechParking.Application.Dtos.Locations.Fares;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Models.Locations.Fares;

namespace ETechParking.Application.Interfaces.Locations.Fares;

public interface IFareService : IBaseService<Fare, FareDto, int>
{
}
