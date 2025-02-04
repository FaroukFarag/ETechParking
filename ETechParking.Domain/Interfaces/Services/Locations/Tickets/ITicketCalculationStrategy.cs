using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Domain.Interfaces.Services.Locations.Tickets;

public interface ITicketCalculationStrategy
{
    decimal CalculateTotalFare(Ticket ticket);
}
