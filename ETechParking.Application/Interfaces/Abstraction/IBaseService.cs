using ETechParking.Application.Dtos.Abstraction;

namespace ETechParking.Application.Interfaces.Abstraction;

public interface IBaseService<TDto> where TDto : BaseModelDto
{
    Task<TDto> CreateAsync(TDto entityDto);
    Task<TDto> GetAsync(int id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> Update(TDto newEntityDto);
    Task<TDto> Delete(int id);
}
