using AutoMapper;
using ETechParking.Application.Dtos.Locations;
using ETechParking.Domain.Models.Locations;

namespace ETechParking.Application.AutoMapper.Locations;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>().ReverseMap();
    }
}
