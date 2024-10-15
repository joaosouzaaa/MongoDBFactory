namespace MongoDBFactory.API.DataTransferObjects.Directors;

public sealed record DirectorRequest(
    string Name,
    DateTime BirthDate,
    string Nationality);
