using ETechParking.Application.Dtos.Abstraction;

namespace ETechParking.Application.Interfaces.Abstraction;

public interface IBaseService<TEntityDto, TPrimaryKey> where TEntityDto : BaseModelDto<TPrimaryKey>
{
    Task<TEntityDto> CreateAsync(TEntityDto entityDto);
    Task<TEntityDto> GetAsync(TPrimaryKey id);
    Task<IEnumerable<TEntityDto>> GetAllAsync();
    Task<TEntityDto> Update(TEntityDto newEntityDto);
    Task<TEntityDto> Delete(TPrimaryKey id);
}
