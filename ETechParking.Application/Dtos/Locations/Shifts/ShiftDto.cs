using ETechParking.Application.Dtos.Abstraction;

namespace ETechParking.Application.Dtos.Locations.Shifts;

public class ShiftDto : BaseModelDto<int>
{
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public decimal? TotalCash { get; set; }
    public decimal? TotalCashDifference { get; set; }
    public decimal? TotalCredit { get; set; }
    public decimal? TotalCreditDifference { get; set; }
    public decimal? TotalVisitors { get; set; }
    public decimal? TotalGuests { get; set; }
    public int LocationId { get; set; }
    public string LocationName { get; set; } = default!;
    public int UserId { get; set; }
    public string UserName { get; set; } = default!;
}
