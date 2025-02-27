using ETechParking.Domain.Enums.Locations.Tickets;
using ETechParking.Domain.Models.Abstraction;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Users;

namespace ETechParking.Domain.Models.Locations.Tickets;

public class Ticket : BaseModel<int>
{
    public string TicketNumber { get; set; } = default!;
    public string PlateNumber { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime EntryDateTime { get; set; } = default!;
    public DateTime? ExitDateTime { get; set; }
    public ClientType ClientType { get; set; }
    public TransactionType? TransactionType { get; set; }
    public bool IsPaid { get; set; }
    public int? NumberOfDays { get; set; }
    public decimal? TotalAmount { get; set; }
    public int LocationId { get; set; }
    public int ShiftId { get; set; }
    public int CreateUserId { get; set; }
    public int? CloseUserId { get; set; }

    public virtual Location Location { get; set; } = default!;
    public virtual Shift Shift { get; set; } = default!;
    public virtual User CreateUser { get; set; } = default!;
    public virtual User? CloseUser { get; set; }
}
