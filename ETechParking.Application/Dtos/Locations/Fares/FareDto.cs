using ETechParking.Application.Dtos.Abstraction;
using ETechParking.Domain.Enums.Locations.Fares;

namespace ETechParking.Application.Dtos.Locations.Fares;

public class FareDto : BaseModelDto<int>
{
    public decimal Amount { get; set; }
    public FareType FareType { get; set; }
    public int LocationId { get; set; }
}
