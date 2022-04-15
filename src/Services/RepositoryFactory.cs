using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AutoPocoIO.Repository.Services
{
    /// <summary>
    /// Create instances of <see cref="RepositoryAsync{TEntity}"/> and <see cref="RepositoryServiceAsync{TEntity, TDto}"/>
    /// with a specific <see cref="DbContext"/>
    /// </summary>
    /// <typeparam name="TContext"><see cref="DbContext"/> to query</typeparam>
    public class RepositoryFactory<TContext> : IRepositoryFactory<TContext> where TContext : DbContext
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactory{TContext}"/>
        /// </summary>
        /// <param name="context"><see cref="DbContext"/> to query</param>
        /// <param name="mapper">Mapping configuration</param>
        public RepositoryFactory(TContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public IRepositoryAsync<TEntity> CreateRepository<TEntity>() where TEntity : class, IEntity
        {
            return new RepositoryAsync<TEntity>(_context);
        }

        /// <inheritdoc/>
        public IRepositoryServiceAsync<TEntity, TDto> CreateRepositoryService<TEntity, TDto>()
            where TDto : IEntityDto
            where TEntity : class, IEntity
        {
            var repository = CreateRepository<TEntity>();
            return new RepositoryServiceAsync<TEntity, TDto>(repository, _mapper);
        }
    }
}

