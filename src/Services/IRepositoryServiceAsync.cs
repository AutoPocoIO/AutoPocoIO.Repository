using System.Linq.Expressions;

namespace AutoPocoIO.Repository.Services;
/// <summary>
/// Data acccess and mapping service from <typeparamref name="TEntity"/> to <typeparamref name="TDto"/>
/// </summary>
/// <typeparam name="TEntity">Database model to map from</typeparam>
/// <typeparam name="TDto">Data transfer object to map to</typeparam>
public interface IRepositoryServiceAsync<TEntity, TDto>
{
    /// <summary>
    /// <para>Maps the data transfer object to <typeparamref name="TEntity"/> then begins tracking the entity.</para>
    /// <para>Then sets the entity state to <see cref="F:Microsoft.EntityFrameworkCore.EntityState.Added" /> and will be inserted into the database when
    /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" /> is called.</para>
    /// </summary>
    /// <param name="dto">The data transfer object to add.</param>
    /// <returns>
    ///     A task that represents the asynchronous Add operation. The task result contains the
    ///     <see cref="T:Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1" /> for the entity. The entry provides access to change tracking
    ///     information and operations for the entity.
    ///     </returns>
    Task AddAsync(TDto dto);
    /// <summary>
    /// Finds an entity with the given primary key values. Then sets the entity state to <see cref="F:Microsoft.EntityFrameworkCore.EntityState.Deleted" />. If no entity is found, then nothing is deleted.
    /// </summary>
    ///<param name="keyValues">The values of the primary key for the entity to be found.</param>
    /// <returns>A task that represets the asychronous Delete operation.</returns>
    Task DeleteAsync(params object?[]? keyValues);
    /// <summary>
    /// Creates a <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query instances of <typeparamref name="TDto" />.
    /// </summary>
    /// <param name="expression">Fitler the sequence of values based on a predicate</param>
    /// <returns>A <see cref="T:System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified by predicate.</returns>
    IQueryable<TDto> GetAll(Expression<Func<TDto, bool>>? expression = null);
    /// <summary>
    ///     Finds an entity with the given primary key values. If an entity with the given primary key values
    ///     is being tracked by the context, then it is returned immediately without making a request to the
    ///     database. Otherwise, a query is made to the database for an entity with the given primary key values
    ///     and this entity, if found, is attached to the context and returned. If no entity is found, then
    ///     null is returned.
    /// </summary>
    /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
    /// <returns>The entity found, or <see langword="null" />.</returns>
    Task<TDto?> GetByIdAsync(params object?[]? keyValues);
    /// <summary>
    /// Maps to an instance of <typeparamref name="TEntity"/>, then begins tracking the given entity, and any other reachable entities that are
    ///         not already being tracked, in the <see cref="F:Microsoft.EntityFrameworkCore.EntityState.Modified" /> state such that they will
    ///         be updated into the database when <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" /> is called.
    /// </summary>
    /// <param name="entityDto">The data transfer object to map from.</param>
    /// <returns>A successfully completed task.</returns>
    Task UpdateAsync(TDto entityDto);
    /// <summary>
    /// Asynchronously returns the first element of a sequence that satisfies a specified condition of default value if no such
    /// element is found.
    /// </summary>
    /// <param name="expression">Filter the sequence of values based on a predicate</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="default" />(<typeparamref name="TDto"/>)
    /// if source is empty or if no element passes the test specified by <paramref name="expression"/>;
    /// otherwise, the first element in source that passes the test specified by <paramref name="expression"/></returns>
    /// <exception cref="ArgumentNullException"/>
    Task<TDto?> GetFirstAsync(Expression<Func<TDto, bool>> expression);
}
