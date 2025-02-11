using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Application.Interfaces.Locations.Users;
using ETechParking.Domain.Models.Locations.Users;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) :
    BaseController<IUserService, User, UserDto, int>(userService)
{
    private readonly IUserService _userService = userService;

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
