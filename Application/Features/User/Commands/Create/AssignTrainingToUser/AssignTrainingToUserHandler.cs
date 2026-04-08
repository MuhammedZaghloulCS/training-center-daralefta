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
            /*if (request == null)
                return BaseResponse<string>.BadRequestResponse("Request cannot be null");

            var training = await _unitOfWork.ITraining
                .GetByPkAsync(request._dto.TrainingId, t => t.Courses);

            if (training == null)
                return BaseResponse<string>.NotFoundResponse("Training not found");

            var usersTraining2 = request._dto.UserIds.Select(uid => new UsersTrainings { TrainingId = request._dto.TrainingId, UserId = uid }).ToList();

            // 1️⃣ Assign Training (Idempotent)
            await _unitOfWork.IAssignUserTraining
                .AddRangeIfNotExistsAsync(usersTraining2);

            var courses = await _unitOfWork.ICourse.FindRowAsync(c => c.TrainingId == request._dto.TrainingId);
            // 2️⃣ Assign Courses
            var usersCourses = courses
             .SelectMany(c => request._dto.UserIds.Select(id => new UsersCourse
             {
                 UserId = id,
                 CourseId = c.Id
             })).ToList();

            await _unitOfWork.IAssignUserCourse
                .AddRangeIfNotExistsAsync(usersCourses);

            // 3️⃣ Get all Sessions in ONE query
            var courseIds = courses.Select(c => c.Id).ToList();

            var sessions = await _unitOfWork.ISession
                .FindRowAsync(s => s.CourseId.HasValue && courseIds.Contains(s.CourseId.Value));

            // 4️⃣ Assign Sessions
            var usersSessions = sessions.SelectMany(s => request._dto.UserIds.Select(id => new UserSession
            {
                SessionId = s.Id,
                UserId = id
            })).ToList();
            
                await _unitOfWork.IAssignUserSession
                    .AddRangeIfNotExistsAsync(usersSessions);
            

            await _unitOfWork.Complete();*/

            return BaseResponse<string>.SuccessResponse("Success");
        }

    }
}
