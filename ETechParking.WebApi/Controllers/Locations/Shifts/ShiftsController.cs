using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Interfaces.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Shifts;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController(IShiftService shiftService) : BaseController<IShiftService, Shift, ShiftDto, int>(shiftService)
{
    private readonly IShiftService _shiftService = shiftService;

    [HttpPost("CloseShift")]
    public async Task<IActionResult> CloseShift(CloseShiftDto payTicketDto)
    {
        return Ok(await _shiftService.CloseShift(payTicketDto));
    }
}
