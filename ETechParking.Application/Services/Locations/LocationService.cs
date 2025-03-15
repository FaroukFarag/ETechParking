using AutoMapper;
using ETechParking.Application.Dtos.Locations;
using ETechParking.Application.Interfaces.Locations;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Locations;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations;

namespace ETechParking.Application.Services.Locations;

public class LocationService(ILocationRepository locationRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<Location, LocationDto, int>(locationRepository, unitOfWork, mapper), ILocationService
{
    private readonly ILocationRepository _locationRepository = locationRepository;

    public async Task<long> GetLocationCountAsync()
    {
        return await _locationRepository.GetCountAsync();
    }
}
