using FluentValidation;
using MongoDBFactory.API.Entities;

namespace MongoDBFactory.API.Validators;

public sealed class DirectorValidator : AbstractValidator<Director>
{
    public DirectorValidator()
    {
        RuleFor(d => d.Name).Length(2, 100);

        RuleFor(d => d.BirthDate).LessThan(DateTime.Today);

        RuleFor(d => d.Nationality).Length(3, 100);
    }
}
