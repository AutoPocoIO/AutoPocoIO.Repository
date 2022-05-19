using Microsoft.EntityFrameworkCore;

namespace AutoPocoIO.Repository.Services;

/// <summary>
/// Create instances of <see cref="RepositoryAsync{TEntity}"/> and <see cref="RepositoryServiceAsync{TEntity, TDto}"/>
/// with a specific <see cref="DbContext"/>
/// </summary>
/// <typeparam name="TContext"><see cref="DbContext"/> to query</typeparam>
public interface IRepositoryFactory<TContext> where TContext : DbContext
{
    /// <summary>
    /// Create intance of <see cref="IRepositoryAsync{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for which a set should be returned.</typeparam>
    /// <returns>An instance of <see cref="IRepositoryAsync{TEntity}"/></returns>
    IRepositoryAsync<TEntity> CreateRepository<TEntity>() where TEntity : class;
    /// <summary>
    /// Create instance of <see cref="IRepositoryServiceAsync{TEntity, TDto}"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for which a set should be returned.</typeparam>
    /// <typeparam name="TDto">The type of data transfer object to map from <typeparamref name="TEntity"/></typeparam>
    /// <returns>An instance of <see cref="IRepositoryServiceAsync{TEntity, TDto}"/></returns>
    IRepositoryServiceAsync<TEntity, TDto> CreateRepositoryService<TEntity, TDto>()
        where TDto : IEntityDto
        where TEntity : class;
}

