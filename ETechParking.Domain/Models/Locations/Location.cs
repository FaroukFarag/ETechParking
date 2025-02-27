using ETechParking.Domain.Models.Abstraction;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Users;

namespace ETechParking.Domain.Models.Locations;

public class Location : BaseModel<int>
{
    public string Name { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }

    public virtual IEnumerable<User> Users { get; set; } = default!;
    public virtual IEnumerable<Fare> Fares { get; set; } = default!;
    public virtual IEnumerable<Ticket> Tickets { get; set; } = default!;
    public virtual IEnumerable<Shift> Shifts { get; set; } = default!;
}
