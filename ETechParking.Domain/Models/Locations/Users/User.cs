﻿using ETechParking.Domain.Models.Locations.Roles;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Tickets;
using Microsoft.AspNetCore.Identity;

namespace ETechParking.Domain.Models.Locations.Users;

public class User : IdentityUser<int>
{
    public bool IsFirstLogin { get; set; }
    public int RoleId { get; set; }
    public int LocationId { get; set; }

    public virtual Role Role { get; set; } = default!;
    public virtual Location Location { get; set; } = default!;
    public virtual IEnumerable<Shift> CashierShifts { get; set; } = default!;
    public virtual IEnumerable<Shift> AccountantShifts { get; set; } = default!;
    public virtual IEnumerable<Ticket> CreatedTickets { get; set; } = default!;
    public virtual IEnumerable<Ticket> ClosedTickets { get; set; } = default!;
}
