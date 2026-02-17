using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Create.AssignTrainingToUser
{
    public class AssignTrainingToUserCommand : IRequest<BaseResponse<string>>
    {
        public UsersTrainingDTO _dto { get; set; }
    }
}
