using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Activation.Command
{
    public class ActivateUserByUsernameCommand : IRequest<BaseResponse<UserDTO>>
    {
        public string Username { get; set; }
    }
}
