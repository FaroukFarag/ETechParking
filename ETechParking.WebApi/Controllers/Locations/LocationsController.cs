using ETechParking.Application.Dtos.Locations;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations;

[Route("api/[controller]")]
[ApiController]
public class LocationsController(ILocationService locationService) : ControllerBase
{
    private readonly ILocationService _locationService = locationService;

    [HttpPost("Create")]
    public async Task<IActionResult> Create(LocationDto locationDto)
    {
        return Ok(await _locationService.CreateAsync(locationDto));
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var locationDto = await _locationService.GetAsync(id);

        if (locationDto == null)
            return NotFound();

        return Ok(locationDto);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _locationService.GetAllAsync());
    }

    [HttpPost("GetAllPaginated")]
    public async Task<IActionResult> GetAllPaginated(PaginatedModelDto paginatedModelDto)
    {
        return Ok(await _locationService.GetAllPaginatedAsync(paginatedModelDto));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(LocationDto newLocationDto)
    {
        return Ok(await _locationService.Update(newLocationDto));
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var locationDto = await _locationService.Delete(id);

        if (locationDto == null)
            return NotFound();

        return Ok(locationDto);
    }

    [HttpDelete("DeleteRange")]
    public async Task<IActionResult> DeleteRange(IEnumerable<LocationDto> locationDtos)
    {
        return Ok(await _locationService.DeleteRange(locationDtos));
    }
}
