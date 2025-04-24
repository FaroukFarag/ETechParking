using ETechParking.Application.Dtos.Locations;
using ETechParking.Application.Interfaces.Locations;
using ETechParking.Domain.Models.Locations;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class LocationsController(ILocationService locationService) :
    BaseCrudController<ILocationService, Location, LocationDto, int>(locationService)
{
    private readonly ILocationService _locationService = locationService;

    [HttpGet("GetTotalLocations")]
    public async Task<IActionResult> GetLocationsCount()
    {
        var count = await _locationService.GetLocationCountAsync();

        return Ok(count);
    }
}
