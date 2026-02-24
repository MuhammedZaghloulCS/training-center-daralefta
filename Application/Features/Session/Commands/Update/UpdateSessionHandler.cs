using Application.Common;
using Application.Features.Session.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Commands.Update
{
    public class UpdateSessionHandler : IRequestHandler<UpdateSessionCommand, BaseResponse<SessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSessionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SessionDto>> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.ISession.GetByPkAsync(request.Id);
            if (session == null||session.IsDeleted)
            {
                return BaseResponse<SessionDto>.NotFoundResponse("Session not found");
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Topic))
                errors.Add("Topic is required");

            if (request.RoomId < 1)
                errors.Add("RoomId is invalid");

            if (request.CourseId < 1)
                errors.Add("CourseId is invalid");

            if (request.SessionDate == default)
                errors.Add("SessionDate is required");

            if (request.EndTime <= request.StartTime)
                errors.Add("EndTime must be greater than StartTime");

            if (errors.Any())
                return BaseResponse<SessionDto>.FailureResponse("Validation failed", errors);

            session.SessionDate = request.SessionDate;
            session.StartTime = request.StartTime;
            session.EndTime = request.EndTime;
            session.Topic = request.Topic;
            session.RoomId = request.RoomId;
            session.CourseId = request.CourseId;
            session.UpdatedBy = request.UpdatedBy;
            session.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ISession.Update(session);
            await _unitOfWork.Complete();

            var dto = new SessionDto
            {
                Id = session.Id,
                CreatedBy = session.CreatedBy,
                CreatedDate = session.CreatedDate,
                UpdatedBy = session.UpdatedBy,
                UpdatedAt = session.UpdatedAt,
                SessionDate = session.SessionDate,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Topic = session.Topic,
                RoomId = session.RoomId,
                CourseId = session.CourseId,
                LecturerId = session.lecturerId
            };

            return BaseResponse<SessionDto>.SuccessResponse(dto, "Session updated successfully");
        }
    }
}
