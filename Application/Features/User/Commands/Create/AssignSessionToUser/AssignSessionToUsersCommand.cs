using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Create.AssignSessionToUser
{
    public class AssignSessionToUsersCommand : IRequest<BaseResponse<string>>
    {
        public UsersSessionDTO _dto { get; set; }
    }
}
