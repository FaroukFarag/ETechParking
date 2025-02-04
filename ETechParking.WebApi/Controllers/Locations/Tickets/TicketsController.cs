using ETechParking.Application.Dtos.Locations.Tickets;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Locations.Tickets
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController(ITicketService ticketService) : ControllerBase
    {
        private readonly ITicketService _ticketService = ticketService;

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TicketDto ticketDto)
        {
            return Ok(await _ticketService.CreateAsync(ticketDto));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var ticketDto = await _ticketService.GetAsync(id);

            if (ticketDto == null)
                return NotFound();

            return Ok(ticketDto);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ticketService.GetAllAsync());
        }

        [HttpPost("GetAllPaginated")]
        public async Task<IActionResult> GetAllPaginated(PaginatedModelDto paginatedModelDto)
        {
            return Ok(await _ticketService.GetAllPaginatedAsync(paginatedModelDto));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(TicketDto newTicketDto)
        {
            return Ok(await _ticketService.Update(newTicketDto));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticketDto = await _ticketService.Delete(id);

            if (ticketDto == null)
                return NotFound();

            return Ok(ticketDto);
        }

        [HttpDelete("DeleteRange")]
        public async Task<IActionResult> DeleteRange(IEnumerable<TicketDto> ticketDtos)
        {
            return Ok(await _ticketService.DeleteRange(ticketDtos));
        }
    }
}
