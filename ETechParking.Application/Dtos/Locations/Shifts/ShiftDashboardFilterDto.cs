using ETechParking.Application.Dtos.Shared.Filters;
using ETechParking.Domain.Enums.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Domain.Shared.Attributs;

namespace ETechParking.Application.Dtos.Locations.Shifts;

public class ShiftDashboardFilterDto : BaseFilterDto
{
    [FilterProperty(nameof(Shift.StartDateTime))]
    public DateTime? Day { get; set; }
    public ShiftStatus? Status { get; set; }
}
