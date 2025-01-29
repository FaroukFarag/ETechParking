using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Application.Interfaces.Locations.Users;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var token = await _userService.RegisterAsync(registerDto);

        if (token == default!)
        {
            return BadRequest(new { Message = "User registration failed" });
        }

        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var token = await _userService.LoginAsync(loginDto);

        if (token == default!)
        {
            return Unauthorized(new { Message = "Invalid login attempt" });
        }

        return Ok(token);
    }
}
