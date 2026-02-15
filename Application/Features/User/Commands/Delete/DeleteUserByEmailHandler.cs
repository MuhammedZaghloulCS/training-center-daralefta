using Application.Common;
using Domain.Entities;
using Domain.Helper;
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
        private readonly HttpClient httpClient;
        public DeleteUserHandler(UserManager<ApplicationUser> userManager, IHttpClientFactory httpClient)
        {
            _userManager = userManager;
            this.httpClient = httpClient.CreateClient("ExternalApi");
        }

        public async Task<BaseResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if ((String.IsNullOrEmpty(request.Email)))
            {
                return BaseResponse<string>.FailureResponse(
                    "معرف المستخدم غير صالح",
                    new List<string> { "يجب تقديم معرف مستخدم صالح لحذف المستخدم." }
                );
            }
            ApplicationUser existingUser;
            
                existingUser = await _userManager.FindByEmailAsync(request.Email);
            
            
            if (existingUser == null)
            {
                return BaseResponse<string>.FailureResponse(
                    "المستخدم غير موجود",
                    new List<string> { $"لا يوجد مستخدم بالمعرف: {request.Email}" }
                );
            }
            var pin = existingUser.pin;
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
            await httpClient.DeleteAsync(MainConstants.Use("person/delete/" + pin));

            return BaseResponse<string>.SuccessResponse(
                data: $"تم حذف المستخدم {existingUser.UserName} بنجاح",
                message: "تم حذف المستخدم بنجاح"
            );
        }
    }
}