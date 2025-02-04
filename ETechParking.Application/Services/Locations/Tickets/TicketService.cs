using AutoMapper;
using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Application.Interfaces.Locations.Tickets;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Domain.Enums.Locations.Tickets;
using ETechParking.Domain.Interfaces.Repositories.Locations.Tickets;
using ETechParking.Domain.Interfaces.Services.Locations.Tickets;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Domain.Services.Locations.Tickets;

namespace ETechParking.Application.Services.Locations.Tickets;

public class TicketService(ITicketRepository ticketRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<Ticket, TicketDto, int>(ticketRepository, unitOfWork, mapper), ITicketService
{
    private const decimal VatRate = 0.15m;

    public decimal CalculateTotal(Ticket ticket, bool includeVat = true)
    {
        ITicketCalculationStrategy ticketStrategy = ticket.ClientType == ClientType.Visitor
            ? new VisitorTicketCalculationStrategy()
            : new GuestTicketCalculationStrategy();

        decimal totalFare = ticketStrategy.CalculateTotalFare(ticket);

        return includeVat ? totalFare * (1 + VatRate) : totalFare;
    }
}