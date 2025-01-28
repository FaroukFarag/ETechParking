using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Models.Abstraction;

namespace ETechParking.Domain.Models.Locations.Fares;

public class Fare : BaseModel<int>
{
    public decimal Amount { get; set; }
    public FareType FareType { get; set; }
    public int LocationId { get; set; }

    public Location Location { get; set; } = default!;
}
