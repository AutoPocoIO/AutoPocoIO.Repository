using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;

namespace AutoPocoIO.Repository;

public class MappingProfile : Profile
{
    public MappingProfile() : base(Assembly.GetExecutingAssembly().GetName().Name)
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

    }

    public MappingProfile(Assembly assembly) : base(assembly.GetName().Name)
    {
        ApplyMappingsFromAssembly(assembly);
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(c => typeof(IEntityDto).IsAssignableFrom(c) && !c.IsInterface && !c.IsAbstract);
        bool HasInterface(Type t) => t.IsGenericType
            && ( t.GetGenericTypeDefinition() == typeof(IMapFrom<>) || 
                 t.GetGenericTypeDefinition() == typeof(IMapAndProjectFrom<>));

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod(nameof(IEntityDto.Mapping));
            methodInfo?.Invoke(instance, new object[] { this });

            var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

            if (interfaces.Count > 0)
            {
                foreach (var @interface in interfaces)
                {
                    var interfaceMethodInfo = @interface.GetMethod(
                        $"{typeof(IEntityDto).FullName}.{nameof(IEntityDto.Mapping)}",
                        BindingFlags.NonPublic | BindingFlags.Instance);
               
                    interfaceMethodInfo?.Invoke(instance, new object[] { this });
                }
            }

        }

    }

    public static object Cast(Type Type, object data)
    {
        var DataParam = Expression.Parameter(typeof(object), "data");
        var Body = Expression.Block(Expression.Convert(Expression.Convert(DataParam, data.GetType()), Type));

        var Run = Expression.Lambda(Body, DataParam).Compile();
        var ret = Run.DynamicInvoke(data);
        return ret;
    }

    public override string ToString()
    {
        return ProfileName;
    }
}
