using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Models.Shared;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Shared.Filters;
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
        Func<IQueryable<TEntity>, IQueryable<TEntity>> query = default!)
    {
        IQueryable<TEntity> entities = _context.Set<TEntity>();

        if (query != null)
            entities = query(entities);

        return (await entities.FirstOrDefaultAsync(e => EF.Property<TPrimaryKey>(e, "Id")!.Equals(id)))!;
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

    public virtual async Task<IEnumerable<TEntity>> GetAllFilteredAsync<TFilterDto>(
        TFilterDto filterDto,
        Expression<Func<TEntity, bool>> filter = default!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default!,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        if (filterDto == null)
            throw new ArgumentNullException(nameof(filterDto));

        var predicate = filterDto.ToPredicate<TEntity, TFilterDto>();

        Expression<Func<TEntity, bool>> combinedFilter = filter != null
            ? entity => predicate.Compile().Invoke(entity) && filter.Compile().Invoke(entity)
            : predicate;

        var query = BuildQuery(combinedFilter, orderBy, includeProperties);

        return await query.ToListAsync();
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

    public async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> filter = default!)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        return await query.CountAsync();
    }

    public async Task<long> GetCountAsync<TFilterDto>(
        TFilterDto filterDto,
        Expression<Func<TEntity, bool>> filter = default!)
    {
        var query = BuildFilteredQuery(filterDto, filter);

        return await query.CountAsync();
    }

    public async Task<decimal> GetSumAsync<TFilterDto>(
        TFilterDto filterDto,
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>> filter = default!)
    {
        var query = BuildFilteredQuery(filterDto, filter);
        return await query.SumAsync(selector);
    }

    public async Task<decimal> GetAverageAsync<TFilterDto>(
        TFilterDto filterDto,
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>> filter = default!)
    {
        var query = BuildFilteredQuery(filterDto, filter);
        return await query.AverageAsync(selector);
    }

    public async Task<TResult> GetMaxAsync<TFilterDto, TResult>(
        TFilterDto filterDto,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> filter = default!)
    {
        var query = BuildFilteredQuery(filterDto, filter);
        return await query.MaxAsync(selector);
    }

    public async Task<TResult> GetMinAsync<TFilterDto, TResult>(
        TFilterDto filterDto,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> filter = default!)
    {
        var query = BuildFilteredQuery(filterDto, filter);
        return await query.MinAsync(selector);
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
    private IQueryable<TEntity> BuildFilteredQuery<TFilterDto>(
    TFilterDto filterDto,
    Expression<Func<TEntity, bool>> filter = default!)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (filterDto == null && filter == null)
            return query;

        var predicate = filterDto != null ? filterDto.ToPredicate<TEntity, TFilterDto>() : default!;
        var finalFilter = CombineFilters(predicate, filter);

        return finalFilter != null ? query.Where(finalFilter) : query;
    }

    private Expression<Func<TEntity, bool>> CombineFilters(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, bool>> filter)
    {
        if (predicate == null)
            return filter;

        if (filter == null)
            return predicate;

        return Expression.Lambda<Func<TEntity, bool>>(
            Expression.AndAlso(predicate.Body, Expression.Invoke(filter, predicate.Parameters)),
            predicate.Parameters);
    }
}
