using ETechParking.Domain.Models.Abstraction;
using ETechParking.Domain.Models.Locations.Fares;
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

    public IEnumerable<User> Users { get; set; } = default!;
    public IEnumerable<Fare> Fares { get; set; } = default!;
    public IEnumerable<Ticket> Tickets { get; set; } = default!;
}
