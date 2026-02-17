using Application.Common;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Create.AssignCourseToUser
{
    public class AssignCourseToUserHandler : IRequestHandler<AssignCourseToUserCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssignCourseToUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(
    AssignCourseToUserCommand request,
    CancellationToken cancellationToken)
        {
            if (request == null)
                return BaseResponse<string>.BadRequestResponse("Request cannot be null");

            var Course = await _unitOfWork.ICourse
                .GetByPkAsync(request._dto.CourseId, t => t.Sessions);

            if (Course == null)
                return BaseResponse<string>.NotFoundResponse("Course not found");

            var usersCourse2 = request._dto.UserIds.Select(uid => new UsersCourse { CourseId = request._dto.CourseId, UserId = uid }).ToList();

            await _unitOfWork.IAssignUserCourse
                .AddRangeIfNotExistsAsync(usersCourse2);

            var sessions = await _unitOfWork.ISession.FindRowAsync(c => c.CourseId == request._dto.CourseId);
            // 2️⃣ Assign sessions
            var usersSessions = sessions
             .SelectMany(c => request._dto.UserIds.Select(id => new UserSession
             {
                 UserId = id,
                 SessionId = c.Id
             })).ToList();

            await _unitOfWork.IAssignUserSession
                .AddRangeIfNotExistsAsync(usersSessions);


            await _unitOfWork.Complete();

            return BaseResponse<string>.SuccessResponse("Success");
        }

    }
}
