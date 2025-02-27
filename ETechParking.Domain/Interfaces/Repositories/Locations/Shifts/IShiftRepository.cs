using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Locations.Shifts;

namespace ETechParking.Domain.Interfaces.Repositories.Locations.Shifts;

public interface IShiftRepository : IBaseRepository<Shift, int>
{
    Task<Shift?> GetLastUserOpenedShiftAsync(int userId);
}
