using Application.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.Delete
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, BaseResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if ((request.UserId == null || request.UserId == Guid.Empty)&&(String.IsNullOrEmpty(request.UserName)))
            {
                return BaseResponse<string>.FailureResponse(
                    "معرف المستخدم غير صالح",
                    new List<string> { "يجب تقديم معرف مستخدم صالح لحذف المستخدم." }
                );
            }
            ApplicationUser existingUser;
            if (!String.IsNullOrEmpty(request.UserName))
            {
                existingUser = await _userManager.FindByNameAsync(request.UserName);
            }
            else
            {
                existingUser = await _userManager.FindByIdAsync(request.UserId.ToString()); 
            }

            if (existingUser == null)
            {
                return BaseResponse<string>.FailureResponse(
                    "المستخدم غير موجود",
                    new List<string> { $"لا يوجد مستخدم بالمعرف: {request.UserId}" }
                );
            }

            // حذف المستخدم
            var result = await _userManager.DeleteAsync(existingUser);

            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return BaseResponse<string>.FailureResponse("فشل حذف المستخدم", errors);
            }

            return BaseResponse<string>.SuccessResponse(
                data: $"تم حذف المستخدم {existingUser.UserName} بنجاح",
                message: "تم حذف المستخدم بنجاح"
            );
        }
    }
}