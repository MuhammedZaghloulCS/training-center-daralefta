using Application.Common;
using Application.Features.Session.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Commands.Create
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, BaseResponse<SessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSessionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SessionDto>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
                errors.Add("CreatedBy is required");

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

            var session = new Domain.Entities.Session
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                SessionDate = request.SessionDate,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Topic = request.Topic,
                RoomId = request.RoomId,
                CourseId = request.CourseId
            };

            await _unitOfWork.ISession.AddAsync(session);
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
                CourseId = session.CourseId
            };

            return BaseResponse<SessionDto>.SuccessResponse(dto, "Session created successfully");
        }
    }
}
