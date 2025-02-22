using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Models.Locations.Shifts;

namespace ETechParking.Application.Interfaces.Locations.Shifts;

public interface IShiftService : IBaseService<Shift, ShiftDto, int>
{
    Task<ShiftDto> CloseShift(CloseShiftDto payTicketDto);
    Task<ShiftDto> ReviewShift(ReviewShiftDto reviewShiftDto);
}
