using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Application.Dtos.Shared;
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
    public async Task<IActionResult> GetAllFiltered(ShiftFilterDto shiftFilterDto)
    {
        return Ok(await _shiftService.GetAllFilteredAsync(shiftFilterDto));
    }

    [HttpGet("GetAllByUserId")]
    public async Task<IActionResult> GetAllByUserId()
    {
        return Ok(await _shiftService.GetAllByUserIdAsync(GetCurrentUserId()));
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
    public async Task<IActionResult> CloseShift(CloseShiftDto payTicketDto)
    {
        return Ok(await _shiftService.CloseShiftAsync(payTicketDto));
    }
}
