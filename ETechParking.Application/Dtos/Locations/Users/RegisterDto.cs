﻿namespace ETechParking.Application.Dtos.Locations.Users;

public class RegisterDto
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public int LocationId { get; set; }
}
