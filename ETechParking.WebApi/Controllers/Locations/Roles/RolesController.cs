using ETechParking.Application.Dtos.Locations.Roles;
using ETechParking.Application.Interfaces.Locations.Roles;
using ETechParking.Domain.Models.Locations.Roles;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Roles;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleService roleService) :
    BaseController<IRoleService, Role, RoleDto, int>(roleService)
{
    private readonly IRoleService _roleService = roleService;
}
