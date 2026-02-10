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

        RuleFor(x => x._dto.Password)
            .NotEmpty().WithMessage("كلمة المرور مطلوبة")
            .MinimumLength(8).WithMessage("كلمة المرور يجب ألا تقل عن 8 أحرف")
            .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$")
            .WithMessage("كلمة المرور يجب أن تحتوي على حرف كبير، حرف صغير، رقم، ورمز خاص");


        RuleFor(x => x._dto.ConfirmPassword)
    .NotEmpty().WithMessage("تأكيد كلمة المرور مطلوبة")
    .Equal(x => x._dto.Password)
    .WithMessage("كلمات المرور غير متطابقة");


        RuleFor(x => x._dto.PhoneNumber)
     .NotEmpty().WithMessage("رقم الموبايل مطلوب")
     .Matches(@"^(?:\+2|002)?01[0125][0-9]{8}$")
     .WithMessage("رقم الموبايل غير صحيح (رقم مصري)");



        RuleFor(x => x._dto.BirthDate)
      .NotEmpty().WithMessage("تاريخ الميلاد مطلوب")
      .LessThan(DateTime.Today)
      .WithMessage("تاريخ الميلاد يجب أن يكون في الماضي");
    }
}
