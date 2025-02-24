using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Models.Locations.Shifts;

namespace ETechParking.Application.Interfaces.Locations.Shifts;

public interface IShiftService : IBaseService<Shift, ShiftDto, int>
{
    Task<IEnumerable<ShiftDto>> GetAllByUserIdAsync(int userId);
    Task<IEnumerable<ShiftDto>> GetAllPaginatedByUserIdAsync(PaginatedModelDto paginatedModelDto, int userId);
    Task<ShiftDto> CloseShiftAsync(CloseShiftDto payTicketDto);
    Task<ShiftDto> ReviewShiftAsync(ReviewShiftDto reviewShiftDto);
}
