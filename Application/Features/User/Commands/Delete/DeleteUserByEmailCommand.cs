using Application.Common;
using MediatR;
using System;

namespace Application.Features.User.Commands.Delete
{
    public class DeleteUserByEmailCommand : IRequest<BaseResponse<string>>
    {
 
        public string Email { get; set; }
    }
}