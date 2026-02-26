using Application.Common;
using Application.Features.User.DTOs;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Model
{
    public class GetUsersInRoleQuery : IRequest<BaseResponse<List<UserDTO>>>
    {
        public UsersRolesEnum Role { get; set; }
    }
}
