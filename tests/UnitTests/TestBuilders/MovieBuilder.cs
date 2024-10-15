using MongoDBFactory.API.Entities;

namespace UnitTests.TestBuilders;

public sealed class MovieBuilder
{
    private string _title = "test";
    private string _genre = "test";
    private int _releaseYear = 1999;

    public static MovieBuilder NewObject() =>
        new();

    public Movie DomainBuild() =>
        new()
        {
            Director = DirectorBuilder.NewObject().DomainBuild(),
            Genre = _genre,
            Id = Guid.NewGuid(),
            ReleaseYear = _releaseYear,
            Title = _title
        };

    public MovieBuilder WithTitle(string title)
    {
        _title = title;

        return this;
    }

    public MovieBuilder WithGenre(string genre)
    {
        _genre = genre;

        return this;
    }

    public MovieBuilder WithReleaseYear(int releaseYear)
    {
        _releaseYear = releaseYear;

        return this;
    }
}
