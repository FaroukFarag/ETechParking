using ETechParking.Domain.Interfaces.Repositories.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Repositories.Abstraction;

namespace ETechParking.Infrastructure.Data.Repositories.Locations.Tickets;

public class TicketRepository(ETechParkingDbContext context) : BaseRepository<Ticket, int>(context), ITicketRepository
{
}
