using AutoMapper;
using ETechParking.Application.Dtos.Abstraction;
using ETechParking.Domain.Models.Abstraction;

namespace ETechParking.Application.AutoMapper.Abstraction;

public class BaseModelProfile : Profile
{
    public BaseModelProfile()
    {
        CreateMap<BaseModel, BaseModelDto>().ReverseMap();
    }
}
