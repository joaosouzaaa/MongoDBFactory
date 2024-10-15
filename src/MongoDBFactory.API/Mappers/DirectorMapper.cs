using MongoDBFactory.API.DataTransferObjects.Directors;
using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Interfaces.Mappers;

namespace MongoDBFactory.API.Mappers;

public sealed class DirectorMapper : IDirectorMapper
{
    public DirectorResponse DomainToResponse(Director director) =>
        new(director.Name,
            director.BirthDate,
            director.Nationality);

    public Director RequestToDomain(DirectorRequest directorRequest) =>
        new()
        {
            Name = directorRequest.Name,
            BirthDate = directorRequest.BirthDate,
            Nationality = directorRequest.Nationality
        };
}
