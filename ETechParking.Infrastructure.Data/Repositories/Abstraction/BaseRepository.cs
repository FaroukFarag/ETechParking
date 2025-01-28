using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Abstraction;
using ETechParking.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ETechParking.Infrastructure.Data.Repositories.Abstraction;

public abstract class BaseRepository<TEntity, TPrimaryKey>(ETechParkingDbContext context) : IBaseRepository<TEntity, TPrimaryKey> where TEntity : BaseModel<TPrimaryKey>
{
    private readonly ETechParkingDbContext _context = context;

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);

        return entity;
    }

    public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);

        return entity!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual TEntity Update(TEntity newEntity)
    {
        _context.Set<TEntity>().Update(newEntity);

        return newEntity;
    }

    public virtual TEntity Delete(TPrimaryKey id)
    {
        TEntity entity = _context.Set<TEntity>().Find(id)!;

        if (entity == null)
            return default!;

        _context.Remove(entity);

        return entity;
    }
}
