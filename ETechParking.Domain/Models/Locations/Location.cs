using ETechParking.Domain.Models.Abstraction;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Locations.Users;

namespace ETechParking.Domain.Models.Locations;

public class Location : BaseModel<int>
{
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;

    public IEnumerable<User> Users { get; set; } = default!;
    public IEnumerable<Fare> Fares { get; set; } = default!;
}
