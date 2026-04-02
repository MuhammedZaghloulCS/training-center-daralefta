using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Features.User.Commands.ResetPassword.Command
{
    public class ResetPasswordCommand : IRequest<BaseResponse<UserDTO>>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }


        [MinLength(8, ErrorMessage = "يجب أن تتكون كلمة المرور من 8 أحرف على الأقل.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$",
            ErrorMessage = "يجب أن تحتوي كلمة المرور على حرف كبير واحد على الأقل، وحرف صغير واحد على الأقل، ورقم واحد على الأقل، ورمز خاص واحد على الأقل.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "كلمات المرور غير متطابقة.")]
        public string ConfirmPassword
        {
            get; set;
        }
    }
}
