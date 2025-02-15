using AutoMapper;
using ETechParking.Application.Dtos.Locations.Shifts;
using ETechParking.Domain.Enums.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Shifts;

namespace ETechParking.Application.AutoMapper.Locations.Shifts;

public class ShiftProfile : Profile
{
    public ShiftProfile()
    {
        CreateMap<Shift, ShiftDto>()
            .ForMember(des => des.LocationName, opt => opt.MapFrom(src => src.Location.Name))
            .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(des => des.TotalCashDifference, opt => opt
                .MapFrom(src => src.TotalCash - src.Tickets
                    .Where(t => t.TransactionType == TransactionType.Cash).Sum(t => t.TotalAmount)))
            .ForMember(des => des.TotalCreditDifference, opt => opt
                .MapFrom(src => src.TotalCredit - src.Tickets
                    .Where(t => t.TransactionType == TransactionType.Credit).Sum(t => t.TotalAmount)))
            .ForMember(des => des.TotalVisitors, opt => opt
                .MapFrom(src => src.Tickets.Where(t => t.ClientType == ClientType.Visitor).Count()))
            .ForMember(des => des.TotalGuests, opt => opt
                .MapFrom(src => src.Tickets.Where(t => t.ClientType == ClientType.Guest).Count()));

        CreateMap<ShiftDto, Shift>();
    }
}
