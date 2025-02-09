using ETechParking.Domain.Interfaces.Repositories.Locations.Users;
using ETechParking.Domain.Models.Locations.Users;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Repositories.Abstraction;

namespace ETechParking.Infrastructure.Data.Repositories.Locations.Users;

public class UserRepository(ETechParkingDbContext context) : BaseRepository<User, int>(context), IUserRepository
{
}
