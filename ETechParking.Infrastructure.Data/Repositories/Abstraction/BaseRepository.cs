using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Shared;
using ETechParking.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ETechParking.Infrastructure.Data.Repositories.Abstraction;

public class BaseRepository<TEntity, TPrimaryKey>(ETechParkingDbContext context) : IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
{
    private readonly ETechParkingDbContext _context = context;

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);

        return entity;
    }

    public virtual async Task<TEntity> GetAsync(
        TPrimaryKey id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include = default!)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (include != null)
            query = include(query);

        return (await query.FirstOrDefaultAsync(e => EF.Property<TPrimaryKey>(e, "Id")!.Equals(id)))!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter = default!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default!,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = BuildQuery(filter, orderBy, includeProperties);

        return await query.ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllPaginatedAsync(
        PaginatedModel paginatedModel,
        Expression<Func<TEntity, bool>> filter = default!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default!,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        paginatedModel.PageNumber = paginatedModel.PageNumber <= 0 ? 1 : paginatedModel.PageNumber;

        var query = BuildQuery(filter, orderBy, includeProperties);

        return await query
            .Skip((paginatedModel.PageNumber - 1) * paginatedModel.PageSize)
            .Take(paginatedModel.PageSize)
            .ToListAsync();
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

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        _context.RemoveRange(entities);
    }

    private IQueryable<TEntity> BuildQuery(
    Expression<Func<TEntity, bool>> filter = default!,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default!,
    params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

        if (orderBy != null)
            query = orderBy(query);

        return query;
    }
}
