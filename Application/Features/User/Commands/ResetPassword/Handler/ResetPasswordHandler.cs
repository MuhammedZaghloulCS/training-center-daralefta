using Application.Common;
using Application.Features.User.Commands.ResetPassword.Command;
using Application.Features.User.DTOs;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.ResetPassword.Handler
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, BaseResponse<UserDTO>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ResetPasswordHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseResponse<UserDTO>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request == null ||  string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return(BaseResponse<UserDTO>.FailureResponse("طلب غير صالح: ايميل وكلمة المرور الجديدة مطلوبان."));
            }

            var userFromId= await _userManager.FindByIdAsync(request.Id);
            var userFromUsername= await _userManager.FindByNameAsync(request.UserName);
            var userFromEmail= await _userManager.FindByEmailAsync(request.Email);
            if (userFromId == null || userFromUsername == null || userFromEmail == null)
                return (BaseResponse<UserDTO>.FailureResponse("طلب غير صالح: لا تقم بتعديل اي شئ عند استعادة كلمة السر.")); ;

            bool isSameUser =
                userFromId.Id == userFromUsername.Id &&
                userFromId.Id == userFromEmail.Id;

            if (!isSameUser)
                return (BaseResponse<UserDTO>.FailureResponse("طلب غير صالح: لا تقم بتعديل اي شئ عند استعادة كلمة السر."));


           var isRemoved= await _userManager.RemovePasswordAsync(userFromEmail);
            var isChanged=await _userManager.AddPasswordAsync(userFromEmail, request.Password);
            if(!isRemoved.Succeeded&&!isChanged.Succeeded)
                return (BaseResponse<UserDTO>.FailureResponse("طلب غير صالح:حاول مره اخري."));



            var userDto = userFromEmail.Adapt<UserDTO>();
            var roles = await _userManager.GetRolesAsync(userFromId);
            userDto.roles = roles.ToList();
            return BaseResponse<UserDTO>.SuccessResponse(userDto, "تم تغير كلمة السر بنجاح");


        }
    }
}
