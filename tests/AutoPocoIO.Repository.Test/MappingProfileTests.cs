using AutoMapper;
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

    public class PersonDtoWithMap : IEntityDto
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

    [Fact]
    public void ApplyMappingsFromAssembly_AddsPersonEntiyToDtoMapping()
    {
        var configuration = new MapperConfiguration(c => c.AddProfile(new MappingProfile(typeof(MappingProfileTests).Assembly)));
        var mapper = configuration.CreateMapper();

        PersonEntity entity = new()
        {
            Id = 1,
            Name = "name1"
        };

        var dto = mapper.Map<PersonDtoWithMap>(entity);

        Assert.Equal(1, dto.Id);
        Assert.Equal("name1", dto.Name);

    }
}
