using ETechParking.Domain.Models.Abstraction;
using ETechParking.Domain.Models.Shared;

namespace ETechParking.Domain.Interfaces.Repositories.Abstraction;

public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : BaseModel<TPrimaryKey>
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> GetAsync(TPrimaryKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllPaginatedAsync(PaginatedModel paginatedModel);
    TEntity Update(TEntity newEntity);
    TEntity Delete(TPrimaryKey id);
    void DeleteRange(IEnumerable<TEntity> entities);
}
