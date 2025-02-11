using AutoMapper;
using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Tickets;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Domain.Enums.Locations.Tickets;
using ETechParking.Domain.Interfaces.Repositories.Locations;
using ETechParking.Domain.Interfaces.Repositories.Locations.Tickets;
using ETechParking.Domain.Interfaces.Services.Locations.Tickets;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Domain.Models.Shared;
using ETechParking.Domain.Services.Locations.Tickets;
using Microsoft.EntityFrameworkCore;

namespace ETechParking.Application.Services.Locations.Tickets;

public class TicketService(
    ITicketRepository ticketRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILocationRepository locationRepository)
    : BaseService<Ticket, TicketDto, int>(ticketRepository, unitOfWork, mapper), ITicketService
{
    private readonly ITicketRepository _ticketRepository = ticketRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILocationRepository _locationRepository = locationRepository;

    public async override Task<TicketDto> CreateAsync(TicketDto ticketDto)
    {
        var existingTickets = await _ticketRepository.GetAllAsync(
            filter: t => t.PlateNumber == ticketDto.PlateNumber && !t.IsPaid);

        if (existingTickets.Any())
        {
            throw new InvalidOperationException("An unpaid ticket already exists for this plate number.");
        }

        await base.CreateAsync(ticketDto);

        var location = await _locationRepository.GetAsync(ticketDto.LocationId);

        ticketDto.LocationName = location.Name;

        return ticketDto;
    }

    public async override Task<IEnumerable<TicketDto>> GetAllAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync(includeProperties: t => t.Location);
        var ticketsDtos = _mapper.Map<IReadOnlyList<TicketDto>>(tickets);

        return ticketsDtos;
    }

    public async override Task<IEnumerable<TicketDto>> GetAllPaginatedAsync(PaginatedModelDto paginatedModelDto)
    {
        var paginatedModel = _mapper.Map<PaginatedModel>(paginatedModelDto);
        var tickets = await _ticketRepository.GetAllPaginatedAsync(
            paginatedModel: paginatedModel,
            includeProperties: t => t.Location);
        var ticketsDtos = _mapper.Map<IReadOnlyList<TicketDto>>(tickets);

        return ticketsDtos;
    }

    public async Task<TicketDto> CalculateTicketTotal(CalculateTicketTotalDto ticketTotalDto)
    {
        var ticket = await GetLatestUnpaidTicketAsync(ticketTotalDto.PlateNumber);

        if (ticketTotalDto.ExitDateTime < ticket.EntryDateTime)
        {
            throw new ArgumentException("Exit date/time cannot be earlier than entry date/time.");
        }

        ticket.ExitDateTime = ticketTotalDto.ExitDateTime;

        _ticketRepository.Update(ticket);

        await _unitOfWork.Complete();

        return await CalculateAndMapTicketTotalAsync(ticket);
    }

    public async Task<TicketDto> PayTicket(PayTicketDto payTicketDto)
    {
        var ticket = await GetLatestUnpaidTicketAsync(payTicketDto.PlateNumber);

        ticket.IsPaid = true;
        ticket.TransactionType = payTicketDto.TransactionType;

        _ticketRepository.Update(ticket);

        return await CalculateAndMapTicketTotalAsync(ticket);
    }

    private async Task<Ticket> GetLatestUnpaidTicketAsync(string plateNumber)
    {
        var tickets = await _ticketRepository.GetAllAsync(t => t.PlateNumber == plateNumber && !t.IsPaid);
        var ticket = tickets.LastOrDefault();

        if (ticket == null)
        {
            throw new InvalidOperationException("No unpaid ticket found for the provided plate number.");
        }

        return ticket;
    }

    private async Task<TicketDto> CalculateAndMapTicketTotalAsync(Ticket ticket)
    {
        ticket.Location = await _locationRepository.GetAsync(
            ticket.LocationId,
            query => query.Include(l => l.Fares));

        var totalWithoutVat = CalculateTotal(ticket);
        var totalWithVat = CalculateTotal(ticket, true);

        var ticketDto = _mapper.Map<TicketDto>(ticket);

        ticketDto.TotalWithoutVat = totalWithoutVat;
        ticketDto.TotalWithVat = totalWithVat;
        ticketDto.Vat = totalWithVat - totalWithoutVat;

        return ticketDto;
    }

    private static decimal CalculateTotal(Ticket ticket, bool includeVat = false)
    {
        ITicketCalculationStrategy ticketStrategy = ticket.ClientType == ClientType.Visitor
            ? new VisitorTicketCalculationStrategy()
            : new GuestTicketCalculationStrategy();

        decimal totalFare = ticketStrategy.CalculateTotalFare(ticket);

        return includeVat ? totalFare : totalFare - (totalFare / 1.15m) * 0.15m;
    }
}