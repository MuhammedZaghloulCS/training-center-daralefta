using Application.Common;
using Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Create.AssignCourseToUser
{
    public class AssignCourseToUserCommand : IRequest<BaseResponse<string>>
    {
        public UsersCourseDTO _dto { get; set; }
    }
}
