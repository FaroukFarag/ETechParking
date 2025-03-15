using ETechParking.Application.Dtos.Shared.Filters;
using ETechParking.Common.Interfaces.Filters;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Domain.Shared.Attributs;

namespace ETechParking.Application.Dtos.Locations.Tickets;

public class TicketFilterDto : BaseFilterDto, IFilterDto
{
    [FilterProperty(nameof(Ticket.EntryDateTime))]
    public DateTime? FromDateTime { get; set; }
    [FilterProperty(nameof(Ticket.EntryDateTime))]
    public DateTime? ToDateTime { get; set; }
    public int? CreateUserId { get; set; }
    public int? CloseUserId { get; set; }
    public int? ShiftId { get; set; }
}
