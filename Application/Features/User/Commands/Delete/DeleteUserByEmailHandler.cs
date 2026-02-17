using Application.Common;
using Domain.Entities;
using Domain.Helper;
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
        public DeleteUserHandler(UserManager<ApplicationUser> userManager, IHttpClientFactory httpClient,ISysUnitOfWork sysUnitOfWork)
        {
            _userManager = userManager;
            this.httpClient = httpClient.CreateClient("ExternalApi");
            this.sysUnitOfWork = sysUnitOfWork;
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
            var personOnSys = await sysUnitOfWork.ISysPersonRepository.GetPersonByPinAsync(pin);
            sysUnitOfWork.ISysPersonRepository.Delete(personOnSys);
            await sysUnitOfWork.Complete();

            return BaseResponse<string>.SuccessResponse(
                data: $"تم حذف المستخدم {existingUser.UserName} بنجاح",
                message: "تم حذف المستخدم بنجاح"
            );
        }
    }
}