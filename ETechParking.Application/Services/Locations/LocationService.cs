using AutoMapper;
using ETechParking.Application.Dtos.Locations;
using ETechParking.Application.Interfaces.Locations;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations;

namespace ETechParking.Application.Services.Locations;

public class LocationService(IBaseRepository<Location, int> repository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<Location, LocationDto, int>(repository, unitOfWork, mapper), ILocationService
{
}
