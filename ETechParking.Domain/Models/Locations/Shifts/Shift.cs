using ETechParking.Domain.Models.Abstraction;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Users;

namespace ETechParking.Domain.Models.Locations.Shifts;

public class Shift : BaseModel<int>
{
    public DateTime StartDateTime { get; } = DateTime.Now;
    public DateTime? EndDateTime { get; set; }
    public decimal? TotalCash { get; set; }
    public decimal? TotalCredit { get; set; }
    public int LocationId { get; set; }
    public int UserId { get; set; }

    public virtual Location Location { get; set; } = default!;
    public virtual User User { get; set; } = default!;
    public virtual IEnumerable<Ticket> Tickets { get; set; } = default!;
    public virtual IEnumerable<Shift> Shifts { get; set; } = default!;
}
