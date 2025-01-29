using ETechParking.Application.Dtos.Abstraction;
using ETechParking.Domain.Models.Abstraction;

namespace ETechParking.Application.Interfaces.Abstraction;

public interface IBaseService<TEntity, TEntityDto, TPrimaryKey>
    where TEntity : BaseModel<TPrimaryKey>
    where TEntityDto : BaseModelDto<TPrimaryKey>
{
    Task<TEntityDto> CreateAsync(TEntityDto entityDto);
    Task<TEntityDto> GetAsync(TPrimaryKey id);
    Task<IEnumerable<TEntityDto>> GetAllAsync();
    Task<TEntityDto> Update(TEntityDto newEntityDto);
    Task<TEntityDto> Delete(TPrimaryKey id);
}
