# AutoPocoIO.Repository

[![Build status](https://ci.appveyor.com/api/projects/status/j82hp84cmocj5vae/branch/main?svg=true)](https://ci.appveyor.com/project/pjames997/autopocoio-repository/branch/main)
[![codecov](https://codecov.io/gh/AutoPocoIO/AutoPocoIO.Repository/branch/main/graph/badge.svg?token=h2WjFKaYNT)](https://codecov.io/gh/AutoPocoIO/AutoPocoIO.Repository)

### Installation
```
dotnet add package AutoPocoIO.Repository
```
### How do I get started?
Mark all database entites with IEntity
```csharp
public class PersonEntity : IEntity
{
}
```
Mark all database data transfer objects with IEntityDto and IMapping
```csharp
using AutoMapper;

public class PersonEntityDto : IEntityDto, IMapping
{
  public void Mapping(Profile profile)
  {
    profile.CreateProjection<PersonEntity, PersonDto>();
    profile.CreateMap<PersonEntity, PersonDto>().ReverseMap();
  }
}
```
### Usage
Inject Operation type into controller
```csharp
public SampleController(IRepositoryServiceAsync<Entity, Dto> service)
{
    _service = service;
}
```

Create and load an object from a Database Table or View:
```csharp
var foo = _service.GetAll();
var bar = _service.GetAll(c => c.Name == "test");
```
Update tables with the Dto mapped to the Entity:
```csharp
var foo = await _service.AddAsync(new Dto { Name = "name1" });
await _service.DeleteAsync(1);
```
