using MongoDBFactory.API.Validators;
using UnitTests.TestBuilders;

namespace UnitTests.ValidatorsTests;

public sealed class DirectorValidatorTests
{
    private readonly DirectorValidator _directorValidator;

    public DirectorValidatorTests()
    {
        _directorValidator = new DirectorValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var director = DirectorBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _directorValidator.ValidateAsync(director, default);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var directorWithInvalidName = DirectorBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _directorValidator.ValidateAsync(directorWithInvalidName, default);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidNameParameters() =>
        new()
        {
            string.Empty,
            "a",
            new string('a', 101)
        };

    [Theory]
    [MemberData(nameof(InvalidNationalityParameters))]
    public async Task ValidateAsync_InvalidNationality_ReturnsFalse(string nationality)
    {
        // A
        var directorWithInvalidNationality = DirectorBuilder.NewObject().WithNationality(nationality).DomainBuild();

        // A
        var validationResult = await _directorValidator.ValidateAsync(directorWithInvalidNationality, default);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidNationalityParameters() =>
        new()
        {
            string.Empty,
            "a",
            new string('a', 101)
        };

    [Fact]
    public async Task ValidateAsync_InvalidBirthDate_ReturnsFalse()
    {
        // A
        var invalidBirthDate = DateTime.UtcNow.AddDays(12);
        var directorWithInvalidBirthDate = DirectorBuilder.NewObject().WithBirthDate(invalidBirthDate).DomainBuild();

        // A
        var validationResult = await _directorValidator.ValidateAsync(directorWithInvalidBirthDate, default);

        // A
        Assert.False(validationResult.IsValid);
    }
}
