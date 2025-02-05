﻿using ETechParking.Application.Dtos.Locations.Users;

namespace ETechParking.Application.Interfaces.Users;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto registerDto);

    Task<string> LoginAsync(LoginDto loginDto);
}
