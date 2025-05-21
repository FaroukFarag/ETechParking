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
            .ForMember(des => des.CashierUserName, opt => opt
                .MapFrom(src => src.CashierUser.UserName))
            .ForMember(des => des.AccountantUserName, opt => opt
                .MapFrom(src => src.AccountantUser!.UserName))
            .ForMember(des => des.TotalCashCaculated, opt => opt
                .MapFrom(src => src.Tickets
                    .Where(t => t.TransactionType == TransactionType.Cash).Sum(t => t.TotalAmount)))
            .ForMember(des => des.CashierTotalCashDifference, opt => opt
                .MapFrom(src => src.CashierTotalCash - src.Tickets
                    .Where(t => t.TransactionType == TransactionType.Cash).Sum(t => t.TotalAmount)))
            .ForMember(des => des.AccountantTotalCashDifference, opt => opt
                .MapFrom(src => src.AccountantTotalCash - src.Tickets
                    .Where(t => t.TransactionType == TransactionType.Cash).Sum(t => t.TotalAmount)))
            .ForMember(des => des.TotalCreditCaculated, opt => opt
                .MapFrom(src => src.Tickets
                    .Where(t => t.TransactionType == TransactionType.Credit).Sum(t => t.TotalAmount)))
            .ForMember(des => des.CashierTotalCreditDifference, opt => opt
                .MapFrom(src => src.CashierTotalCredit - src.Tickets
                    .Where(t => t.TransactionType == TransactionType.Credit).Sum(t => t.TotalAmount)))
            .ForMember(des => des.AccountantTotalCreditDifference, opt => opt
                .MapFrom(src => src.AccountantTotalCredit - src.Tickets
                    .Where(t => t.TransactionType == TransactionType.Credit).Sum(t => t.TotalAmount)))
            .ForMember(des => des.TotalVisitors, opt => opt
                .MapFrom(src => src.Tickets.Where(t => t.ClientType == ClientType.Normal).Count()))
            .ForMember(des => des.TotalGuests, opt => opt
                .MapFrom(src => src.Tickets.Where(t => t.ClientType == ClientType.VIP).Count()));

        CreateMap<ShiftDto, Shift>();
    }
}
