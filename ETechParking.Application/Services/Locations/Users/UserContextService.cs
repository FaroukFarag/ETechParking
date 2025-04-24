using ETechParking.Domain.Interfaces.Services.Locations.Users;
using Microsoft.AspNetCore.Http;

namespace ETechParking.Application.Services.Locations.Users;

public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public bool IsAuthenticated()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        return user?.Identity?.IsAuthenticated ?? false;
    }

    public bool IsAdmin()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        return user?.IsInRole("Admin") ?? false;
    }

    public int? GetLocationId()
    {
        var locationIdClaim = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == "locationId")?.Value;

        if (int.TryParse(locationIdClaim, out int locationId))
        {
            return locationId;
        }

        return default!;
    }
}
