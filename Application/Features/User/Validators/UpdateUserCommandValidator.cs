using Application.Features.User.Commands.Update;
using FluentValidation;
using System;

namespace Application.Features.User.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UpdateUser)
                .NotNull()
                .WithMessage("بيانات المستخدم مطلوبة");


            RuleFor(x => x.UpdateUser.Email)
                .NotEmpty()
                .WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress()
                .WithMessage("صيغة البريد الإلكتروني غير صحيحة");

            RuleFor(x => x.UpdateUser.PhoneNumber)
                .NotEmpty()
                .WithMessage("رقم الهاتف مطلوب");

            RuleFor(x => x.UpdateUser.FirstName)
                .NotEmpty()
                .WithMessage("الاسم الأول مطلوب")
                .MaximumLength(100)
                .WithMessage("الاسم الأول لا يمكن أن يتجاوز 100 حرف");

            RuleFor(x => x.UpdateUser.LastName)
                .NotEmpty()
                .WithMessage("الاسم الأخير مطلوب")
                .MaximumLength(100)
                .WithMessage("الاسم الأخير لا يمكن أن يتجاوز 100 حرف");

            RuleFor(x => x.UpdateUser.Gender)
    .InclusiveBetween(1, 2)
                .WithMessage("قيمة الجنس غير صحيحة");

        
            RuleFor(x => x.UpdateUser.JobTitle)
                .NotEmpty()
                .WithMessage("المسمى الوظيفي مطلوب")
                .MaximumLength(300)
                .WithMessage("المسمى الوظيفي لا يمكن أن يتجاوز 300 حرف");

            RuleFor(x => x.UpdateUser.AcademicTitle)
                .MaximumLength(500)
                .WithMessage("اللقب الأكاديمي لا يمكن أن يتجاوز 500 حرف");

            RuleFor(x => x.UpdateUser.Organization)
                .MaximumLength(500)
                .WithMessage("اسم الجهة لا يمكن أن يتجاوز 500 حرف");

            RuleFor(x => x.UpdateUser.Specialization)
                .MaximumLength(500)
                .WithMessage("التخصص لا يمكن أن يتجاوز 500 حرف");

            RuleFor(x => x.UpdateUser.Skills)
                .MaximumLength(500)
                .WithMessage("المهارات لا يمكن أن تتجاوز 500 حرف");

            RuleFor(x => x.UpdateUser.WhatsappNumber)
                .MaximumLength(50)
                .WithMessage("رقم الواتساب لا يمكن أن يتجاوز 50 حرف");

            RuleFor(x => x.UpdateUser.BirthDate)
                .NotEmpty()
                .WithMessage("تاريخ الميلاد مطلوب")
                .LessThan(DateTime.Now)
                .WithMessage("تاريخ الميلاد يجب أن يكون في الماضي");

            RuleFor(x => x.UpdateUser.AddressInsideCairo)
                .MaximumLength(500)
                .WithMessage("العنوان داخل القاهرة لا يمكن أن يتجاوز 500 حرف");

            RuleFor(x => x.UpdateUser.AddressOutsideCairo)
                .MaximumLength(500)
                .WithMessage("العنوان خارج القاهرة لا يمكن أن يتجاوز 500 حرف");


        }
    }
}