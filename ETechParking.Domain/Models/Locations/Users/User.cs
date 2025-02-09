﻿using ETechParking.Domain.Models.Locations.Roles;
using Microsoft.AspNetCore.Identity;

namespace ETechParking.Domain.Models.Locations.Users;

public class User : IdentityUser<int>
{
    public int RoleId { get; set; }
    public int LocationId { get; set; }

    public virtual Role Role { get; set; } = default!;
    public virtual Location Location { get; set; } = default!;
}
