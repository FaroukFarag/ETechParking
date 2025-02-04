using ETechParking.Application.Dtos.Abstraction;
using ETechParking.Domain.Enums.Locations.Tickets;
using System.Text;

namespace ETechParking.Application.Dtos.Locations.Tickets
{
    public class TicketDto : BaseModelDto<int>
    {
        public string TicketNumber { get; set; } = default!;
        public string PlateNumber { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime EntryDateTime { get; set; } = default!;
        public DateTime ExitDateTime { get; set; } = default!;
        public ClientType ClientType { get; set; }
        public TransactionType TransactionType { get; set; }
        public bool IsPaid { get; set; }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public string QrCode => Convert.ToBase64String(Encoding.UTF8.GetBytes($"Id: {Id},TicketNumber: {TicketNumber}, PlateNumber: {PlateNumber}"));
    }
}
