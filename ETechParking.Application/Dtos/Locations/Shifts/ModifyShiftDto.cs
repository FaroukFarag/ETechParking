using ETechParking.Application.Dtos.Abstraction;

namespace ETechParking.Application.Dtos.Locations.Shifts;

public abstract class ModifyShiftDto : BaseModelDto<int>
{
    public decimal TotalCash { get; set; }
    public decimal TotalCredit { get; set; }
}
