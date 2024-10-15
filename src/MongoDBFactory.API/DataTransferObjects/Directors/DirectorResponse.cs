namespace MongoDBFactory.API.DataTransferObjects.Directors;

public sealed record DirectorResponse(
    string Name,
    DateTime DateOfBirth,
    string Nationality);
