using AutoMapper;
using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Domain.Models.Locations.Users;

namespace ETechParking.Application.AutoMapper.Locations.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>();
    }
}
