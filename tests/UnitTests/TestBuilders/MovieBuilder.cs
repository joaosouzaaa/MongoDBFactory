using MongoDBFactory.API.DataTransferObjects.Movies;
using MongoDBFactory.API.Entities;

namespace UnitTests.TestBuilders;

public sealed class MovieBuilder
{
    private string _title = "test";
    private string _genre = "test";
    private int _releaseYear = 1999;
    private readonly Guid _id = Guid.NewGuid();

    public static MovieBuilder NewObject() =>
        new();

    public Movie DomainBuild() =>
        new()
        {
            Director = DirectorBuilder.NewObject().DomainBuild(),
            Genre = _genre,
            Id = _id,
            ReleaseYear = _releaseYear,
            Title = _title
        };

    public MovieResponse ResponseBuild() =>
        new(_id,
            _title,
            _genre,
            _releaseYear,
            DirectorBuilder.NewObject().ResponseBuild());

    public CreateMovieRequest CreateRequestBuild() =>
        new(_title,
            _genre,
            _releaseYear,
            DirectorBuilder.NewObject().RequestBuild());

    public UpdateMovieRequest UpdateRequestBuild() =>
        new(_id,
            _title,
            _genre,
            _releaseYear,
            DirectorBuilder.NewObject().RequestBuild());

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
