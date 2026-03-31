using Application.Common;
using Domain.Entities;
using Domain.Helper;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.Delete
{
    public class DeleteUserByUserNameHandler : IRequestHandler<DeleteUserByUserNameCommand, BaseResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClient httpClient;
        private readonly ISysUnitOfWork sysUnitOfWork;
        public DeleteUserByUserNameHandler(UserManager<ApplicationUser> userManager, IHttpClientFactory httpClient,ISysUnitOfWork sysUnitOfWork)
        {
            _userManager = userManager;
            this.httpClient = httpClient.CreateClient("ExternalApi");
            this.sysUnitOfWork = sysUnitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(DeleteUserByUserNameCommand request, CancellationToken cancellationToken)
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

            if (existingUser == null||existingUser.IsDeleted)
            {
                return BaseResponse<string>.FailureResponse(
                    "المستخدم غير موجود",
                    new List<string> { $"لا يوجد مستخدم بالمعرف: {request.UserId}" }
                );
            }
            var pin = existingUser.pin;
            existingUser.IsDeleted = true;
            existingUser.pin = null;

            // حذف المستخدم
            var result = await _userManager.UpdateAsync(existingUser);

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