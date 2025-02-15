using AutoMapper;
using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Application.AutoMapper.Locations.Tickets;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<TicketDto, Ticket>();

        CreateMap<Ticket, TicketDto>()
            .ForMember(des => des.LocationName, opt => opt.MapFrom(src => src.Location.Name))
            .ForMember(des => des.CreateUserName, opt => opt.MapFrom(src => src.CreateUser.UserName))
            .ForMember(des => des.CloseUserName, opt => opt.MapFrom(src => src.CreateUser.UserName));
    }
}
