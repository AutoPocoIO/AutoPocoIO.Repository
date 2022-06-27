using AutoMapper;

namespace AutoPocoIO.Repository;

/// <summary>
/// Marks a class as a data transfer object for a database entity
/// </summary>
public interface IEntityDto
{ 
    /// <summary>
    /// Configuration for mapping to entity.
    /// </summary>
    /// <param name="profile">Profile to add the mappings to</param>
    void Mapping(Profile profile);
    /// <summary>
    /// (Optional) Changes to default mapping after 
    /// </summary>
    /// <param name="expression"></param>
    void AfterMapping(IMappingExpression expression) { }
}