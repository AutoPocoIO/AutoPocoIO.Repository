using System.Linq.Expressions;

namespace AutoPocoIO.Repository.Services;
public interface IRepositoryServiceAsync<TEntity, TDto>
{
    Task AddAsync(TDto dto);
    Task DeleteAsync(params object?[]? keyValues);
    IQueryable<TDto> GetAll(Expression<Func<TDto, bool>>? expression = null);
    Task<TDto?> GetByIdAsync(params object?[]? keyValues);
    Task UpdateAsync(TDto entityDto);
    Task<TDto?> GetFirstAsync(Expression<Func<TDto, bool>> expression);
}
