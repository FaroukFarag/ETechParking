using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Services.Locations.Tickets;

public class NormalTicketCalculationStrategy : BaseTicketCalculationStrategy
{
    protected override FareType FareType => ETechParking.Domain.Enums.Locations.Fares.FareType.Normal;
}