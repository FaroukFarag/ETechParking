using ETechParking.Application.Dtos.Locations;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Models.Locations;

namespace ETechParking.Application.Interfaces.Locations;

public interface ILocationService : IBaseService<Location, LocationDto, int>
{
}
