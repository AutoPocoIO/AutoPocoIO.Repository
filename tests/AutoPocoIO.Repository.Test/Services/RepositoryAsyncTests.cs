using AutoPocoIO.Repository.Services;
using AutoPocoIO.Repository.Test.Services.TestUtilities;
using AutoPocoIO.Repository.Test.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AutoPocoIO.Repository.Test.Services;

public class RepositoryAsyncTests : SqliteTestBase<TestDbContext>
{
    public RepositoryAsyncTests()
    {
        using var context = new TestDbContext(Options);
        context.Database.EnsureCreated();

        //Add entities
        context.People.AddRange(new()
        {
            Id = 1,
            Name = "entityName1"
        },
        new()
        {
            Id = 2,
            Name = "entityName2"
        });
        context.SaveChanges();
    }

    [Fact]
    public void Entities_GetDbSet()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);
        var entites = repo.Entities;

        Assert.Equal(context.People, entites);
    }

    [Fact]
    public async Task AddAsync_SetsStateToAdded()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);
        PersonEntity entity = new()
        {
            Id = 3,
            Name = "entityName"
        };

        await repo.AddAsync(entity);

        Assert.Equal(EntityState.Added, context.Entry(entity).State);
    }

    [Fact]
    public async Task DeleteAsync_SetsStateToDeleted()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);

        var entity = context.People.First();
        await repo.DeleteAsync(entity);

        Assert.Equal(EntityState.Deleted, context.Entry(entity).State);
    }

    [Fact]
    public void GetAll_ReturnsAllRecordsWhenNullPredicateExpression()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);
        var results = repo.GetAll();

        Assert.Collection(results,
            result =>
            {
                Assert.Equal(1, result.Id);
                Assert.Equal("entityName1", result.Name);
            },
            result =>
            {
                Assert.Equal(2, result.Id);
                Assert.Equal("entityName2", result.Name);
            });
    }

    [Fact]
    public void GetAll_ReturnsFilteredRecordsMatchingPredicateExpression()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);
        var results = repo.GetAll(c => c.Name == "entityName2");

        Assert.Collection(results,
            result =>
            {
                Assert.Equal(2, result.Id);
                Assert.Equal("entityName2", result.Name);
            });
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNullWhenNotFound()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);
        var results = await repo.GetByIdAsync(3);

        Assert.Null(results);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsEntityWhenFound()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);
        var result = await repo.GetByIdAsync(2);

        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("entityName2", result.Name);
    }

    [Fact]
    public async Task GetFirstAsync_ReturnsNullWhenNotFound()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);
        var results = await repo.GetFirstAsync(c => c.Name == "notFound");

        Assert.Null(results);
    }

    [Fact]
    public async Task GetFirstAsync_ReturnsEntityWhenFound()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);
        var result = await repo.GetFirstAsync(c => c.Name == "entityName2");

        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("entityName2", result.Name);
    }


    [Fact]
    public async Task UpdateAsync_SetsStateToModified()
    {
        using var context = new TestDbContext(Options);
        var repo = new RepositoryAsync<PersonEntity>(context);

        var entity = context.People.First();
        await repo.UpdateAsync(entity);

        Assert.Equal(EntityState.Modified, context.Entry(entity).State);
    }


}