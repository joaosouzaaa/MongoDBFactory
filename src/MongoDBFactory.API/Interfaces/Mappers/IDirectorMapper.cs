using MongoDBFactory.API.DataTransferObjects.Directors;
using MongoDBFactory.API.Entities;

namespace MongoDBFactory.API.Interfaces.Mappers;

public interface IDirectorMapper
{
    DirectorResponse DomainToResponse(Director director);
    Director RequestToDomain(DirectorRequest directorRequest);
}
