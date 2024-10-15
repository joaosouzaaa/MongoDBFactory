namespace MongoDBFactory.API.Entities;

public sealed class Director
{
    public required string Name { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string Nationality { get; set; }
}
