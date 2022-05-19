﻿using System.Linq.Expressions;
using AutoMapper;

namespace AutoPocoIO.Repository.Services;

/// <summary>
/// Data acccess and mapping service from <typeparamref name="TEntity"/> to <typeparamref name="TDto"/>
/// </summary>
/// <typeparam name="TEntity">Database model to map from</typeparam>
/// <typeparam name="TDto">Data transfer object to map to</typeparam>
public class RepositoryServiceAsync<TEntity, TDto> : IRepositoryServiceAsync<TEntity, TDto>
    where TDto : IEntityDto
    where TEntity : class
{
    private IRepositoryAsync<TEntity> _repository;
    private IMapper _mapper;

    /// <summary>
    /// Initalizes a new instance of <see cref="RepositoryServiceAsync{TEntity, TDto}"/>
    /// </summary>
    /// <param name="repository">Database access service</param>
    /// <param name="mapper">Mapping configuration</param>
    public RepositoryServiceAsync(IRepositoryAsync<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddAsync(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.AddAsync(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(params object?[]? keyValues)
    {
        var entity = await _repository.GetByIdAsync(keyValues);
        if (entity != null)
            await _repository.DeleteAsync(entity);
    }

    /// <inheritdoc/>
    public IQueryable<TDto> GetAll(Expression<Func<TDto, bool>>? expression = null)
    {
        var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
        return _mapper.ProjectTo<TDto>(_repository.GetAll(predicate));
    }

    /// <inheritdoc/>
    public async Task<TDto?> GetByIdAsync(params object?[]? keyValues)
    {
        var entity = await _repository.GetByIdAsync(keyValues);
        if (entity == null)
            return default;

        return _mapper.Map<TDto>(entity);
    }

    /// <inheritdoc/>
    public async Task<TDto?> GetFirstAsync(Expression<Func<TDto, bool>> expression)
    {
        var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
        var entity = await _repository.GetFirstAsync(predicate);
        if (entity == null)
            return default;

        return _mapper.Map<TDto>(entity);
    }

    /// <inheritdoc/>
    public Task SaveChangesAsync()
    {
        return _repository.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(TDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        await _repository.UpdateAsync(entity);
    }
}
