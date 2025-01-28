using ETechParking.Domain.Models.Abstraction;

namespace ETechParking.Domain.Interfaces.Repositories.Abstraction;

public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : BaseModel<TPrimaryKey>
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> GetAsync(TPrimaryKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    TEntity Update(TEntity newEntity);
    TEntity Delete(TPrimaryKey id);
}
