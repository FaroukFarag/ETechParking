using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Users;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("Create")]
    public async Task<IActionResult> Create(UserDto userDto)
    {
        return Ok(await _userService.CreateAsync(userDto));
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var userDto = await _userService.GetAsync(id);

        if (userDto == null)
            return NotFound();

        return Ok(userDto);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _userService.GetAllAsync());
    }

    [HttpPost("GetAllPaginated")]
    public async Task<IActionResult> GetAllPaginated(PaginatedModelDto paginatedModelDto)
    {
        return Ok(await _userService
            .GetAllPaginatedAsync(
                paginatedModel: paginatedModelDto,
                filter: default!,
                orderBy: default!,
                u => u.Role,
                u => u.Location));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(UserDto newUserDto)
    {
        return Ok(await _userService.Update(newUserDto));
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var userDto = await _userService.Delete(id);

        if (userDto == null)
            return NotFound();

        return Ok(userDto);
    }

    [HttpDelete("DeleteRange")]
    public async Task<IActionResult> DeleteRange(IEnumerable<UserDto> usersDtos)
    {
        return Ok(await _userService.DeleteRange(usersDtos));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var loggedInDto = await _userService.LoginAsync(loginDto);

        if (loggedInDto == default!)
        {
            return Unauthorized(new { Message = "Invalid login attempt" });
        }

        return Ok(loggedInDto);
    }
}
