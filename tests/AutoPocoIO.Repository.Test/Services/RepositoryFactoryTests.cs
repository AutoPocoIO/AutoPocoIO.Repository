using System;
using AutoMapper;
using AutoPocoIO.Repository.Services;
using AutoPocoIO.Repository.Test.Services.TestUtilities;
using AutoPocoIO.Repository.Test.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AutoPocoIO.Repository.Test.Services;

public class RepositoryFactoryTests : SqliteTestBase<TestDbContext>
{
    private readonly IMapper _mapper;

    public RepositoryFactoryTests()
    {
        var config = new MapperConfiguration(c => { });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Create_InstanceOfRepository_WithEntity()
    {
        using var context = new TestDbContext(Options);
        RepositoryFactory<TestDbContext> factory = new(context, _mapper);

        var instance1 = factory.CreateRepository<PersonEntity>();
        var instance2 = factory.CreateRepository<PersonEntity>();

        Assert.IsType<RepositoryAsync<PersonEntity>>(instance1);
        Assert.NotNull(instance1.Entities);
        Assert.IsType<RepositoryAsync<PersonEntity>>(instance2);
        Assert.NotSame(instance1, instance2);
    }

    [Fact]
    public void Create_InstanceOfRepositoryService()
    {
        using var context = new TestDbContext(Options);
        RepositoryFactory<TestDbContext> factory = new(context, _mapper);

        var instance1 = factory.CreateRepositoryService<PersonEntity, PersonDto>();
        var instance2 = factory.CreateRepositoryService<PersonEntity, PersonDto>();

        Assert.IsType<RepositoryServiceAsync<PersonEntity, PersonDto>>(instance1);
        Assert.IsType<RepositoryServiceAsync<PersonEntity, PersonDto>>(instance2);
        Assert.NotSame(instance1, instance2);
    }
}

