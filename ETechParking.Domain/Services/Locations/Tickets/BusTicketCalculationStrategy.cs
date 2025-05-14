using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Services.Locations.Tickets;

public class BusTicketCalculationStrategy : BaseTicketCalculationStrategy
{
    protected override FareType VehicleFareType => FareType.Bus;
}