namespace ETechParking.Reporting.Dtos;

public class ShiftReportFilterDto : BaseReportDto
{
    public DateTime? FromDateTime { get; set; }
    public DateTime? ToDateTime { get; set; }
    public int? LocationId { get; set; }
    public int? CashierUserId { get; set; }
    public int? AccountantUserId { get; set; }
}
