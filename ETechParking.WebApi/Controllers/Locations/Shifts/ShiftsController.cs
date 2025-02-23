using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Application.Interfaces.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Shifts;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Accountant,Cashier")]
public class ShiftsController(IShiftService shiftService) : BaseController<IShiftService, Shift, ShiftDto, int>(shiftService)
{
    private readonly IShiftService _shiftService = shiftService;

    [HttpPost("GetAllFiltered")]
    public async Task<IActionResult> GetAllFiltered(TicketFilterDto ticketFilterDto)
    {
        return Ok(await _shiftService.GetAllFilteredAsync(ticketFilterDto));
    }

    [HttpPost("CloseShift")]
    public async Task<IActionResult> CloseShift(CloseShiftDto payTicketDto)
    {
        return Ok(await _shiftService.CloseShift(payTicketDto));
    }
}
