using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Abstraction;
using ETechParking.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ETechParking.Infrastructure.Data.Repositories.Abstraction;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
{
    private readonly ETechParkingDbContext _context;

    public BaseRepository(ETechParkingDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);

        return entity;
    }

    public virtual async Task<T> GetAsync(int id)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);

        return entity!;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual T Update(T newEntity)
    {
        _context.Set<T>().Update(newEntity);

        return newEntity;
    }

    public virtual T Delete(int id)
    {
        T entity = _context.Set<T>().Where(t => t.Id == id)?.FirstOrDefault()!;

        if (entity == null)
            return default!;

        _context.Remove(entity);

        return entity;
    }
}
