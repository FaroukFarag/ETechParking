﻿using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Infrastructure.Data.Context;

namespace ETechParking.Infrastructure.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ETechParkingDbContext _context;

    public UnitOfWork(ETechParkingDbContext context)
    {
        _context = context;
    }

    public async Task Complete()
    {
        await _context.SaveChangesAsync();
    }
}
