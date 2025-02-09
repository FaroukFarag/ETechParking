using ETechParking.Application.Dtos.Abstraction;

namespace ETechParking.Application.Dtos.Locations.Roles;

public class RoleDto : BaseModelDto<int>
{
    public string Name { get; set; } = default!;
}
