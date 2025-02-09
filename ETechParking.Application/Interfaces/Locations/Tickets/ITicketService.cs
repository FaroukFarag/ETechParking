using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Application.Validators.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Application.Interfaces.Locations.Tickets;

public interface ITicketService : IBaseService<Ticket, TicketDto, int>
{
    Task<TicketDto> CalculateTicketTotal(CalculateTicketTotalDto ticketTotalDto);
    Task<TicketDto> PayTicket(PayTicketDto payTicketDto);
}
