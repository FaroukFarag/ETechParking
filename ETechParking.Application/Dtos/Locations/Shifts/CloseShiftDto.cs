using ETechParking.Application.Dtos.Abstraction;

namespace ETechParking.Application.Dtos.Locations.Shifts;

public class CloseShiftDto : BaseModelDto<int>
{
    public DateTime EndDateTime { get; set; }
    public decimal TotalCash { get; set; }
    public decimal TotalCredit { get; set; }
}
