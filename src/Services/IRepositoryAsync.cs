using System.Linq.Expressions;

namespace AutoPocoIO.Repository.Services;

public interface IRepositoryAsync<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// <para>
    ///         Begins tracking the given entity, and any other reachable entities that are
    ///         not already being tracked, in the <see cref="F:Microsoft.EntityFrameworkCore.EntityState.Added" /> state such that they will
    ///         be inserted into the database when <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" /> is called.
    ///     </para>
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>
    ///     A task that represents the asynchronous Add operation. The task result contains the
    ///     <see cref="T:Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1" /> for the entity. The entry provides access to change tracking
    ///     information and operations for the entity.
    ///     </returns>
    Task AddAsync(TEntity entity);
    /// <summary>
    ///   Begins tracking the given entity in the <see cref="F:Microsoft.EntityFrameworkCore.EntityState.Deleted" /> state such that it will
    ///     be removed from the database when <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" /> is called.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    /// <returns>
    ///     A task that represents the asynchronous Delete operation. The task result contains the
    ///     <see cref="T:Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry`1" /> for the entity. The entry provides access to change tracking
    ///     information and operations for the entity.
    ///     </returns>
    Task DeleteAsync(TEntity entity);
    /// <summary>
    /// <para>
    ///  Creates a <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query instances of <typeparamref name="TEntity" />.
    /// </para>
    /// <para>  
    /// The change tracker  will not track any of the entities that are return from a LINQ query.
    /// If the entity instances are modified, this will not be detected by the changed tracker
    /// and <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" />
    /// will not persist those changes to the database.
    /// </para>
    /// </summary>
    /// <param name="expression">Fitler the sequence of values based on a predicate</param>
    /// <returns>A <see cref="T:System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified by predicate.</returns>
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null);
    /// <summary>
    ///     Finds an entity with the given primary key values. If an entity with the given primary key values
    ///     is being tracked by the context, then it is returned immediately without making a request to the
    ///     database. Otherwise, a query is made to the database for an entity with the given primary key values
    ///     and this entity, if found, is attached to the context and returned. If no entity is found, then
    ///     null is returned.
    /// </summary>
    /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
    /// <returns>The entity found, or <see langword="null" />.</returns>
    Task<TEntity?> GetByIdAsync(params object?[]? keyValues);
    Task UpdateAsync(TEntity entity);
    /// <summary>
    /// Asynchronously returns the first element of a sequence that satisfies a specified condition of default value if no such
    /// element is found.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="default" />(<typeparamref name="TEntity"/>)
    /// if source is empty or if no element passes the test specified by <paramref name="expression"/>;
    /// otherwise, the first element in source that passes the test specified by <paramref name="expression"/></returns>
    /// <exception cref="ArgumentNullException"/>
    Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> expression);
    /// <summary>
    ///     <para>
    ///         Creates a <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <typeparamref name="TEntity" />.
    ///     </para>
    ///     <para>
    ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
    ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
    ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
    ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-query">Querying data with EF Core</see> and
    ///     <see href="https://aka.ms/efcore-docs-change-tracking">Changing tracking</see> for more information.
    /// </remarks>
    /// <typeparam name="TEntity">The type of entity for which a set should be returned.</typeparam>
    /// <returns>A set for the given entity type.</returns>
    IQueryable<TEntity> Entities { get; }
}
