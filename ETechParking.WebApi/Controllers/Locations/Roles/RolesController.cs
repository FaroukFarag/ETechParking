using ETechParking.Application.Dtos.Locations.Roles;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Roles;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Roles;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpPost("Create")]
    public async Task<IActionResult> Create(RoleDto roleDto)
    {
        return Ok(await _roleService.CreateAsync(roleDto));
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var roleDto = await _roleService.GetAsync(id);

        if (roleDto == null)
            return NotFound();

        return Ok(roleDto);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _roleService.GetAllAsync());
    }

    [HttpPost("GetAllPaginated")]
    public async Task<IActionResult> GetAllPaginated(PaginatedModelDto paginatedModelDto)
    {
        return Ok(await _roleService.GetAllPaginatedAsync(paginatedModelDto));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(RoleDto newRoleDto)
    {
        return Ok(await _roleService.Update(newRoleDto));
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var roleDto = await _roleService.Delete(id);

        if (roleDto == null)
            return NotFound();

        return Ok(roleDto);
    }

    [HttpDelete("DeleteRange")]
    public async Task<IActionResult> DeleteRange(IEnumerable<RoleDto> rolesDtos)
    {
        return Ok(await _roleService.DeleteRange(rolesDtos));
    }
}
