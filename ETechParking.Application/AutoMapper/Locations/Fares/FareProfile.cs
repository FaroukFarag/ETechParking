using AutoMapper;
using ETechParking.Application.Dtos.Locations.Fares;
using ETechParking.Domain.Models.Locations.Fares;

namespace ETechParking.Application.AutoMapper.Locations.Fares;

public class FareProfile : Profile
{
    public FareProfile()
    {
        CreateMap<FareDto, Fare>();

        CreateMap<Fare, FareDto>()
            .ForMember(des => des.LocationName, opt => opt
                .MapFrom(src => src.Location.Name))
            .ForMember(des => des.FareTypeName, opt => opt
                .MapFrom(src => src.FareType.ToString()));
    }
}
