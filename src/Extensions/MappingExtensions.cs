using AutoMapper;

namespace AutoPocoIO.Repository.Extensions
{
    /// <summary>
    /// Extensions for adding mappings to the <see cref="Profile"/>
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Create required projection and 2-way mapping for an entity
        /// </summary>
        /// <typeparam name="Entity">Database entity type</typeparam>
        /// <typeparam name="EntityDto">Data trasfer object type</typeparam>
        /// <param name="profile"><see cref="Profile"/> to add mappings to.</param>
        public static void CreateDefaultMappings<Entity, EntityDto>(this Profile profile)
        {
            profile.CreateProjection<Entity, EntityDto>();
            profile.CreateMap<Entity, EntityDto>().ReverseMap();
        }
    }
}

