using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Update
{
    public class UpdateUserCommand : IRequest<BaseResponse<UserDTO>>
    {
        public UpdateUserDTO UpdateUser { get; set; }
    }
}
