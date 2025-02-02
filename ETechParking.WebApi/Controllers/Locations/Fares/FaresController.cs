using ETechParking.Application.Dtos.Locations.Fares;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Fares;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Fares;

[Route("api/[controller]")]
[ApiController]
public class FaresController(IFareService fareService) : ControllerBase
{
    private readonly IFareService _fareService = fareService;

    [HttpPost("Create")]
    public async Task<IActionResult> Create(FareDto fareDto)
    {
        return Ok(await _fareService.CreateAsync(fareDto));
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var fareDto = await _fareService.GetAsync(id);

        if (fareDto == null)
            return NotFound();

        return Ok(fareDto);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _fareService.GetAllAsync());
    }

    [HttpPost("GetAllPaginated")]
    public async Task<IActionResult> GetAllPaginated(PaginatedModelDto paginatedModelDto)
    {
        return Ok(await _fareService.GetAllPaginatedAsync(
            paginatedModel: paginatedModelDto,
            includeProperties: f => f.Location));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(FareDto newFareDto)
    {
        return Ok(await _fareService.Update(newFareDto));
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var fareDto = await _fareService.Delete(id);

        if (fareDto == null)
            return NotFound();

        return Ok(fareDto);
    }

    [HttpDelete("DeleteRange")]
    public async Task<IActionResult> DeleteRange(IEnumerable<FareDto> fareDtos)
    {
        return Ok(await _fareService.DeleteRange(fareDtos));
    }
}
