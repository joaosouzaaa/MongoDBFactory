using FluentValidation;
using FluentValidation.Results;
using MongoDBFactory.API.DataTransferObjects.Movies;
using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Interfaces.Mappers;
using MongoDBFactory.API.Interfaces.Repositories;
using MongoDBFactory.API.Interfaces.Settings;
using MongoDBFactory.API.Services;
using MongoDBFactory.API.Settings.PaginationSettings;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServicesTests;

public sealed class MovieServiceTests
{
    private readonly Mock<IMovieRepository> _movieRepositoryMock;
    private readonly Mock<IMovieMapper> _movieMapperMock;
    private readonly Mock<IValidator<Movie>> _validatorMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly MovieService _movieService;

    public MovieServiceTests()
    {
        _movieRepositoryMock = new Mock<IMovieRepository>();
        _movieMapperMock = new Mock<IMovieMapper>();
        _validatorMock = new Mock<IValidator<Movie>>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _movieService = new MovieService(
            _movieRepositoryMock.Object,
            _movieMapperMock.Object,
            _validatorMock.Object,
            _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_CallsAdd()
    {
        // A
        var createMovieRequest = MovieBuilder.NewObject().CreateRequestBuild();

        var movie = MovieBuilder.NewObject().DomainBuild();
        _movieMapperMock.Setup(m => m.CreateRequestToDomain(
            It.IsAny<CreateMovieRequest>()))
            .Returns(movie);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<Movie>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        await _movieService.AddAsync(createMovieRequest, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never());

        _movieMapperMock.Verify(m => m.CreateRequestToDomain(
            It.IsAny<CreateMovieRequest>()),
            Times.Once());

        _validatorMock.Verify(v => v.ValidateAsync(
           It.IsAny<Movie>(),
           It.IsAny<CancellationToken>()),
           Times.Once());

        _movieRepositoryMock.Verify(m => m.AddAsync(
            It.IsAny<Movie>(),
            It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task AddAsync_InvalidEntity_CallsAddNotification()
    {
        // A
        var createMovieRequest = MovieBuilder.NewObject().CreateRequestBuild();

        var movie = MovieBuilder.NewObject().DomainBuild();
        _movieMapperMock.Setup(m => m.CreateRequestToDomain(
            It.IsAny<CreateMovieRequest>()))
            .Returns(movie);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "tet"),
            new("test", "tet")
        };
        var validationResult = new ValidationResult(validationFailureList);
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<Movie>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        await _movieService.AddAsync(createMovieRequest, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Exactly(validationResult.Errors.Count));

        _movieMapperMock.Verify(m => m.CreateRequestToDomain(
            It.IsAny<CreateMovieRequest>()),
            Times.Once());

        _validatorMock.Verify(v => v.ValidateAsync(
           It.IsAny<Movie>(),
           It.IsAny<CancellationToken>()),
           Times.Once());

        _movieRepositoryMock.Verify(m => m.AddAsync(
            It.IsAny<Movie>(),
            It.IsAny<CancellationToken>()),
            Times.Never());
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_CallsDelete()
    {
        // A
        var id = Guid.NewGuid();

        _movieRepositoryMock.Setup(m => m.AnyAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // A
        await _movieService.DeleteAsync(id, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never());

        _movieRepositoryMock.Verify(m => m.AnyAsync(
           It.IsAny<Guid>(),
           It.IsAny<CancellationToken>()),
           Times.Once());

        _movieRepositoryMock.Verify(m => m.DeleteAsync(
           It.IsAny<Guid>(),
           It.IsAny<CancellationToken>()),
           Times.Once());
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_CallsAddNotification()
    {
        // A
        var id = Guid.NewGuid();

        _movieRepositoryMock.Setup(m => m.AnyAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // A
        await _movieService.DeleteAsync(id, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Once());

        _movieRepositoryMock.Verify(m => m.AnyAsync(
           It.IsAny<Guid>(),
           It.IsAny<CancellationToken>()),
           Times.Once());

        _movieRepositoryMock.Verify(m => m.DeleteAsync(
           It.IsAny<Guid>(),
           It.IsAny<CancellationToken>()),
           Times.Never());
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsResponseList()
    {
        // A
        var movieList = new List<Movie>()
        {
            MovieBuilder.NewObject().DomainBuild(),
            MovieBuilder.NewObject().DomainBuild(),
            MovieBuilder.NewObject().DomainBuild()
        };
        _movieRepositoryMock.Setup(m => m.GetAllAsync(
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(movieList);

        var movieResponseList = new List<MovieResponse>()
        {
            MovieBuilder.NewObject().ResponseBuild(),
            MovieBuilder.NewObject().ResponseBuild(),
            MovieBuilder.NewObject().ResponseBuild(),
            MovieBuilder.NewObject().ResponseBuild()
        };
        _movieMapperMock.Setup(m => m.DomainListToResponseList(
            It.Is<List<Movie>>(m => m.Count == movieList.Count)))
            .Returns(movieResponseList);

        // A
        var movieResponseListResult = await _movieService.GetAllAsync(default);

        // A
        _movieRepositoryMock.Verify(m => m.GetAllAsync(
            It.IsAny<CancellationToken>()),
            Times.Once());

        _movieMapperMock.Verify(m => m.DomainListToResponseList(
            It.Is<List<Movie>>(m => m.Count == movieList.Count)),
            Times.Once());

        Assert.Equal(movieResponseList.Count, movieResponseListResult.Count);
    }

    [Fact]
    public async Task GetAllPaginatedAsync_SuccessfulScenario_ReturnsResponsePageList()
    {
        // A
        var pageParameters = new PageParameters()
        {
            PageNumber = 1,
            PageSize = 1
        };

        var movieList = new List<Movie>()
        {
            MovieBuilder.NewObject().DomainBuild(),
            MovieBuilder.NewObject().DomainBuild(),
            MovieBuilder.NewObject().DomainBuild()
        };
        var moviePageList = new PageList<Movie>()
        {
            CurrentPage = 123,
            PageSize = 3,
            Result = movieList,
            TotalCount = 6,
            TotalPages = 8
        };
        _movieRepositoryMock.Setup(m => m.GetAllPaginatedAsync(
            It.IsAny<PageParameters>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(moviePageList);

        var movieResponseList = new List<MovieResponse>()
        {
            MovieBuilder.NewObject().ResponseBuild(),
            MovieBuilder.NewObject().ResponseBuild(),
            MovieBuilder.NewObject().ResponseBuild(),
            MovieBuilder.NewObject().ResponseBuild()
        };
        var movieResponsePageList = new PageList<MovieResponse>()
        {
            CurrentPage = 1,
            PageSize = 1,
            Result = movieResponseList,
            TotalCount = 6,
            TotalPages = 8
        };
        _movieMapperMock.Setup(m => m.DomainPageListToResponsePageList(
            It.IsAny<PageList<Movie>>()))
            .Returns(movieResponsePageList);

        // A
        var movieResponsePageListResult = await _movieService.GetAllPaginatedAsync(pageParameters, default);

        // A
        _movieRepositoryMock.Verify(m => m.GetAllPaginatedAsync(
            It.IsAny<PageParameters>(),
            It.IsAny<CancellationToken>()),
            Times.Once());

        _movieMapperMock.Verify(m => m.DomainPageListToResponsePageList(
            It.IsAny<PageList<Movie>>()),
            Times.Once());

        Assert.Equal(movieResponsePageList.Result.Count, movieResponsePageListResult.Result.Count);
    }

    [Fact]
    public async Task GetByIdAsync_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var id = Guid.NewGuid();

        var movie = MovieBuilder.NewObject().DomainBuild();
        _movieRepositoryMock.Setup(m => m.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(movie);

        var movieResponse = MovieBuilder.NewObject().ResponseBuild();
        _movieMapperMock.Setup(m => m.DomainToResponse(
            It.IsAny<Movie>()))
            .Returns(movieResponse);

        // A
        var movieResponseResult = await _movieService.GetByIdAsync(id, default);

        // A
        _movieRepositoryMock.Verify(m => m.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()),
            Times.Once());

        _movieMapperMock.Verify(m => m.DomainToResponse(
            It.IsAny<Movie>()),
            Times.Once());

        Assert.NotNull(movieResponseResult);
    }

    [Fact]
    public async Task GetByIdAsync_EntityDoesNotExist_ReturnsNull()
    {
        // A
        var id = Guid.NewGuid();

        _movieRepositoryMock.Setup(m => m.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Movie?>(null));

        // A
        var movieResponseResult = await _movieService.GetByIdAsync(id, default);

        // A
        _movieRepositoryMock.Verify(m => m.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()),
            Times.Once());

        _movieMapperMock.Verify(m => m.DomainToResponse(
            It.IsAny<Movie>()),
            Times.Never());

        Assert.Null(movieResponseResult);
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_CallsUpdate()
    {
        // A
        var updateMovieRequest = MovieBuilder.NewObject().UpdateRequestBuild();

        _movieRepositoryMock.Setup(m => m.AnyAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var movie = MovieBuilder.NewObject().DomainBuild();
        _movieMapperMock.Setup(m => m.UpdateRequestToDomain(
            It.IsAny<UpdateMovieRequest>()))
            .Returns(movie);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<Movie>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        await _movieService.UpdateAsync(updateMovieRequest, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never());

        _movieRepositoryMock.Verify(m => m.AnyAsync(
           It.IsAny<Guid>(),
           It.IsAny<CancellationToken>()),
           Times.Once());

        _movieMapperMock.Verify(m => m.UpdateRequestToDomain(
            It.IsAny<UpdateMovieRequest>()),
            Times.Once());

        _validatorMock.Verify(v => v.ValidateAsync(
           It.IsAny<Movie>(),
           It.IsAny<CancellationToken>()),
           Times.Once());

        _movieRepositoryMock.Verify(m => m.UpdateAsync(
            It.IsAny<Movie>(),
            It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_CallsAddNotification()
    {
        // A
        var updateMovieRequest = MovieBuilder.NewObject().UpdateRequestBuild();

        _movieRepositoryMock.Setup(m => m.AnyAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // A
        await _movieService.UpdateAsync(updateMovieRequest, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Once());

        _movieRepositoryMock.Verify(m => m.AnyAsync(
           It.IsAny<Guid>(),
           It.IsAny<CancellationToken>()),
           Times.Once());

        _movieMapperMock.Verify(m => m.UpdateRequestToDomain(
            It.IsAny<UpdateMovieRequest>()),
            Times.Never());

        _validatorMock.Verify(v => v.ValidateAsync(
           It.IsAny<Movie>(),
           It.IsAny<CancellationToken>()),
           Times.Never());

        _movieRepositoryMock.Verify(m => m.UpdateAsync(
            It.IsAny<Movie>(),
            It.IsAny<CancellationToken>()),
            Times.Never());
    }

    [Fact]
    public async Task UpdateAsync_EntityInvalid_CallsAddNotification()
    {
        // A
        var updateMovieRequest = MovieBuilder.NewObject().UpdateRequestBuild();

        _movieRepositoryMock.Setup(m => m.AnyAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var movie = MovieBuilder.NewObject().DomainBuild();
        _movieMapperMock.Setup(m => m.UpdateRequestToDomain(
            It.IsAny<UpdateMovieRequest>()))
            .Returns(movie);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "tet"),
            new("test", "tet")
        };
        var validationResult = new ValidationResult(validationFailureList);
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<Movie>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        await _movieService.UpdateAsync(updateMovieRequest, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
             It.IsAny<string>(),
             It.IsAny<string>()),
             Times.Exactly(validationResult.Errors.Count));

        _movieRepositoryMock.Verify(m => m.AnyAsync(
           It.IsAny<Guid>(),
           It.IsAny<CancellationToken>()),
           Times.Once());

        _movieMapperMock.Verify(m => m.UpdateRequestToDomain(
            It.IsAny<UpdateMovieRequest>()),
            Times.Once());

        _validatorMock.Verify(v => v.ValidateAsync(
           It.IsAny<Movie>(),
           It.IsAny<CancellationToken>()),
           Times.Once());

        _movieRepositoryMock.Verify(m => m.UpdateAsync(
            It.IsAny<Movie>(),
            It.IsAny<CancellationToken>()),
            Times.Never());
    }
}
