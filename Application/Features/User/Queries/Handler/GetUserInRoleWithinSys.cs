using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Handler { 

    using Domain.Entities;
    using Domain.Enums;
    using global::Application.Common;
    using global::Application.Features.User.DTOs;
    using global::Application.Features.User.Queries.Model;
    using Infrastructure.Migrations;
    using Mapster;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Application.Features.User.Queries.Handler
    {
        public class GetUserInRoleWithinSysHandler : IRequestHandler<GetUserInRoleWithinSysQuery, BaseResponse<List<UserDTO>>>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public GetUserInRoleWithinSysHandler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<BaseResponse<List<UserDTO>>> Handle(GetUserInRoleWithinSysQuery request, CancellationToken cancellationToken)
            {
                if (request == null || !Enum.IsDefined(typeof(UsersRolesEnum), request.Role))
                {
                    BaseResponse<List<UserDTO>>.FailureResponse("Invalid request: Role is required and must be a valid UsersRolesEnum value.");
                }

                var roleName = request.Role.GetDescription();
                var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            var invalidPins = new List<string?> { "", "0", null," " };

            var result = usersInRole
                .Where(u => !u.IsDeleted && !invalidPins.Contains(u.pin))
                .Adapt<List<UserDTO>>();

            return BaseResponse<List<UserDTO>>.SuccessResponse(result, $"Users in role {request.Role} retrieved successfully.");

            }
        }
    }

}
