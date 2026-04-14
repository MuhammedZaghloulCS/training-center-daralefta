using Application.Common;
using Domain.Entities;
using Domain.Helper;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using Infrastructure.Implementations.UnitOfWork.SysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.Delete
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserByEmailCommand, BaseResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClient httpClient;
        private readonly ISysUnitOfWork sysUnitOfWork;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserHandler(UserManager<ApplicationUser> userManager, IHttpClientFactory httpClient,ISysUnitOfWork sysUnitOfWork,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            this.httpClient = httpClient.CreateClient("ExternalApi");
            this.sysUnitOfWork = sysUnitOfWork;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(DeleteUserByEmailCommand request, CancellationToken cancellationToken)
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
            
            
            if (existingUser == null ||existingUser.IsDeleted)
            {
                return BaseResponse<string>.FailureResponse(
                    "المستخدم غير موجود",
                    new List<string> { $"لا يوجد مستخدم بالمعرف: {request.Email}" }
                );
            }
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            if (userRoles.Contains("Instructor"))
            {
                var userSessions=await _unitOfWork.IUserSessionRepository.FindRowAsync(x => x.UserId == existingUser.Id);
                if (userSessions.Any())
                {
                    return BaseResponse<string>.FailureResponse(
                      "فشل حذف المستخدم",
                      new List<string> { $"لا يمكن حذف المستخدم {existingUser.FullName} لأنه مرتبط بجلسات تدريبية." });
                }
            }
            var pin = existingUser.pin;
            existingUser.IsDeleted = true;
            // حذف المستخدم
            existingUser.pin= null;
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