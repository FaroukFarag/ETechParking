using ETechParking.Application.Dtos.Abstraction;
using ETechParking.Domain.Enums.Locations.Fares;

namespace ETechParking.Application.Dtos.Locations.Fares;

public class FareDto : BaseModelDto<int>
{
    public decimal Amount { get; set; }
    public FareType FareType { get; set; }
    public string? FareTypeName { get; set; }
    public int EnterGracePeriod { get; set; }
    public int ExitGracePeriod { get; set; }
    public int? MaxLimit { get; set; }
    public int LocationId { get; set; }
    public string? LocationName { get; set; }
}
