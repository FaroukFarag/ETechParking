namespace ETechParking.Application.Dtos.Locations.Tickets;

public class CalculateTicketTotalDto
{
    public string PlateNumber { get; set; } = default!;
    public DateTime ExitDateTime { get; set; } = default!;
}
