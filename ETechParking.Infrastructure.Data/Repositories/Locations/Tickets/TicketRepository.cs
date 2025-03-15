using ETechParking.Domain.Interfaces.Repositories.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Repositories.Abstraction;

namespace ETechParking.Infrastructure.Data.Repositories.Locations.Tickets;

public class TicketRepository(ETechParkingDbContext context) : BaseRepository<Ticket, int>(context), ITicketRepository
{
    public async Task<IEnumerable<TicketStatistics>> GetTransactionTypeStatisticsAsync<TFilterDto>(TFilterDto filter)
    {

        var filteredTickets = await GetAllFilteredAsync(filter);

        var statistics = filteredTickets
            .Where(t => t.TransactionType.HasValue)
            .GroupBy(t => t.TransactionType)
            .Select(g => new TicketStatistics
            {
                Type = g.Key.ToString()!,
                Value = g.Count()
            })
            .ToList();

        return statistics;
    }

    public async Task<IEnumerable<TicketStatistics>> GetClientTypeStatisticsAsync<TFilterDto>(TFilterDto filter)
    {

        var filteredTickets = await GetAllFilteredAsync(filter);

        var statistics = filteredTickets
            .GroupBy(t => t.ClientType)
            .Select(g => new TicketStatistics
            {
                Type = g.Key.ToString()!,
                Value = g.Count()
            })
            .ToList();

        return statistics;
    }
}
