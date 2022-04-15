# AutoPocoIO.Repository
[![Nuget](https://img.shields.io/nuget/v/AutoPocoIO.Repository)](https://www.nuget.org/packages/AutoPocoIO.Repository/latest)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/AutoPocoIO.Repository)](https://www.nuget.org/packages/AutoPocoIO.Repository/absoluteLatest)
[![GitHub license](https://img.shields.io/github/license/AutoPocoIO/AutoPocoIO.Repository)](https://github.com/AutoPocoIO/AutoPocoIO.Repository/blob/main/LICENSE)
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
Mark all database data transfer objects with IEntityDto
```csharp
using AutoMapper;

public class PersonEntityDto : IEntityDto
{
  public void Mapping(Profile profile)
  {
    profile.CreateProjection<PersonEntity, PersonDto>();
    profile.CreateMap<PersonEntity, PersonDto>().ReverseMap();
  }
}
```
Register services in Startup
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGenericMappingServices(typeof(Program).Assembly);
```

### Usage
Inject Operation type into controller
```csharp
private readonly IRepositoryServiceAsync<Entity, Dto> _service;
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
Modify tables with the Dto mapped to the Entity:
```csharp
await _service.AddAsync(new Dto { Name = "name1" });
await _service.DeleteAsync(1);
await _service.SaveChangesAsync();
```
