using AutoMapper;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Shared;
using System.Linq.Expressions;

namespace ETechParking.Application.Services.Abstraction;

public class BaseService<TEntity, TEntityDto, TPrimaryKey>(
    IBaseRepository<TEntity, TPrimaryKey> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IBaseService<TEntity, TEntityDto, TPrimaryKey>
    where TEntity : class
    where TEntityDto : class
{
    private readonly IBaseRepository<TEntity, TPrimaryKey> _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public virtual async Task<TEntityDto> CreateAsync(TEntityDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);

        await _repository.CreateAsync(entity);

        await _unitOfWork.Complete();

        return _mapper.Map<TEntityDto>(entity);
    }

    public virtual async Task<TEntityDto> GetAsync(TPrimaryKey id)
    {
        var entity = await _repository.GetAsync(id);
        var entityDto = _mapper.Map<TEntityDto>(entity);

        return entityDto;
    }

    public virtual async Task<IEnumerable<TEntityDto>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter = default!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default!,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var entities = await _repository.GetAllAsync(filter, orderBy, includeProperties);
        var entitiesDtos = _mapper.Map<IReadOnlyList<TEntityDto>>(entities);

        return entitiesDtos;
    }

    public virtual async Task<IEnumerable<TEntityDto>> GetAllPaginatedAsync(
        PaginatedModelDto paginatedModelDto,
        Expression<Func<TEntity, bool>> filter = default!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default!,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var entities = await _repository.GetAllPaginatedAsync(
            paginatedModel: _mapper.Map<PaginatedModel>(paginatedModelDto),
            includeProperties: includeProperties);
        var entitiesDtos = _mapper.Map<IReadOnlyList<TEntityDto>>(entities);

        return entitiesDtos;
    }
    public virtual async Task<TEntityDto> Update(TEntityDto newEntityDto)
    {
        var entity = _mapper.Map<TEntity>(newEntityDto);

        _repository.Update(entity);

        await _unitOfWork.Complete();

        return _mapper.Map<TEntityDto>(entity);
    }

    public virtual async Task<TEntityDto> Delete(TPrimaryKey id)
    {
        var entity = _repository.Delete(id);
        var entityDto = _mapper.Map<TEntityDto>(entity);

        await _unitOfWork.Complete();

        return entityDto;
    }

    public async virtual Task<bool> DeleteRange(IEnumerable<TEntityDto> entitiesDtos)
    {
        var entities = _mapper.Map<IReadOnlyList<TEntity>>(entitiesDtos);

        _repository.DeleteRange(entities);

        return await _unitOfWork.Complete();
    }
}
