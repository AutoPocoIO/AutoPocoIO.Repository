using AutoMapper;

namespace AutoPocoIO.Repository.Test.Services.TestUtilities;
public class OtherPersonDto: IMapFrom<DefaultMapPersonDto>, IEntityDto, IPerson
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;


    public void Mapping(Profile profile)
    {
        profile.CreateMap<PersonEntity, OtherPersonDto>();
    }
}

public class DefaultMapPersonDto : IMapFrom<PersonEntity>, IPerson
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
