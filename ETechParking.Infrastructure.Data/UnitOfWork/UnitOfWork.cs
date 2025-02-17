using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Infrastructure.Data.Context;

namespace ETechParking.Infrastructure.Data.UnitOfWork;

public class UnitOfWork(ETechParkingDbContext context) : IUnitOfWork
{
    private readonly ETechParkingDbContext _context = context;

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}
