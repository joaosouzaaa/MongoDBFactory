using FluentValidation;
using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Validators;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ValidatorsTests;

public sealed class MovieValidatorTests
{
    private readonly Mock<IValidator<Director>> _directorValidatorMock;
    private readonly MovieValidator _movieValidator;

    public MovieValidatorTests()
    {
        _directorValidatorMock = new Mock<IValidator<Director>>();
        _movieValidator = new MovieValidator(_directorValidatorMock.Object);
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var movie = MovieBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _movieValidator.ValidateAsync(movie, default);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidTitleParameters))]
    public async Task ValidateAsync_InvalidTitle_ReturnsFalse(string title)
    {
        // A
        var movieWithInvalidTitle = MovieBuilder.NewObject().WithTitle(title).DomainBuild();

        // A
        var validationResult = await _movieValidator.ValidateAsync(movieWithInvalidTitle, default);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidTitleParameters() =>
        new()
        {
            string.Empty,
            new string('a', 201)
        };

    [Theory]
    [MemberData(nameof(InvalidGenreParameters))]
    public async Task ValidateAsync_InvalidGenre_ReturnsFalse(string genre)
    {
        // A
        var movieWithInvalidGenre = MovieBuilder.NewObject().WithGenre(genre).DomainBuild();

        // A
        var validationResult = await _movieValidator.ValidateAsync(movieWithInvalidGenre, default);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidGenreParameters() =>
        new()
        {
            string.Empty,
            new string('a', 101)
        };

    [Fact]
    public async Task ValidateAsync_InvalidReleaseYear_ReturnsFalse()
    {
        // A
        var invalidReleaseYear = DateTime.UtcNow.AddYears(1).Year;
        var movieWithInvalidReleaseYear = MovieBuilder.NewObject().WithReleaseYear(invalidReleaseYear).DomainBuild();

        // A
        var validationResult = await _movieValidator.ValidateAsync(movieWithInvalidReleaseYear, default);

        // A
        Assert.False(validationResult.IsValid);
    }
}
