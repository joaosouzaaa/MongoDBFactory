namespace MongoDBFactory.API.DataTransferObjects.Directors;

public sealed record DirectorRequest(
    string Name,
    DateTime DateOfBirth,
    string Nationality);
