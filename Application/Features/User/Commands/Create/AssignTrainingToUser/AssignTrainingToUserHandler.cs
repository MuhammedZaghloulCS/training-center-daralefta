using Application.Common;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Create.AssignTrainingToUser
{
    public class AssignTrainingToUserHandler : IRequestHandler<AssignTrainingToUserCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssignTrainingToUserHandler(IUnitOfWork unitOfWork)
        {
           _unitOfWork=unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(
    AssignTrainingToUserCommand request,
    CancellationToken cancellationToken)
        {
            if (request == null)
                return BaseResponse<string>.BadRequestResponse("Request cannot be null");

            var training = await _unitOfWork.ITraining
                .GetByPkAsync(request._dto.TrainingId, t => t.Courses);

            if (training == null)
                return BaseResponse<string>.NotFoundResponse("Training not found");

            // 1️⃣ Assign Training (Idempotent)
            await _unitOfWork.IAssignUserTraining
                .AddIfNotExistsAsync(new UsersTrainings
                {
                    TrainingId = request._dto.TrainingId,
                    UserId = request._dto.UserId
                });

            var courses = await _unitOfWork.ICourse.FindRowAsync(c => c.TrainingId == request._dto.TrainingId);
            // 2️⃣ Assign Courses
            var userCourses = courses.Select(c => new UsersCourse
            {
                UserId = request._dto.UserId,
                CourseId = c.Id
            }).ToList();

            await _unitOfWork.IAssignUserCourse
                .AddRangeIfNotExistsAsync(userCourses);

            // 3️⃣ Get all Sessions in ONE query
            var courseIds = courses.Select(c => c.Id).ToList();

            var sessions = await _unitOfWork.ISession
                .FindRowAsync(s => s.CourseId.HasValue && courseIds.Contains(s.CourseId.Value));

            // 4️⃣ Assign Sessions
            var userSessions = sessions.Select(s => new UserSession
            {
                SessionId = s.Id,
                UserId = request._dto.UserId
            }).ToList();
            
                await _unitOfWork.IAssignUserSession
                    .AddRangeIfNotExistsAsync(userSessions);
            

            await _unitOfWork.Complete();

            return BaseResponse<string>.SuccessResponse("Success");
        }

    }
}
