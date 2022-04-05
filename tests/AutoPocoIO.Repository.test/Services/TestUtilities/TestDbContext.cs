using Microsoft.EntityFrameworkCore;

namespace AutoPocoIO.Repository.Test.TestUtilities;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PersonEntity> People => Set<PersonEntity>();
}

public class PersonEntity : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}

