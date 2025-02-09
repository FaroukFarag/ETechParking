using AutoMapper;
using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Domain.Models.Locations.Users;

namespace ETechParking.Application.AutoMapper.Locations.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, User>();

        CreateMap<User, UserDto>()
            .ForMember(des => des.RoleName, opt => opt.MapFrom(src => src.Role.Name))
            .ForMember(des => des.LocationName, opt => opt.MapFrom(src => src.Location.Name));
    }
}
