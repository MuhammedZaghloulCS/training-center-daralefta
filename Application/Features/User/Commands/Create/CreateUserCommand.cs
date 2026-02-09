using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Create
{
    public class CreateUserCommand :IRequest<BaseResponse<UserDTO>>
    {
        public CreateUserDTO _dto;
        public List<string> Roles;
    }
}
