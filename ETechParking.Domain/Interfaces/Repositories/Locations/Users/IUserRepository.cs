using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Locations.Users;

namespace ETechParking.Domain.Interfaces.Repositories.Locations.Users;

public interface IUserRepository : IBaseRepository<User, int>
{
}