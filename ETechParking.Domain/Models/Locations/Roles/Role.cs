using ETechParking.Domain.Models.Locations.Users;
using Microsoft.AspNetCore.Identity;

namespace ETechParking.Domain.Models.Locations.Roles;

public class Role : IdentityRole<int>
{
    public virtual IEnumerable<User> Users { get; set; } = default!;
}
