using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Abstraction;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User ID not found in claims.");
        }

        return int.Parse(userIdClaim);
    }
}
