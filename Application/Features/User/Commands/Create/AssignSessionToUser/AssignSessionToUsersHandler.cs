using Application.Common;
using Application.Features.User.Commands.Create.AssignCourseToUser;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Commands.Create.AssignSessionToUser
{
    public class AssignSessionToUsersHandler : IRequestHandler<AssignSessionToUsersCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssignSessionToUsersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(
    AssignSessionToUsersCommand request,
    CancellationToken cancellationToken)
        {
            if (request == null)
                return BaseResponse<string>.BadRequestResponse("Request cannot be null");

            var Session = await _unitOfWork.ISession
                .GetByPkAsync(request._dto.sessionId);

            if (Session == null)
                return BaseResponse<string>.NotFoundResponse("Session not found");

            var usersSession2 = request._dto.UserIds.Select(uid => new UserSession { SessionId = request._dto.sessionId, UserId = uid }).ToList();

            // 1️⃣ Assign Training (Idempotent)
            await _unitOfWork.IAssignUserSession
                .AddRangeIfNotExistsAsync(usersSession2);

            

            await _unitOfWork.Complete();

            return BaseResponse<string>.SuccessResponse("Success");
        }

    }
    
 
}
