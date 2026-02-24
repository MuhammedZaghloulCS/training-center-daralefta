using Application.Common;
using Application.Features.Session.DTOs;
using Application.Features.Session.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Queries.Handler
{
    public class GetSessionByIdHandler : IRequestHandler<GetSessionByIdQuery, BaseResponse<SessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSessionByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SessionDto>> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
        {
            var sessions = await _unitOfWork.ISession.FindRowAsync(s => s.Id == request.Id, s => s.Room, s => s.Course);
            var session = sessions.FirstOrDefault();

            if (session == null||session.IsDeleted)
            {
                return BaseResponse<SessionDto>.NotFoundResponse("Session not found");
            }

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

            return BaseResponse<SessionDto>.SuccessResponse(dto, "Session retrieved successfully");
        }
    }
}
