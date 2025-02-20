using ETechParking.Application.Dtos.Abstraction;
using ETechParking.Domain.Enums.Locations.Tickets;

namespace ETechParking.Application.Dtos.Locations.Tickets
{
    public class TicketDto : BaseModelDto<int>
    {
        public Guid? TicketNumber { get; } = Guid.NewGuid();
        public string PlateNumber { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime EntryDateTime { get; set; } = default!;
        public DateTime? ExitDateTime { get; set; }
        public ClientType ClientType { get; set; }
        public TransactionType? TransactionType { get; set; }
        public bool IsPaid { get; set; }
        public int? NumberOfDays { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalWithoutVat { get; set; }
        public decimal? TotalWithVat { get; set; }
        public decimal? Vat { get; set; }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public int CreateUserId { get; set; }
        public string? CreateUserName { get; set; }
        public int? CloseUserId { get; set; }
        public string? CloseUserName { get; set; }
        public int ShiftId { get; set; }

        public string QrCode { get; set; } = default!;
    }
}
