using AutoMapper;
using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Tickets;
using System.Text;

namespace ETechParking.Application.AutoMapper.Locations.Tickets;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<TicketDto, Ticket>();

        CreateMap<Ticket, TicketDto>()
            .ForMember(des => des.ClientTypeName, opt => opt.MapFrom(src => src.ClientType.ToString()))
            .ForMember(des => des.TransactionTypeName, opt => opt.MapFrom(src => src.TransactionType.ToString()))
            .ForMember(des => des.LocationName, opt => opt.MapFrom(src => src.Location.Name))
            .ForMember(des => des.CreateUserName, opt => opt.MapFrom(src => src.CreateUser.UserName))
            .ForMember(des => des.CloseUserName, opt => opt.MapFrom(src => src.CloseUser!.UserName))
            .ForMember(des => des.QrCode, opt => opt
                .MapFrom(src => Convert.ToBase64String(
                    Encoding.UTF8
                        .GetBytes($"Id: {src.Id},TicketNumber: {src.TicketNumber}, PlateNumber: {src.PlateNumber}"))));

        CreateMap<TicketStatistics, TicketTransactionTypeDto>().ReverseMap();
    }
}
