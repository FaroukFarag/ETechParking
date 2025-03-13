using ETechParking.Domain.Models.Shared;
using System.Linq.Expressions;

namespace ETechParking.Domain.Interfaces.Repositories.Abstraction;

public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> GetAsync(
        TPrimaryKey id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> query = default!);
    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter = default!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default!,
        params Expression<Func<TEntity, object>>[] includeProperties);
    Task<IEnumerable<TEntity>> GetAllPaginatedAsync(
        PaginatedModel paginatedModel,
        Expression<Func<TEntity, bool>> filter = default!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default!,
        params Expression<Func<TEntity, object>>[] includeProperties);
    Task<IEnumerable<TEntity>> GetAllFilteredAsync<TFilterDto>(
        TFilterDto filterDto,
        Expression<Func<TEntity, bool>> filter = default!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default!,
        params Expression<Func<TEntity, object>>[] includeProperties);
    TEntity Update(TEntity newEntity);
    TEntity Delete(TPrimaryKey id);
    void DeleteRange(IEnumerable<TEntity> entities);

    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = default!);

    Task<decimal> GetSumAsync(
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>> filter = default!);

    Task<decimal> GetAverageAsync(
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>> filter = default!);

    Task<TResult> GetMaxAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> filter = default!);

    Task<TResult> GetMinAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> filter = default!);
}
