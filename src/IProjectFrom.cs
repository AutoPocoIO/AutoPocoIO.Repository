using System.Reflection;
using AutoMapper;

namespace AutoPocoIO.Repository;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IProjectFrom<T> : IEntityDto
{
    void IEntityDto.Mapping(Profile profile)
    {
        //Project
        MethodInfo? method = typeof(Profile).GetMethod(nameof(Profile.CreateProjection), Array.Empty<Type>());
        MethodInfo? genericMethod = method?.MakeGenericMethod(typeof(T), GetType());
        genericMethod?.Invoke(profile, null);
    }
}