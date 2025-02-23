using ETechParking.Common.Interfaces.Filters;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Domain.Shared.Attributs;

namespace ETechParking.Application.Dtos.Locations.Shifts;

public class ShiftFilterDto : IFilterDto
{
    [FilterProperty(nameof(Shift.StartDateTime))]
    public DateTime? FromDateTime { get; set; }
    [FilterProperty(nameof(Shift.StartDateTime))]
    public DateTime? ToDateTime { get; set; }
    public int? LocationId { get; set; }
    public int? CashierUserId { get; set; }
    public int? AccountantUserId { get; set; }
}
