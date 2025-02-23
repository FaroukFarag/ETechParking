namespace ETechParking.Reporting.Dtos.Tickets;

public class TicketReportFilterDto : BaseReportDto
{
    public DateTime? FromDateTime { get; set; }
    public DateTime? ToDateTime { get; set; }
    public int? LocationId { get; set; }
    public int? CreateUserId { get; set; }
    public int? CloseUserId { get; set; }
}
