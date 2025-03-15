using ETechParking.Application.Dtos.Shared.Filters;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Domain.Shared.Attributs;

namespace ETechParking.Application.Dtos.Locations.Tickets;

public class TicketDashboardFilterDto : BaseFilterDto
{
    [FilterProperty(nameof(Ticket.EntryDateTime))]
    public DateTime? Day { get; set; }
    public bool? IsPaid { get; set; }
}
