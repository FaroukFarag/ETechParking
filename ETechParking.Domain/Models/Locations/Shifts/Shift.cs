using ETechParking.Domain.Enums.Locations.Shifts;
using ETechParking.Domain.Models.Abstraction;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Users;

namespace ETechParking.Domain.Models.Locations.Shifts;

public class Shift : BaseModel<int>
{
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public decimal? CashierTotalCash { get; set; }
    public decimal? CashierTotalCredit { get; set; }
    public decimal? AccountantTotalCash { get; set; }
    public decimal? AccountantTotalCredit { get; set; }
    public ShiftStatus Status { get; set; }
    public int LocationId { get; set; }
    public int CashierUserId { get; set; }
    public int? AccountantUserId { get; set; }

    public virtual Location Location { get; set; } = default!;
    public virtual User CashierUser { get; set; } = default!;
    public virtual User? AccountantUser { get; set; } = default!;
    public virtual IEnumerable<Ticket> Tickets { get; set; } = default!;
}
