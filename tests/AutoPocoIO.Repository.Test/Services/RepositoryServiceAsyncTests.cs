using System.Linq.Expressions;
using AutoMapper;
using AutoPocoIO.Repository.Services;
using AutoPocoIO.Repository.Test.Services.TestUtilities;
using AutoPocoIO.Repository.Test.TestUtilities;
using Moq;
using Xunit;

namespace AutoPocoIO.Repository.Test.Services;
public class RepositoryServiceAsyncTests : SqliteTestBase<TestDbContext>
{
    private readonly Mock<IRepositoryAsync<PersonEntity>> _repository;
    private readonly IMapper _mapper;
    private readonly RepositoryServiceAsync<PersonEntity, PersonDto> _service;
    private PersonDto _dto => new()
    {
        Id = 1,
        Name = "name1"
    };

    private PersonEntity _entity => new()
    {
        Id = 1,
        Name = "name1"
    };

    public RepositoryServiceAsyncTests()
    {
        _repository = new Mock<IRepositoryAsync<PersonEntity>>();

        var config = new MapperConfiguration(c =>
        {
            c.CreateProjection<PersonEntity, PersonDto>();
            c.CreateMap<PersonEntity, PersonDto>().ReverseMap();
        });

        _mapper = config.CreateMapper();


        _service = new(_repository.Object, _mapper);
    }

    [Fact]
    public async Task AddAsync_MapsDtoToEntityThenAdds()
    {
        _repository.Setup(c => c.AddAsync(_entity)).Verifiable();

        await _service.AddAsync(_dto);

        _repository.Verify();
    }

    [Fact]
    public async Task DeleteAsync_DeletesTheEntityIfFound()
    {
        _repository.Setup(c => c.GetByIdAsync(1))
            .ReturnsAsync(_entity);
        _repository.Setup(c => c.DeleteAsync(_entity)).Verifiable();

        await _service.DeleteAsync(1);

        _repository.Verify();
    }

    [Fact]
    public async Task DeleteAsync_SkipsDeletesTheEntityIfNotFound()
    {
        _repository.Setup(c => c.GetByIdAsync(1))
            .ReturnsAsync((PersonEntity?)null);
        _repository.Setup(c => c.DeleteAsync(_entity)).Verifiable();

        await _service.DeleteAsync(1);

        _repository.Verify(c => c.DeleteAsync(It.IsAny<PersonEntity>()), Times.Never);
    }

    [Fact]
    public void GetAll_MapsExpressionAndProjectsEntityToDto()
    {

        var repoList = new PersonEntity[] { _entity }.AsQueryable();
        var projectedList = new PersonDto[] { _dto }.AsQueryable();
        string? predicateMapped = null;

        _repository.Setup(c => c.GetAll(It.IsAny<Expression<Func<PersonEntity, bool>>>()))
            .Callback<Expression<Func<PersonEntity, bool>>>(c => predicateMapped = c.Body.ToString())
            .Returns(repoList);

        var results = _service.GetAll(c => c.Id == 1);

        Assert.Equal("c => (c.Id == 1)", predicateMapped);
        Assert.Equal(projectedList, results);
        _repository.Verify();
    }

    [Fact]
    public async Task GetByIdAsync_MapsEntityToDtoWhenInRepository()
    {
        _repository.Setup(c => c.GetByIdAsync(1))
            .ReturnsAsync(_entity);

        var result = await _service.GetByIdAsync(1);

        Assert.Equal(_dto, result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnNullWhenNotInRepository()
    {
        _repository.Setup(c => c.GetByIdAsync(1))
            .ReturnsAsync((PersonEntity?)null);

        var result = await _service.GetByIdAsync(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByFirstAsync_MapsEntityToDtoWhenInRepository()
    {
        string? predicateMapped = null;
        var context = new TestDbContext(Options);
        context.Database.EnsureCreated();
        context.People.Add(_entity);
        context.SaveChanges();

        _repository.Setup(c => c.GetAll(It.IsAny<Expression<Func<PersonEntity, bool>>>()))
             .Callback<Expression<Func<PersonEntity, bool>>>(c => predicateMapped = c.Body.ToString())
            .Returns(context.People);

        var result = await _service.GetFirstAsync(c => c.Id == 1);

        Assert.Equal("c => (c.Id == 1)", predicateMapped);
        Assert.Equal(_dto, result);
    }

    [Fact]
    public async Task GetByFirstAsync_ReturnsNullWhenNotInRepository()
    {
        string? predicateMapped = null;
        var context = new TestDbContext(Options);
        context.Database.EnsureCreated();

        _repository.Setup(c => c.GetAll(It.IsAny<Expression<Func<PersonEntity, bool>>>()))
             .Callback<Expression<Func<PersonEntity, bool>>>(c => predicateMapped = c.Body.ToString())
            .Returns(context.People);


        var result = await _service.GetFirstAsync(c => c.Id == 1);

        Assert.Equal("c => (c.Id == 1)", predicateMapped);
        Assert.Null(result);
    }


    [Fact]
    public async Task UpdateAsync_MapsDtoToEntityThenUpdates()
    {
        _repository.Setup(c => c.UpdateAsync(_entity)).Verifiable();

        await _service.UpdateAsync(_dto);

        _repository.Verify();
    }

    [Fact]
    public async Task SaveChanges_SavesChangesInRepository()
    {
        _repository.Setup(c => c.SaveChangesAsync()).Verifiable();
        await _service.SaveChangesAsync();

        _repository.Verify();
    }
}