namespace MongoDBFactory.API.DataTransferObjects.Directors;

public sealed record DirectorResponse(
    string Name,
    DateTime BirthDate,
    string Nationality);
