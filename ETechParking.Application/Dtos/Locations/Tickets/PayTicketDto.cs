using ETechParking.Domain.Enums.Locations.Tickets;

namespace ETechParking.Application.Dtos.Locations.Tickets;

public class PayTicketDto
{
    public string TicketNumber { get; set; } = default!;
    public TransactionType TransactionType { get; set; }
    public DateTime ExitDateTime { get; set; } = default!;
    public int ShiftId { get; set; }
    public int CloseUserId { get; set; }
}
