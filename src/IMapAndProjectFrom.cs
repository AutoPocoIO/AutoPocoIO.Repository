using System.Reflection;
using AutoMapper;

namespace AutoPocoIO.Repository;

/// <summary>
/// Create default mapping and projection
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IMapAndProjectFrom<T> : IEntityDto
{
    void IEntityDto.Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType()).ReverseMap();

        //Project
        MethodInfo? method = typeof(Profile).GetMethod(nameof(Profile.CreateProjection), Array.Empty<Type>());
        MethodInfo? genericMethod = method?.MakeGenericMethod(typeof(T), GetType());
        genericMethod?.Invoke(profile, null);
    }
}
