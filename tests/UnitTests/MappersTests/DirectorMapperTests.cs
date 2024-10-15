using MongoDBFactory.API.Mappers;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;

public sealed class DirectorMapperTests
{
    private readonly DirectorMapper _directorMapper;

    public DirectorMapperTests()
    {
        _directorMapper = new DirectorMapper();
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var director = DirectorBuilder.NewObject().DomainBuild();

        // A
        var directorResponseResult = _directorMapper.DomainToResponse(director);

        // A
        Assert.Equal(director.Name, directorResponseResult.Name);
        Assert.Equal(director.BirthDate, directorResponseResult.BirthDate);
        Assert.Equal(director.Nationality, directorResponseResult.Nationality);
    }

    [Fact]
    public void RequestToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var directorRequest = DirectorBuilder.NewObject().RequestBuild();

        // A
        var directorResult = _directorMapper.RequestToDomain(directorRequest);

        // A
        Assert.Equal(directorRequest.Name, directorResult.Name);
        Assert.Equal(directorRequest.BirthDate, directorResult.BirthDate);
        Assert.Equal(directorRequest.Nationality, directorResult.Nationality);
    }
}
