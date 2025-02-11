using AutoMapper;
using ETechParking.Application.Dtos.Locations.Fares;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Fares;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Locations.Fares;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Shared;

namespace ETechParking.Application.Services.Locations.Fares;

public class FareService(IFareRepository fareRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<Fare, FareDto, int>(fareRepository, unitOfWork, mapper), IFareService
{
    private readonly IFareRepository _fareRepository = fareRepository;
    private readonly IMapper _mapper = mapper;

    public async override Task<IEnumerable<FareDto>> GetAllAsync()
    {
        var fares = await _fareRepository.GetAllAsync(includeProperties: f => f.Location);
        var faresDtos = _mapper.Map<IReadOnlyList<FareDto>>(fares);

        return faresDtos;
    }

    public async override Task<IEnumerable<FareDto>> GetAllPaginatedAsync(PaginatedModelDto paginatedModelDto)
    {
        var paginatedModel = _mapper.Map<PaginatedModel>(paginatedModelDto);
        var fares = await _fareRepository.GetAllPaginatedAsync(
            paginatedModel: paginatedModel,
            includeProperties: f => f.Location);
        var faresDtos = _mapper.Map<IReadOnlyList<FareDto>>(fares);

        return faresDtos;
    }
}
