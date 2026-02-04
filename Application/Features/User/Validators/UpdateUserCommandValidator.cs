using Application.Features.User.Commands.Update;
using FluentValidation;
using System;

namespace Application.Features.User.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.User)
                .NotNull()
                .WithMessage("بيانات المستخدم مطلوبة");

            RuleFor(x => x.User.Id)
                .NotEmpty()
                .WithMessage("معرف المستخدم مطلوب");

            RuleFor(x => x.User.Email)
                .NotEmpty()
                .WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress()
                .WithMessage("صيغة البريد الإلكتروني غير صحيحة");

            RuleFor(x => x.User.PhoneNumber)
                .NotEmpty()
                .WithMessage("رقم الهاتف مطلوب");

            RuleFor(x => x.User.FirstName)
                .NotEmpty()
                .WithMessage("الاسم الأول مطلوب")
                .MaximumLength(100)
                .WithMessage("الاسم الأول لا يمكن أن يتجاوز 100 حرف");

            RuleFor(x => x.User.LastName)
                .NotEmpty()
                .WithMessage("الاسم الأخير مطلوب")
                .MaximumLength(100)
                .WithMessage("الاسم الأخير لا يمكن أن يتجاوز 100 حرف");

            RuleFor(x => x.User.Gender)
                .IsInEnum()
                .WithMessage("قيمة الجنس غير صحيحة");

            RuleFor(x => x.User.PersonType)
                .IsInEnum()
                .WithMessage("نوع الشخص غير صحيح");

            RuleFor(x => x.User.JobTitle)
                .NotEmpty()
                .WithMessage("المسمى الوظيفي مطلوب")
                .MaximumLength(300)
                .WithMessage("المسمى الوظيفي لا يمكن أن يتجاوز 300 حرف");

            RuleFor(x => x.User.AcademicTitle)
                .MaximumLength(500)
                .WithMessage("اللقب الأكاديمي لا يمكن أن يتجاوز 500 حرف");

            RuleFor(x => x.User.Organization)
                .MaximumLength(500)
                .WithMessage("اسم الجهة لا يمكن أن يتجاوز 500 حرف");

            RuleFor(x => x.User.Specialization)
                .MaximumLength(500)
                .WithMessage("التخصص لا يمكن أن يتجاوز 500 حرف");

            RuleFor(x => x.User.Skills)
                .MaximumLength(500)
                .WithMessage("المهارات لا يمكن أن تتجاوز 500 حرف");

            RuleFor(x => x.User.WhatsappNumber)
                .MaximumLength(50)
                .WithMessage("رقم الواتساب لا يمكن أن يتجاوز 50 حرف");

            RuleFor(x => x.User.BirthDate)
                .NotEmpty()
                .WithMessage("تاريخ الميلاد مطلوب")
                .LessThan(DateTime.Now)
                .WithMessage("تاريخ الميلاد يجب أن يكون في الماضي");

            RuleFor(x => x.User.AddressInsideCairo)
                .MaximumLength(500)
                .WithMessage("العنوان داخل القاهرة لا يمكن أن يتجاوز 500 حرف");

            RuleFor(x => x.User.AddressOutsideCairo)
                .MaximumLength(500)
                .WithMessage("العنوان خارج القاهرة لا يمكن أن يتجاوز 500 حرف");

            // التحقق من كلمة المرور (اختياري عند التحديث)
            When(x => !string.IsNullOrEmpty(x.User.Password), () =>
            {
                RuleFor(x => x.User.Password)
                    .MinimumLength(8)
                    .WithMessage("كلمة المرور يجب أن تكون 8 أحرف على الأقل")
                    .Matches(@"[A-Z]")
                    .WithMessage("كلمة المرور يجب أن تحتوي على حرف كبير واحد على الأقل")
                    .Matches(@"[a-z]")
                    .WithMessage("كلمة المرور يجب أن تحتوي على حرف صغير واحد على الأقل")
                    .Matches(@"[0-9]")
                    .WithMessage("كلمة المرور يجب أن تحتوي على رقم واحد على الأقل")
                    .Matches(@"[\W_]")
                    .WithMessage("كلمة المرور يجب أن تحتوي على رمز خاص واحد على الأقل");

                RuleFor(x => x.User.ConfirmPassword)
                    .Equal(x => x.User.Password)
                    .WithMessage("كلمات المرور غير متطابقة");
            });
        }
    }
}