using AutoMapper;

namespace AutoPocoIO.Repository;

/// <summary>
/// Marks a class as a data transfer object for a database entity
/// </summary>
public interface IEntityDto
{
    void Mapping(Profile profile);
}

