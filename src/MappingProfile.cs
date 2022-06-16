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

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod(nameof(IEntityDto.Mapping));
            methodInfo!.Invoke(instance, new object[] { this });
        }

    }

    public override string ToString()
    {
        return ProfileName;
    }
}
