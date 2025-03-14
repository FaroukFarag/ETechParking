using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Shifts;
using ETechParking.Domain.Enums.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Shifts;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Supervisor,Accountant,Cashier")]
public class ShiftsController(IShiftService shiftService) : BaseController<IShiftService, Shift, ShiftDto, int>(shiftService)
{
    private readonly IShiftService _shiftService = shiftService;

    [HttpPost("GetAllFiltered")]
    public async Task<IActionResult> GetAllFiltered(ShiftFilterDto shiftFilterDto)
    {
        return Ok(await _shiftService.GetAllFilteredAsync(shiftFilterDto));
    }

    [HttpGet("GetAllByUserId")]
    public async Task<IActionResult> GetAllByUserId()
    {
        return Ok(await _shiftService.GetAllByUserIdAsync(GetCurrentUserId()));
    }

    [HttpGet("GetAllShiftTickets")]
    public async Task<IActionResult> GetAllShiftTickets(int shiftId)
    {
        return Ok(await _shiftService.GetAllShiftTicketsAsync(shiftId));
    }

    [HttpGet("GetAllPaginatedByUserId")]
    public async Task<IActionResult> GetAllPaginatedByUserId(PaginatedModelDto paginatedModelDto)
    {
        return Ok(await _shiftService.GetAllPaginatedByUserIdAsync(
            paginatedModelDto,
            GetCurrentUserId())
        );
    }

    [HttpPost("CloseShift")]
    public async Task<IActionResult> CloseShift(CloseShiftDto closeShiftDto)
    {
        return Ok(await _shiftService.CloseShiftAsync(closeShiftDto));
    }

    [HttpPost("ConfirmShift")]
    public async Task<IActionResult> ConfirmShift(ConfirmShiftDto confirmShiftDto)
    {
        return Ok(await _shiftService.ConfirmShiftAsync(confirmShiftDto, GetCurrentUserId()));
    }

    [HttpGet("GetTotalShifts")]
    public async Task<IActionResult> GetShiftsCount([FromQuery] ShiftStatus? status = default!)
    {
        var count = await _shiftService.GetShiftCountAsync(status);

        return Ok(count);
    }
}
