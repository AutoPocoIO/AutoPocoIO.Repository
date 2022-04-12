using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AutoPocoIO.Repository.Services
{
    public class RepositoryFactory<TContext> : IRepositoryFactory<TContext> where TContext : DbContext
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;
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

