using Application.Features.User.Commands;
using FluentValidation;

public class CreateUserCommandValidator
    : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x._dto.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MinimumLength(3).WithMessage("First name length must be more than 2");

        RuleFor(x => x._dto.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MinimumLength(3).WithMessage("Last name length must be more than 2"); ;

        RuleFor(x => x._dto.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x._dto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches("[A-Z]").WithMessage("Password must contain capital letter")
            .Matches("[0-9]").WithMessage("Password must contain number");

        RuleFor(x => x._dto.PhoneNumber)
            .NotEmpty();

        RuleFor(x => x._dto.BirthDate)
            .NotEmpty();
    }
}
