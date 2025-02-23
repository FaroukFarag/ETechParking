using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Application.Interfaces.Locations.Users;
using ETechParking.Domain.Models.Locations.Users;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Users;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController(IUserService userService)
    : BaseController<IUserService, User, UserDto, int>(userService)
{
    private readonly IUserService _userService = userService;

    [HttpPost("CashierLogin")]
    [AllowAnonymous]
    public async Task<IActionResult> CashierLogin(LoginDto loginDto)
        => await HandleLoginAsync(loginDto, isCashier: true);

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDto)
        => await HandleLoginAsync(loginDto, isCashier: false);

    private async Task<IActionResult> HandleLoginAsync(LoginDto loginDto, bool isCashier)
    {
        var loggedInDto = await _userService.LoginAsync(loginDto, isCashier);

        return loggedInDto is null
            ? Unauthorized(new { Message = "Invalid login attempt" })
            : Ok(loggedInDto);
    }
}
