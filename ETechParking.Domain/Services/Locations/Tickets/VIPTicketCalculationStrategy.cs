using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Services.Locations.Tickets;

public class VIPTicketCalculationStrategy : BaseTicketCalculationStrategy
{
    protected override FareType FareType => ETechParking.Domain.Enums.Locations.Fares.FareType.VIP;
}