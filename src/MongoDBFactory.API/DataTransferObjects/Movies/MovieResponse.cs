using MongoDBFactory.API.DataTransferObjects.Directors;

namespace MongoDBFactory.API.DataTransferObjects.Movies;

public sealed record MovieResponse(
    Guid Id,
    string Title,
    string Genre,
    int ReleaseYear,
    DirectorResponse Director);
