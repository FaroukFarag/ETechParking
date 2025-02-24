using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Models.Locations.Users;

namespace ETechParking.Application.Interfaces.Locations.Users;

public interface IUserService : IBaseService<User, UserDto, int>
{
    Task<LoggedInDto> LoginAsync(LoginDto model, bool isCashier);
    Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
}
