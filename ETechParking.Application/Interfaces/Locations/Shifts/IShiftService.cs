using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Enums.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Shifts;

namespace ETechParking.Application.Interfaces.Locations.Shifts;

public interface IShiftService : IBaseService<Shift, ShiftDto, int>
{
    Task<IEnumerable<ShiftDto>> GetAllByUserIdAsync(int userId);
    Task<IEnumerable<ShiftDto>> GetAllPaginatedByUserIdAsync(PaginatedModelDto paginatedModelDto, int userId);
    Task<IEnumerable<TicketDto>> GetAllShiftTicketsAsync(int shiftId);
    Task<ShiftDto> CloseShiftAsync(CloseShiftDto payTicketDto);
    Task<ShiftDto> ConfirmShiftAsync(ConfirmShiftDto confirmShiftDto, int userId);
    Task<long> GetShiftCountAsync(ShiftStatus? status = default!);
}
