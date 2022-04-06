using Microsoft.EntityFrameworkCore;

namespace AutoPocoIO.Repository.Test.Services.TestUtilities;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PersonEntity> People => Set<PersonEntity>();
}



