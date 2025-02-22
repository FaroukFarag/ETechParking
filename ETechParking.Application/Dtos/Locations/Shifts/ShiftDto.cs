using ETechParking.Application.Dtos.Abstraction;
using ETechParking.Domain.Enums.Locations.Shifts;

namespace ETechParking.Application.Dtos.Locations.Shifts;

public class ShiftDto : BaseModelDto<int>
{
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public decimal? CashierTotalCash { get; set; }
    public decimal? AccountantTotalCash { get; set; }
    public decimal? TotalCashCaculated { get; set; }
    public decimal? CashierTotalCashDifference { get; set; }
    public decimal? CashierTotalCreditDifference { get; set; }
    public decimal? CashierTotalCredit { get; set; }
    public decimal? AccountantTotalCredit { get; set; }
    public decimal? TotalCreditCaculated { get; set; }
    public decimal? AccountantTotalCashDifference { get; set; }
    public decimal? AccountantTotalCreditDifference { get; set; }
    public decimal? TotalVisitors { get; set; }
    public decimal? TotalGuests { get; set; }
    public ShiftStatus Status { get; set; } = ShiftStatus.Opened;
    public int LocationId { get; set; }
    public string LocationName { get; set; } = default!;
    public int UserId { get; set; }
    public string CashierUserName { get; set; } = default!;
    public string AccountantUserName { get; set; } = default!;
}
