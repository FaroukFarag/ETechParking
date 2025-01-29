using AutoMapper;
using ETechParking.Application.Dtos.Locations.Fares;
using ETechParking.Application.Interfaces.Locations.Fares;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Locations.Fares;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations.Fares;

namespace ETechParking.Application.Services.Locations.Fares;

public class FareService(IFareRepository fareRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<Fare, FareDto, int>(fareRepository, unitOfWork, mapper), IFareService
{
}
