using AutoMapper;
using ETechParking.Application.Dtos.Users;
using ETechParking.Domain.Models.Users;

namespace ETechParking.Application.AutoMapper.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, ApplicationUser>();
    }
}
