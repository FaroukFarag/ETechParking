namespace ETechParking.Application.Dtos.Locations.Users;

public class ForgotPasswordDto
{
    public string UserName { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}
