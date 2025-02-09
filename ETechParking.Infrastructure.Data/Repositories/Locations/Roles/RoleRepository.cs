using ETechParking.Domain.Interfaces.Repositories.Locations.Roles;
using ETechParking.Domain.Models.Locations.Roles;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Repositories.Abstraction;

namespace ETechParking.Infrastructure.Data.Repositories.Locations.Roles;

public class RoleRepository(ETechParkingDbContext context) : BaseRepository<Role, int>(context), IRoleRepository
{
}
