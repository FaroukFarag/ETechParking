using AutoMapper;
using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Shifts;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Locations.Shifts;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Domain.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace ETechParking.Application.Services.Locations.Shifts;

public class ShiftService(IShiftRepository shiftRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<Shift, ShiftDto, int>(shiftRepository, unitOfWork, mapper), IShiftService
{
    private readonly IShiftRepository _shiftRepository = shiftRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async override Task<ShiftDto> GetAsync(int id)
    {
        var shift = await _shiftRepository.GetAsync(
            id,
            q => q.Include(s => s.User).Include(s => s.Location));
        var shiftDto = _mapper.Map<ShiftDto>(shift);

        return shiftDto;
    }

    public async override Task<IEnumerable<ShiftDto>> GetAllAsync()
    {
        var shifts = await _shiftRepository.GetAllAsync(
            filter: default!,
            orderBy: q => q.OrderByDescending(s => s.StartDateTime),
            s => s.Location,
            s => s.Tickets,
            s => s.User);
        var shiftsDtos = _mapper.Map<IReadOnlyList<ShiftDto>>(shifts);

        return shiftsDtos;
    }

    public async override Task<IEnumerable<ShiftDto>> GetAllPaginatedAsync(PaginatedModelDto paginatedModelDto)
    {
        var paginatedModel = _mapper.Map<PaginatedModel>(paginatedModelDto);
        var shifts = await _shiftRepository.GetAllPaginatedAsync(
            paginatedModel: paginatedModel,
            filter: default!,
            orderBy: q => q.OrderByDescending(s => s.StartDateTime),
            s => s.Location,
            s => s.Tickets,
            s => s.User);
        var shiftsDtos = _mapper.Map<IReadOnlyList<ShiftDto>>(shifts);

        return shiftsDtos;
    }

    public async Task<IEnumerable<ShiftDto>> GetAllFilteredAsync(ShiftFilterDto shiftFilterDto)
    {
        var shifts = await _shiftRepository.GetAllFilteredAsync(
            filterDto: shiftFilterDto,
            filter: default!,
            orderBy: q => q.OrderByDescending(s => s.StartDateTime),
            s => s.Location,
            s => s.Tickets,
            s => s.User);

        return _mapper.Map<IReadOnlyList<ShiftDto>>(shifts);
    }

    public async Task<ShiftDto> CloseShift(CloseShiftDto closeShiftDto)
    {
        var shift = await _shiftRepository
            .GetAsync(
                closeShiftDto.Id,
                query => query.Include(s => s.Tickets)
                    .Include(s => s.Location).Include(s => s.User)
            );

        shift.EndDateTime = closeShiftDto.EndDateTime;
        shift.TotalCash = closeShiftDto.TotalCash;
        shift.TotalCredit = closeShiftDto.TotalCredit;

        _shiftRepository.Update(shift);

        var shiftClosed = await _unitOfWork.Complete();

        if (!shiftClosed)
            return default!;

        return _mapper.Map<ShiftDto>(shift);
    }
}
