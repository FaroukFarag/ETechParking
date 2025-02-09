using ETechParking.Application.Dtos.Locations.Roles;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Models.Locations.Roles;

namespace ETechParking.Application.Interfaces.Locations.Roles;

public interface IRoleService : IBaseService<Role, RoleDto, int>
{
}
