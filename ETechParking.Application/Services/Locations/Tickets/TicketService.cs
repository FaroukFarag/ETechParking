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
using Microsoft.EntityFrameworkCore;

namespace ETechParking.Application.Services.Locations.Tickets;

public class TicketService(ITicketRepository ticketRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<Ticket, TicketDto, int>(ticketRepository, unitOfWork, mapper), ITicketService
{
    private readonly ITicketRepository _ticketRepository = ticketRepository;
    private const decimal VatRate = 0.15m;

    public override async Task<TicketDto> Update(TicketDto newEntityDto)
    {
        var updatedTicketDto = await base.Update(newEntityDto);
        var updatedTicket = await _ticketRepository.GetAsync(
            updatedTicketDto.Id,
            query => query.Include(t => t.Location).ThenInclude(l => l.Fares));

        if (updatedTicket.ExitDateTime is not null &&
            updatedTicket.ExitDateTime > updatedTicket.EntryDateTime)
        {
            updatedTicketDto.TotalWithoutVat = CalculateTotal(updatedTicket);
            updatedTicketDto.TotalWithVat = CalculateTotal(updatedTicket, true);
            updatedTicketDto.Vat = updatedTicketDto.TotalWithVat - updatedTicketDto.TotalWithoutVat;
        }

        return updatedTicketDto;
    }

    public decimal CalculateTotal(Ticket ticket, bool includeVat = false)
    {
        ITicketCalculationStrategy ticketStrategy = ticket.ClientType == ClientType.Visitor
            ? new VisitorTicketCalculationStrategy()
            : new GuestTicketCalculationStrategy();

        decimal totalFare = ticketStrategy.CalculateTotalFare(ticket);

        return includeVat ? totalFare : totalFare - (totalFare / 1.15m) * 0.15m;
    }
}