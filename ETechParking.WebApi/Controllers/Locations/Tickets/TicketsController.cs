using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Application.Interfaces.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Tickets;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Supervisor,Accountant,Cashier")]
public class TicketsController(ITicketService ticketService) :
    BaseController<ITicketService, Ticket, TicketDto, int>(ticketService)
{
    private readonly ITicketService _ticketService = ticketService;

    public override async Task<IActionResult> Create(TicketDto ticketDto)
    {
        var result = await _ticketService.CreateAsync(ticketDto);

        if (result is null)
            return BadRequest("Plate number has opened ticket!");

        return Ok(result);
    }

    [HttpGet("GetByPlateNumber")]
    public virtual async Task<IActionResult> GetByPlateNumber(string plateNumber)
    {
        var dto = await _ticketService.GetByPlateNumberAsync(plateNumber);

        if (dto == null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost("GetAllFiltered")]
    public async Task<IActionResult> GetAllFiltered(TicketFilterDto ticketFilterDto)
    {
        return Ok(await _ticketService.GetAllFilteredAsync(ticketFilterDto));
    }

    [HttpPost("CalculateTicketTotal")]
    public async Task<IActionResult> CalculateTicketTotal(CalculateTicketTotalDto ticketTotalDto)
    {
        var result = await _ticketService.CalculateTicketTotal(ticketTotalDto);

        if (result is null)
            return BadRequest("Invalid Date!");

        return Ok(result);
    }

    [HttpPost("PayTicket")]
    public async Task<IActionResult> PayTicket(PayTicketDto payTicketDto)
    {
        return Ok(await _ticketService.PayTicket(payTicketDto));
    }

    [HttpGet("GetTotalTickets")]
    public async Task<IActionResult> GetTicketsCount(bool isPaid)
    {
        var count = await _ticketService.GetTicketCountAsync(isPaid);

        return Ok(count);
    }
}
