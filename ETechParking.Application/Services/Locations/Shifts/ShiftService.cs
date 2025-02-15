using AutoMapper;
using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Dtos.Locations.Tickets;
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

    public async override Task<IEnumerable<ShiftDto>> GetAllAsync()
    {
        var shifts = await _shiftRepository.GetAllAsync(
            filter: default!,
            orderBy: default!,
            s => s.Location,
            s => s.Tickets);
        var shiftsDtos = _mapper.Map<IReadOnlyList<ShiftDto>>(shifts);

        return shiftsDtos;
    }

    public async override Task<IEnumerable<ShiftDto>> GetAllPaginatedAsync(PaginatedModelDto paginatedModelDto)
    {
        var paginatedModel = _mapper.Map<PaginatedModel>(paginatedModelDto);
        var shifts = await _shiftRepository.GetAllPaginatedAsync(
            paginatedModel: paginatedModel,
            filter: default!,
            orderBy: default!,
            s => s.Location,
            s => s.Tickets);
        var shiftsDtos = _mapper.Map<IReadOnlyList<ShiftDto>>(shifts);

        return shiftsDtos;
    }

    public async Task<IEnumerable<TicketDto>> GetAllFilteredAsync(ShiftFilterDto shiftFilterDto)
    {
        var tickets = await _shiftRepository.GetAllFilteredAsync(filterDto: shiftFilterDto, includeProperties: t => t.Location);

        return _mapper.Map<IReadOnlyList<TicketDto>>(tickets);
    }

    public async Task<ShiftDto> CloseShift(CloseShiftDto closeShiftDto)
    {
        var shift = await _shiftRepository
            .GetAsync(closeShiftDto.Id, query => query.Include(s => s.Tickets).Include(s => s.Location));

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
