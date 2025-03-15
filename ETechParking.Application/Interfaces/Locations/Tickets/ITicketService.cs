using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Application.Interfaces.Locations.Tickets;

public interface ITicketService : IBaseService<Ticket, TicketDto, int>
{
    Task<TicketDto> GetByPlateNumberAsync(string plateNumber);
    Task<IEnumerable<TicketDto>> GetAllByShiftIdAsync(int shiftId);
    Task<TicketDto> CalculateTicketTotal(CalculateTicketTotalDto ticketTotalDto);
    Task<TicketDto> PayTicket(PayTicketDto payTicketDto);
    Task<long> GetTicketCountAsync(TicketDashboardFilterDto ticketDashboardFilterDto);
    Task<IEnumerable<TicketTransactionTypeDto>> GetTransactionTypeStatisticsAsync(TicketDashboardFilterDto ticketDashboardFilterDto);
    Task<IEnumerable<TicketTransactionTypeDto>> GetClientTypeStatisticsAsync(TicketDashboardFilterDto ticketDashboardFilterDto);
}
