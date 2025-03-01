namespace ETechParking.Application.Dtos.Locations.Users;

public class LoggedInDto
{
    public bool IsFirstLogin { get; set; }
    public DateTime? StartDateTime { get; set; }
    public int UserId { get; set; }
    public int LocationId { get; set; }
    public int? ShiftId { get; set; }
    public string RoleName { get; set; } = default!;

    public string Token { get; set; } = default!;
}
