using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AutoPocoIO.Repository.Services;

/// <summary>
/// Data access services to a table represented by <typeparamref name="TEntity"/>
/// </summary>
/// <typeparam name="TEntity">Database model to interact with</typeparam>
public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
{
    private readonly DbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryAsync{TEntity}"/>
    /// </summary>
    /// <param name="context"><see cref="DbContext"/> to query</param>
    public RepositoryAsync(DbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> Entities => _context.Set<TEntity>();

    /// <inheritdoc/>
    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    /// <inheritdoc/>
    public Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null)
    {
        var result = _context.Set<TEntity>().AsNoTracking();
        if (expression == null)
            return result;

        return result.Where(expression);
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(params object?[]? keyValues)
    {
        return await _context.Set<TEntity>().FindAsync(keyValues);
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
    }

    /// <inheritdoc/>
    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public Task UpdateAsync(TEntity entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }
}

