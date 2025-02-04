using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Application.Interfaces.Locations.Tickets;

public interface ITicketService : IBaseService<Ticket, TicketDto, int>
{
    decimal CalculateTotal(Ticket ticket, bool includeVat = true);
}
