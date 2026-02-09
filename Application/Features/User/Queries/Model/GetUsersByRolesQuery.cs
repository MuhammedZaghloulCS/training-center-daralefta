using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Model
{
    public class GetUsersByRolesQuery : IRequest<BaseResponse<List<UserDTO>>>
    {
        public string roleName { get; set; }
    }
}
