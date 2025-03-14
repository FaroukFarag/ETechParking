namespace ETechParking.Domain.Interfaces.Services.Locations.Users;

public interface IUserContextService
{
    bool IsAuthenticated();
    bool IsAdmin();
    int? GetLocationId();
}
