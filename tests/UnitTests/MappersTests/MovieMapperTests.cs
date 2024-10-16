using MongoDBFactory.API.DataTransferObjects.Directors;
using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Interfaces.Mappers;
using MongoDBFactory.API.Mappers;
using MongoDBFactory.API.Settings.PaginationSettings;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;

public sealed class MovieMapperTests
{
    private readonly Mock<IDirectorMapper> _directorMapperMock;
    private readonly MovieMapper _movieMapper;

    public MovieMapperTests()
    {
        _directorMapperMock = new Mock<IDirectorMapper>();
        _movieMapper = new MovieMapper(_directorMapperMock.Object);
    }

    [Fact]
    public void CreateRequestToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var createMovieRequest = MovieBuilder.NewObject().CreateRequestBuild();

        var director = DirectorBuilder.NewObject().DomainBuild();
        _directorMapperMock.Setup(d => d.RequestToDomain(
            It.IsAny<DirectorRequest>()))
            .Returns(director);

        // A
        var movieResult = _movieMapper.CreateRequestToDomain(createMovieRequest);

        // A
        Assert.Equal(createMovieRequest.Title, movieResult.Title);
        Assert.Equal(createMovieRequest.Genre, movieResult.Genre);
        Assert.Equal(createMovieRequest.ReleaseYear, movieResult.ReleaseYear);
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario_ReturnsResponseList()
    {
        // A
        var movieList = new List<Movie>()
        {
            MovieBuilder.NewObject().DomainBuild(),
            MovieBuilder.NewObject().DomainBuild(),
            MovieBuilder.NewObject().DomainBuild()
        };

        var directorResponse = DirectorBuilder.NewObject().ResponseBuild();
        _directorMapperMock.SetupSequence(d => d.DomainToResponse(
            It.IsAny<Director>()))
            .Returns(directorResponse)
            .Returns(directorResponse)
            .Returns(directorResponse);

        // A
        var movieResponseListResult = _movieMapper.DomainListToResponseList(movieList);

        // A
        Assert.Equal(movieList.Count, movieResponseListResult.Count);
    }

    [Fact]
    public void DomainPageListToResponsePageList_SuccessfulScenario_ReturnsResponsePageList()
    {
        // A
        var movieList = new List<Movie>()
        {
            MovieBuilder.NewObject().DomainBuild(),
            MovieBuilder.NewObject().DomainBuild()
        };
        var moviePageList = new PageList<Movie>()
        {
            CurrentPage = 1,
            PageSize = 11,
            Result = movieList,
            TotalCount = 12,
            TotalPages = 13
        };
        var directorResponse = DirectorBuilder.NewObject().ResponseBuild();
        _directorMapperMock.SetupSequence(d => d.DomainToResponse(
            It.IsAny<Director>()))
            .Returns(directorResponse)
            .Returns(directorResponse);

        // A
        var movieResponsePageListResult = _movieMapper.DomainPageListToResponsePageList(moviePageList);

        // A
        Assert.Equal(moviePageList.CurrentPage, movieResponsePageListResult.CurrentPage);
        Assert.Equal(moviePageList.PageSize, movieResponsePageListResult.PageSize);
        Assert.Equal(moviePageList.Result.Count, movieResponsePageListResult.Result.Count);
        Assert.Equal(moviePageList.TotalCount, movieResponsePageListResult.TotalCount);
        Assert.Equal(moviePageList.TotalPages, movieResponsePageListResult.TotalPages);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var movie = MovieBuilder.NewObject().DomainBuild();

        var directorResponse = DirectorBuilder.NewObject().ResponseBuild();
        _directorMapperMock.Setup(d => d.DomainToResponse(
            It.IsAny<Director>()))
            .Returns(directorResponse);

        // A
        var movieResponseResult = _movieMapper.DomainToResponse(movie);

        // A
        Assert.Equal(movie.Id, movieResponseResult.Id);
        Assert.Equal(movie.Title, movieResponseResult.Title);
        Assert.Equal(movie.Genre, movieResponseResult.Genre);
        Assert.Equal(movie.ReleaseYear, movieResponseResult.ReleaseYear);
    }

    [Fact]
    public void UpdateRequestToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var updateMovieRequest = MovieBuilder.NewObject().UpdateRequestBuild();

        var director = DirectorBuilder.NewObject().DomainBuild();
        _directorMapperMock.Setup(d => d.RequestToDomain(
            It.IsAny<DirectorRequest>()))
            .Returns(director);

        // A
        var movieResult = _movieMapper.UpdateRequestToDomain(updateMovieRequest);

        // A
        Assert.Equal(updateMovieRequest.Id, movieResult.Id);
        Assert.Equal(updateMovieRequest.Title, movieResult.Title);
        Assert.Equal(updateMovieRequest.Genre, movieResult.Genre);
        Assert.Equal(updateMovieRequest.ReleaseYear, movieResult.ReleaseYear);
    }
}
