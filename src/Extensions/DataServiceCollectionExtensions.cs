using System.Reflection;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoPocoIO.Repository.Internal;
using AutoPocoIO.Repository.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AutoPocoIO.Repository.Extensions;

/// <summary>
/// Extension methods for adding required services and mappings
/// </summary>
public static class DataServiceCollectionExtensions
{
    /// <summary>
    /// Add required services and create all mappings
    /// </summary>
    /// <param name="services">The service collection to add to.</param>
    /// <param name="profileAssemblies">Assemblies that include entites and dtos to map</param>
    /// <returns>The service collection to chain calls</returns>
    public static IServiceCollection AddGenericMappingServices(
        this IServiceCollection services,
        params Assembly[] profileAssemblies)
    {
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient(typeof(IRepositoryServiceAsync<,>), typeof(RepositoryServiceAsync<,>));
        services.AddScoped(typeof(IRepositoryFactory<>), typeof(RepositoryFactory<>));

        services.AddAutoMapper(c => c.AddExpressionMapping()
                                      .AddProfileAssemblies(profileAssemblies),
                                typeof(MappingProfile));

        return services;
    }

    private static IMapperConfigurationExpression AddProfileAssemblies(
        this IMapperConfigurationExpression expression,
        params Assembly[] profileAssemblies)
    {

        foreach (var asm in profileAssemblies)
        {
            expression.AddProfile(new MappingProfile(asm));
        }

        return expression;
    }
}

