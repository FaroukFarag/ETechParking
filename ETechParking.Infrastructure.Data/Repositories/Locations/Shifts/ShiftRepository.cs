using ETechParking.Domain.Enums.Locations.Shifts;
using ETechParking.Domain.Interfaces.Repositories.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ETechParking.Infrastructure.Data.Repositories.Locations.Shifts;

public class ShiftRepository(ETechParkingDbContext context) : BaseRepository<Shift, int>(context), IShiftRepository
{
    private readonly ETechParkingDbContext _context = context;

    public async Task<Shift?> GetLastUserOpenedShiftAsync(int userId)
    {
        return await _context.Shifts
            .FirstOrDefaultAsync(s => s.CashierUserId == userId && s.Status == ShiftStatus.Opened);
    }
}
