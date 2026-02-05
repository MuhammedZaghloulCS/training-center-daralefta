using Application.Common;
using MediatR;
using System;

namespace Application.Features.User.Commands.Delete
{
    public class DeleteUserByUserNameCommand : IRequest<BaseResponse<string>>
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
    }
}