using AutoMapper;
using ETechParking.Application.Dtos.Locations.Roles;
using ETechParking.Domain.Models.Locations.Roles;

namespace ETechParking.Application.AutoMapper.Locations.Roles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}
