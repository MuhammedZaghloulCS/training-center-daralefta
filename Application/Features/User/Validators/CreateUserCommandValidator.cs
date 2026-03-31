using Application.Features.User.Commands.Create.CreateUser;
using FluentValidation;

public class CreateUserCommandValidator
    : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x._dto.FirstName)
            .NotEmpty().WithMessage("الاسم الأول مطلوب")
            .MinimumLength(3).WithMessage("الاسم الأول يجب ألا يقل عن 3 أحرف");

        RuleFor(x => x._dto.LastName)
            .NotEmpty().WithMessage("اسم العائلة مطلوب")
            .MinimumLength(3).WithMessage("اسم العائلة يجب ألا يقل عن 3 أحرف");

        RuleFor(x => x._dto.Email)
            .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
            .EmailAddress().WithMessage("البريد الإلكتروني غير صالح");



        RuleFor(x => x._dto.BirthDate)
      .NotEmpty().WithMessage("تاريخ الميلاد مطلوب")
      .LessThan(DateTime.Today)
      .WithMessage("تاريخ الميلاد يجب أن يكون في الماضي");
    }
}
