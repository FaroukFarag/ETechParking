using AutoMapper;
using ETechParking.Application.Dtos.Locations.Roles;
using ETechParking.Application.Interfaces.Locations.Roles;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Locations.Roles;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations.Roles;
using Microsoft.AspNetCore.Identity;

namespace ETechParking.Application.Services.Locations.Roles;

public class RoleService(
    IRoleRepository repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    RoleManager<Role> roleManager) :
    BaseService<Role, RoleDto, int>(repository, unitOfWork, mapper), IRoleService
{
    private readonly IMapper _mapper = mapper;
    private readonly RoleManager<Role> _roleManager = roleManager;

    public async override Task<RoleDto> CreateAsync(RoleDto roleDto)
    {
        var role = _mapper.Map<Role>(roleDto);

        var result = await _roleManager.CreateAsync(role);

        return result.Succeeded ? roleDto : default!;
    }

    public async override Task<RoleDto> Update(RoleDto newRoleDto)
    {
        var role = _mapper.Map<Role>(newRoleDto);

        var result = await _roleManager.UpdateAsync(role);

        return result.Succeeded ? newRoleDto : default!;
    }
}
