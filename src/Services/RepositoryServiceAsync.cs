using System.Linq.Expressions;
using AutoMapper;

namespace AutoPocoIO.Repository.Services;
public class RepositoryServiceAsync<TEntiy, TDto> : IRepositoryServiceAsync<TEntiy, TDto>
    where TDto : IEntityDto
    where TEntiy : IEntity
{
    private IRepositoryAsync<TEntiy> _repository;
    private IMapper _mapper;

    public RepositoryServiceAsync(IRepositoryAsync<TEntiy> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddAsync(TDto dto)
    {
        var entity = _mapper.Map<TEntiy>(dto);
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
        var predicate = _mapper.Map<Expression<Func<TEntiy, bool>>>(expression);
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
        var predicate = _mapper.Map<Expression<Func<TEntiy, bool>>>(expression);
        var entity = await _repository.GetFirstAsync(predicate);
        if (entity == null)
            return default;

        return _mapper.Map<TDto>(entity);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(TDto entityDto)
    {
        var entity = _mapper.Map<TEntiy>(entityDto);
        await _repository.UpdateAsync(entity);
    }
}
