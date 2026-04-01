using Application.Common;
using Application.Features.User.Commands.Activation.Command;
using Application.Features.User.DTOs;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Activation.Handler
{
    public class ActivateUserByUsernameHandler : IRequestHandler<ActivateUserByUsernameCommand, BaseResponse<UserDTO>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ActivateUserByUsernameHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseResponse<UserDTO>> Handle(ActivateUserByUsernameCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                return BaseResponse<UserDTO>.NotFoundResponse($"المستخدم غير موجود");
            }
            user.IsActive = true;
            await _userManager.UpdateAsync(user);

            var userDto = user.Adapt<UserDTO>();

            return BaseResponse<UserDTO>.SuccessResponse(userDto, $"تم تفعيل المستخدم بنجاح");
        }
    }
}
