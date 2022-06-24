using AutoMapper;
using AutoPocoIO.Repository;

namespace AutoPocoIO.Repository.Test.Services.TestUtilities;
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
public class PersonEntity : IEquatable<PersonEntity>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public bool Equals(PersonEntity? other)
    {
        return other != null && Id == other.Id && Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PersonEntity);
    }
}

public class PersonDto : IEntityDto, IEquatable<PersonDto>, IPerson
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public bool Equals(PersonDto? other)
    {
        return other != null && other.Id == Id && other.Name == Name;
    }


    public void Mapping(Profile profile)
    {

    }
}

public interface IPerson
{
    public int Id { get; }
    public string Name { get; }
}
