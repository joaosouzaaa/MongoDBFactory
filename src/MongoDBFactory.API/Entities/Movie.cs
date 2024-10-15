namespace MongoDBFactory.API.Entities;

public sealed class Movie
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public required int ReleaseYear { get; set; }
    public required Director Director { get; set; }
}
