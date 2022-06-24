using AutoMapper;

namespace AutoPocoIO.Repository;

/// <summary>
/// One way mapping only entity
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IMapFrom<T> : IEntityDto
{
    void IEntityDto.Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
