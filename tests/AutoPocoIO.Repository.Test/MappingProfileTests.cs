using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoPocoIO.Repository.Test.Services.TestUtilities;
using Xunit;

namespace AutoPocoIO.Repository.Test;

public class MappingProfileTests
{
    public static IEnumerable<object[]> _data => new List<object[]>()
    {
         new object[]{ new MappingProfile(), "AutoPocoIO.Repository"},
         new object []{ new MappingProfile(typeof(MappingProfileTests).Assembly), "AutoPocoIO.Repository.Test" }
    };

    public class PersonDtoWithMap : IEntityDto, IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEntity, PersonDtoWithMap>();
        }
    }




    [Theory]
    [MemberData(nameof(_data))]
    public void AutoMapper_ConfigurationValid(object profile, string profileName)
    {
        var mappingProfile = profile as MappingProfile;

        var configuration = new MapperConfiguration(c => c.AddProfile(mappingProfile));
        configuration.AssertConfigurationIsValid();

        Assert.NotNull(mappingProfile);
        Assert.Equal(profileName, mappingProfile.ProfileName);
    }

    [Theory]
    [InlineData(typeof(PersonDtoWithMap))]
    [InlineData(typeof(OtherPersonDto))]
    [InlineData(typeof(DefaultMapPersonDto))]
    public void ApplyMappingsFromAssembly_AddsPersonEntiyToDtoMapping(Type destionationType)
    {
        var configuration = new MapperConfiguration(c => c.AddProfile(new MappingProfile(typeof(MappingProfileTests).Assembly)));
        var mapper = configuration.CreateMapper();

        PersonEntity entity = new()
        {
            Id = 1,
            Name = "name1"
        };

        IPerson? dto = mapper.Map(entity, typeof(PersonEntity), destionationType) as IPerson;

        Assert.NotNull(dto);
        Assert.Equal(1, dto.Id);
        Assert.Equal("name1", dto.Name);

    }

    [Fact]
    public void ApplyMappingsFromAssembly_AddsPersonEntiyToDtoProjection()
    {
        var configuration = new MapperConfiguration(c => c.AddProfile(new MappingProfile(typeof(MappingProfileTests).Assembly)));
        var mapper = configuration.CreateMapper();

        PersonEntity entity = new()
        {
            Id = 1,
            Name = "name1"
        };

        var list = new[] { entity }.AsQueryable();

        var dto = (IPerson)list.ProjectTo<DefaultProjectPersonDto>(configuration)
                                .First();
                        
        Assert.NotNull(dto);
        Assert.Equal(1, dto.Id);
        Assert.Equal("name1", dto.Name);

    }
}
