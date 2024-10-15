using MongoDBFactory.API.Entities;

namespace UnitTests.TestBuilders;

public sealed class DirectorBuilder
{
    private string _name = "test";
    private string _nationality = "test";
    private DateTime _birthDate = DateTime.Now.AddYears(-18);

    public static DirectorBuilder NewObject() =>
        new();

    public Director DomainBuild() =>
        new()
        {
            BirthDate = _birthDate,
            Name = _name,
            Nationality = _nationality
        };

    public DirectorBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public DirectorBuilder WithNationality(string nationality)
    {
        _nationality = nationality;

        return this;
    }

    public DirectorBuilder WithBirthDate(DateTime birthDate)
    {
        _birthDate = birthDate;

        return this;
    }
}
