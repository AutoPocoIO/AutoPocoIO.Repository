using System.Reflection;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoPocoIO.Repository;
using AutoPocoIO.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

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
    public static IServiceCollection AddAutoPocoIORepository(
        this IServiceCollection services,
        params Assembly[] profileAssemblies)
    {
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient(typeof(IRepositoryServiceAsync<,>), typeof(RepositoryServiceAsync<,>));
        services.AddScoped(typeof(IRepositoryFactory<>), typeof(RepositoryFactory<>));

        services.AddAutoMapper(c => c.AddExpressionMapping()
                                      .AddProfileAssemblies(profileAssemblies),
                                typeof(MappingProfile));

        services.AddDbContexts();

        return services;
    }

    /// <summary>
    /// Add required services and create all mappings
    /// </summary>
    /// <param name="services">The service collection to add to.</param>
    /// <param name="profileAssemblies">Assemblies that include entites and dtos to map</param>
    /// <returns>The service collection to chain calls</returns>
    public static IServiceCollection AddAutoPocoIORepository<TContext>(
        this IServiceCollection services,
        params Assembly[] profileAssemblies) where TContext : DbContext
    {
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient(typeof(IRepositoryServiceAsync<,>), typeof(RepositoryServiceAsync<,>));
        services.AddScoped(typeof(IRepositoryFactory<>), typeof(RepositoryFactory<>));

        services.AddAutoMapper(c => c.AddExpressionMapping()
                                      .AddProfileAssemblies(profileAssemblies),
                                typeof(MappingProfile));

        services.AddDbContexts();

        return services;
    }

    /// <summary>
    /// Add all DbContexts as the base type
    /// </summary>
    /// <returns>The service collection to chain calls</returns>
    public static void AddDbContexts(this IServiceCollection services)
    {
        var dbContexts = services.Select(c => c.ServiceType)
                                .Where(c => typeof(DbContext).IsAssignableFrom(c)).ToList();

        foreach (var context in dbContexts)
            services.AddScoped(typeof(DbContext), context);
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
