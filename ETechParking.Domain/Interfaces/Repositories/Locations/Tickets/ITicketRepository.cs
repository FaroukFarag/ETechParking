using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Domain.Interfaces.Repositories.Locations.Tickets;

public interface ITicketRepository : IBaseRepository<Ticket, int>
{
    Task<IEnumerable<TicketStatistics>> GetTransactionTypeStatisticsAsync<TFilterDto>(TFilterDto filter);

    Task<IEnumerable<TicketStatistics>> GetClientTypeStatisticsAsync<TFilterDto>(TFilterDto filter);
}
