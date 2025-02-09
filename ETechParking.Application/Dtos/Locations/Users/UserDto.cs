using ETechParking.Application.Dtos.Abstraction;

namespace ETechParking.Application.Dtos.Locations.Users;

public class UserDto : BaseModelDto<int>
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public int LocationId { get; set; }
    public string? LocationName { get; set; }
    public int RoleId { get; set; }
    public string? RoleName { get; set; }
}
