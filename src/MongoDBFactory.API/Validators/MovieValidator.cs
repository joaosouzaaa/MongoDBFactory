using FluentValidation;
using MongoDBFactory.API.Entities;

namespace MongoDBFactory.API.Validators;

public sealed class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator(IValidator<Director> directorValidator)
    {
        RuleFor(m => m.Title).Length(1, 200);

        RuleFor(m => m.Genre).Length(1, 100);

        RuleFor(m => m.ReleaseYear).LessThanOrEqualTo(DateTime.Today.Year);

        RuleFor(m => m.Director).SetValidator(directorValidator);
    }
}
