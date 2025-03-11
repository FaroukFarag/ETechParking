using AutoMapper;
using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Shifts;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Domain.Enums.Locations.Shifts;
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
            q => q.Include(s => s.CashierUser)
                .Include(s => s.AccountantUser)
                .Include(s => s.Location));
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
            s => s.CashierUser,
            s => s.AccountantUser!);
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
            s => s.CashierUser,
            s => s.AccountantUser!);
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
            s => s.CashierUser,
            s => s.AccountantUser!);

        return _mapper.Map<IReadOnlyList<ShiftDto>>(shifts);
    }

    public async Task<IEnumerable<ShiftDto>> GetAllByUserIdAsync(int userId)
    {
        var shifts = await _shiftRepository.GetAllAsync(
            filter: s => s.CashierUserId == userId && s.Status == ShiftStatus.Closed,
            orderBy: q => q.OrderByDescending(s => s.StartDateTime),
            s => s.Location,
            s => s.Tickets,
            s => s.CashierUser,
            s => s.AccountantUser!);
        var shiftsDtos = _mapper.Map<IReadOnlyList<ShiftDto>>(shifts);

        return shiftsDtos;
    }

    public async Task<IEnumerable<ShiftDto>> GetAllPaginatedByUserIdAsync(PaginatedModelDto paginatedModelDto, int userId)
    {
        var paginatedModel = _mapper.Map<PaginatedModel>(paginatedModelDto);
        var shifts = await _shiftRepository.GetAllPaginatedAsync(
            paginatedModel: paginatedModel,
            filter: s => s.CashierUserId == userId && s.Status == ShiftStatus.Closed,
            orderBy: q => q.OrderByDescending(s => s.StartDateTime),
            s => s.Location,
            s => s.Tickets,
            s => s.CashierUser,
            s => s.AccountantUser!);
        var shiftsDtos = _mapper.Map<IReadOnlyList<ShiftDto>>(shifts);

        return shiftsDtos;
    }

    public async Task<ShiftDto> GetLastUserOpenedShiftAsync(int userId)
    {
        var shifts = await _shiftRepository.GetAllAsync(
            filter: s => s.CashierUserId == userId && s.Status == ShiftStatus.Closed,
            orderBy: q => q.OrderByDescending(s => s.StartDateTime));

        return _mapper.Map<ShiftDto>(shifts.FirstOrDefault());
    }

    public async Task<ShiftDto> CloseShiftAsync(CloseShiftDto closeShiftDto)
    {
        var shift = await _shiftRepository
            .GetAsync(
                closeShiftDto.Id,
                query => query.Include(s => s.Tickets)
                    .Include(s => s.Location)
                    .Include(s => s.CashierUser)
                    .Include(s => s.AccountantUser)
            );

        shift.EndDateTime = closeShiftDto.EndDateTime;
        shift.CashierTotalCash = closeShiftDto.TotalCash;
        shift.CashierTotalCredit = closeShiftDto.TotalCredit;
        shift.Status = ShiftStatus.Closed;

        _shiftRepository.Update(shift);

        var shiftClosed = await _unitOfWork.Complete();

        if (!shiftClosed)
            return default!;

        var shiftDto = _mapper.Map<ShiftDto>(shift);

        return _mapper.Map<ShiftDto>(shift);
    }

    public async Task<ShiftDto> ConfirmShiftAsync(ConfirmShiftDto confirmShiftDto, int userId)
    {
        var shift = await _shiftRepository
            .GetAsync(
                confirmShiftDto.Id,
                query => query.Include(s => s.Tickets)
                    .Include(s => s.Location)
                    .Include(s => s.CashierUser)
                    .Include(s => s.AccountantUser)
            );

        shift.AccountantUserId = userId;
        shift.AccountantTotalCash = confirmShiftDto.TotalCash;
        shift.AccountantTotalCredit = confirmShiftDto.TotalCredit;
        shift.Status = ShiftStatus.Confirmed;

        _shiftRepository.Update(shift);

        var shiftClosed = await _unitOfWork.Complete();

        if (!shiftClosed)
            return default!;

        return _mapper.Map<ShiftDto>(shift);
    }
}
