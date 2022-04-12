using System.Reflection;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoPocoIO.Repository.Internal;
using AutoPocoIO.Repository.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AutoPocoIO.Repository.Extensions;

public static class DataServiceCollectionExtensions
{
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

