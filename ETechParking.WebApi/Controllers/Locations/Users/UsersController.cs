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

    [HttpPost("ResetPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var result = await _userService.ResetPasswordAsync(resetPasswordDto);

        return HandleResult(result, "Password reset successfully.", "Failed to reset password. User not found or invalid request.");
    }

    [HttpPost("ForgotPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto request)
    {
        var result = await _userService.ForgotPasswordAsync(request);

        return HandleResult(result, "Password reset successfully.", "Failed to reset password. User not found or invalid request.");
    }

    private async Task<IActionResult> HandleLoginAsync(LoginDto loginDto, bool isCashier)
    {
        var loggedInDto = await _userService.LoginAsync(loginDto, isCashier);

        return loggedInDto is null
            ? Unauthorized(new { Message = "Invalid login attempt" })
            : Ok(loggedInDto);
    }

    private IActionResult HandleResult(bool result, string successMessage, string errorMessage)
    {
        return result
            ? Ok(new { Message = successMessage })
            : BadRequest(new { Message = errorMessage });
    }
}
