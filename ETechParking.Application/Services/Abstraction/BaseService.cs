﻿using AutoMapper;
using ETechParking.Application.Dtos.Abstraction;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Abstraction;

namespace ETechParking.Application.Services.Abstraction;

public class BaseService<TEntity, TEntityDto, TPrimaryKey>(
    IBaseRepository<TEntity, TPrimaryKey> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IBaseService<TEntity, TEntityDto, TPrimaryKey>
    where TEntity : BaseModel<TPrimaryKey>
    where TEntityDto : BaseModelDto<TPrimaryKey>
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

    public virtual async Task<IEnumerable<TEntityDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
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
}
