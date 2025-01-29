using AutoMapper;
using ETechParking.Application.Dtos.Locations.Fares;
using ETechParking.Domain.Models.Locations.Fares;

namespace ETechParking.Application.AutoMapper.Locations.Fares;

public class FareProfile : Profile
{
    public FareProfile()
    {
        CreateMap<Fare, FareDto>().ReverseMap();
    }
}
