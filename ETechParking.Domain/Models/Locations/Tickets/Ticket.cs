using ETechParking.Domain.Enums.Locations.Tickets;
using ETechParking.Domain.Models.Abstraction;

namespace ETechParking.Domain.Models.Locations.Tickets;

public class Ticket : BaseModel<int>
{
    public string TicketNumber { get; set; } = default!;
    public string PlateNumber { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime EntryDateTime { get; set; } = default!;
    public DateTime ExitDateTime { get; set; } = default!;
    public ClientType ClientType { get; set; }
    public TransactionType TransactionType { get; set; }
    public bool IsPaid { get; set; }
    public int LocationId { get; set; }

    public virtual Location Location { get; set; } = default!;
}
