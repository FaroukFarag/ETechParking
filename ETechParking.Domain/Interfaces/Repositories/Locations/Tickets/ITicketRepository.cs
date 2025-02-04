using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Domain.Interfaces.Repositories.Locations.Tickets;

public interface ITicketRepository : IBaseRepository<Ticket, int>
{
}
