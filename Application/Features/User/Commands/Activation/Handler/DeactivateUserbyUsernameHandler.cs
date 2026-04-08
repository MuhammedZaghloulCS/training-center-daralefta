using Application.Common;
using Application.Features.User.Commands.DeActivate.Command;
using Application.Features.User.DTOs;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.DeActivate.Handler
{
    public class DeactivateUserbyUsernameHandler : IRequestHandler<DeactivateUserByUsernameCommand, BaseResponse<UserDTO>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public DeactivateUserbyUsernameHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseResponse<UserDTO>> Handle(DeactivateUserByUsernameCommand request, CancellationToken cancellationToken)
        {   
            var user=await _userManager.FindByNameAsync(request.Username);

            if(user == null)
            {
                return BaseResponse<UserDTO>.NotFoundResponse($"المستخدم غير موجود");
            }
            if (!user.IsActive)
            {
                return BaseResponse<UserDTO>.BadRequestResponse($"المستخدم معطل بالفعل.");
            }
            var role=await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                return BaseResponse<UserDTO>.BadRequestResponse($"لا يمكن تعطيل المستخدم المسؤول.");
            }
            user.IsActive = false;
           await _userManager.UpdateAsync(user);

            var userDto = user.Adapt<UserDTO>();

            return BaseResponse<UserDTO>.SuccessResponse(userDto, $"تم تعطيل المستخدم.");
        }
    }
}
