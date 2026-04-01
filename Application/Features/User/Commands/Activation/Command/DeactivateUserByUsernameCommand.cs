using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.DeActivate.Command
{
    public class DeactivateUserByUsernameCommand : IRequest<BaseResponse<UserDTO>>
    {
        public string Username { get; set; }
    }
}
