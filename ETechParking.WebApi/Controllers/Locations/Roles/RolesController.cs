﻿using ETechParking.Application.Dtos.Locations.Roles;
using ETechParking.Application.Interfaces.Locations.Roles;
using ETechParking.Domain.Models.Locations.Roles;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Roles;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class RolesController(IRoleService roleService) :
    BaseCrudController<IRoleService, Role, RoleDto, int>(roleService)
{
    private readonly IRoleService _roleService = roleService;
}
