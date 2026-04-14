using Application.Common;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
namespace Application.Features.User.Queries.Handler
{
    public class GetUsersInRoleHandler : IRequestHandler<GetUsersInRoleQuery, BaseResponse<List<UserDTO>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUsersInRoleHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager=userManager;
        }
        public async Task<BaseResponse<List<UserDTO>>> Handle(GetUsersInRoleQuery request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(UsersRolesEnum), request.Role)||request.Role==0)
            {
                BaseResponse<List<UserDTO>>.FailureResponse("Invalid request: Role is required and must be a valid UsersRolesEnum value.");
            }
            
            var roleName = request.Role.GetDescription();
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

             var result=usersInRole.Where(u=>!u.IsDeleted && u.IsActive).OrderByDescending(u => u.CreatingDate).Adapt<List<UserDTO>>();

            return  BaseResponse<List<UserDTO>>.SuccessResponse(result, $"Users in role {request.Role} retrieved successfully.");

        }
    }
}
