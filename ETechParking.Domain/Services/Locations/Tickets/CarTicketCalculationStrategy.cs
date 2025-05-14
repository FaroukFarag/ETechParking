using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Services.Locations.Tickets;

public class CarTicketCalculationStrategy : BaseTicketCalculationStrategy
{
    protected override FareType VehicleFareType => FareType.Car;
}