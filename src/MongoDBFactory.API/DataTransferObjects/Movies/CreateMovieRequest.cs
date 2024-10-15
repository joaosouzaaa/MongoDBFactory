using MongoDBFactory.API.DataTransferObjects.Directors;

namespace MongoDBFactory.API.DataTransferObjects.Movies;

public sealed record CreateMovieRequest(
    string Title,
    string Genre,
    int ReleaseYear,
    DirectorRequest Director);
