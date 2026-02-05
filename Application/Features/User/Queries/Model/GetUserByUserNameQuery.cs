using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Model
{
    public class GetUserByUserNameQuery : IRequest<BaseResponse<UserDTO>>
    {
        public string UserName { get; set; }
    }
}
