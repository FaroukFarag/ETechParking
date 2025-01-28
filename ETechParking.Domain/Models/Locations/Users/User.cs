using Microsoft.AspNetCore.Identity;

namespace ETechParking.Domain.Models.Locations.Users;

public class User : IdentityUser
{
    public int LocationId { get; set; }

    public virtual Location Location { get; set; } = default!;
}
